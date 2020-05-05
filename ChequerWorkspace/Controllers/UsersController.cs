using System.Collections.Generic;
using System.Linq;
using ChequerWorkspace.Controllers.Commons;
using ChequerWorkspace.Database;
using ChequerWorkspace.Database.Model;
using ChequerWorkspace.Filters;
using ChequerWorkspace.Models;
using ChequerWorkspace.Models.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace ChequerWorkspace.Controllers
{    
    [Route("Workspace/{workspaceId}/[controller]")]
    public class UsersController : WorkspaceControllerBase
    {
        [UserValidation]
        [WorkspaceValidation]
        [HttpGet]
        public IActionResult Get(string workspaceId)
        {
            var workspace = GetWorkspace(int.Parse(workspaceId));
            
            var users = MockDatabase
                .WorkspaceMembers
                .Where(i => i.WorkspaceId == workspace.Id)
                .Select(i => new
                {
                    Name = i.Identifer,
                    Status = workspace.Owner == i.Identifer ? "Owner" : "Member",
                });
        
            return Ok(users);
        }

        [UserValidation]
        [WorkspaceValidation]
        [WorkspaceUserValidation]
        [HttpPost]
        public IActionResult Post([FromHeader] string identifier, string workspaceId, [FromBody] WorkspaceUserPayload body)
        {
            var workspace = GetWorkspace(int.Parse(workspaceId));
            var workspaceMembers = MockDatabase.WorkspaceMembers
                .Where(m => m.WorkspaceId == workspace.Id)
                .ToList();
            
            if (workspace.Owner != identifier)
                return BadRequest(new ErrorResponse("Only owner can add user to this Workspace.", "BAD_REQUEST"));
            
            if (workspaceMembers.Count(i => i.Identifer == body.Identifier) > 0)
                return BadRequest(new ErrorResponse("This user is already added in this Workspace.", "ALREADY_ADDED"));;
            
            if (workspaceMembers.Count() == workspace.Maximum)
                return BadRequest(new ErrorResponse("This workspace is already full.", "WORKSPACE_USER_FULL"));
            
            MockDatabase.WorkspaceMembers.Add(new WorkspaceMember(body.Identifier, workspace.Id));
            return Ok();
        }

        [UserValidation]
        [WorkspaceValidation]
        [HttpPatch]
        public IActionResult Patch([FromHeader] string identifier, string workspaceId, [FromBody] WorkspaceRemoveUsersPayload body)
        {
            var workspace = GetWorkspace(int.Parse(workspaceId));

            if (workspace.Owner != identifier)
                return BadRequest(new ErrorResponse("Only owner can add user to this Workspace.", "BAD_REQUEST"));

            var members = new List<WorkspaceMember>();
            
            foreach (string removeUser in body.RemoveUsers)
            {
                var member = MockDatabase.WorkspaceMembers.FirstOrDefault(i => i.Identifer == removeUser);
                if (member == null)
                    return Unauthorized(new ErrorResponse("User does not exists", "INVALID_USER_IDENTIFIER"));
                
                members.Add(member);
            }
            
            members.ForEach(m => MockDatabase.WorkspaceMembers.Remove(m));
            
            return Ok();
        }
        
        [UserValidation]
        [WorkspaceValidation]
        [HttpDelete("{userIdentifier}")]
        public IActionResult Delete([FromHeader] string identifier, string workspaceId, string userIdentifier)
        {
            var workspace = GetWorkspace(int.Parse(workspaceId));
            var workspaceMember = MockDatabase.WorkspaceMembers
                .FirstOrDefault(m => 
                    m.WorkspaceId == workspace.Id && m.Identifer == userIdentifier);
            
            if (workspace.Owner != identifier)
                return BadRequest(new ErrorResponse("Only owner can remove user to this Workspace.", "BAD_REQUEST"));
            
            if (workspace.Owner == userIdentifier)
                return BadRequest(new ErrorResponse("Workspace's owner cannot removed.", "OWNER_CANNOT_REMOVED"));
            
            if (workspaceMember == null)
                return BadRequest(new ErrorResponse("This user is not exists in this Workspace.", "NOT_EXISTS_USER"));;
            
            MockDatabase.WorkspaceMembers.Remove(workspaceMember);
            return Ok();
        }
    }
}