using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChequerWorkspace.Controllers.Commons;
using Microsoft.AspNetCore.Mvc;
using ChequerWorkspace.Database;
using ChequerWorkspace.Database.Model;
using ChequerWorkspace.Filters;
using ChequerWorkspace.Models;
using ChequerWorkspace.Models.Payloads;

namespace ChequerWorkspace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkspaceController : WorkspaceControllerBase
    {
        [UserValidation]
        [NameValidation]
        [HttpPost]
        public IActionResult Post([FromHeader] string identifier, [FromBody] WorkspaceAddPayload body)
        {
            if (MockDatabase.Workspaces.Count(w => !w.IsDeleted && w.Owner == identifier) == 5)
                return BadRequest(new ErrorResponse("생성할 수 있는 Workspace 갯수를 초과했습니다.", "TOO_MANY_WORKSPACES"));
            
            var workspace = new Workspace(body.Name, identifier, identifier);
            var member = new WorkspaceMember(identifier, workspace.Id);
            
            MockDatabase.Workspaces.Add(workspace);
            MockDatabase.WorkspaceMembers.Add(member);
        
            return Ok(new {workspaceId = workspace.Id});
        }

        [UserValidation]
        [WorkspaceValidation]
        [HttpGet("{workspaceId}")]
        public IActionResult Get([FromHeader] string identifier, string workspaceId)
        {
            if (MockDatabase.WorkspaceMembers.Count(u => u.WorkspaceId == int.Parse(workspaceId) && u.Identifer == identifier) == 0)
            {
                return BadRequest(new ErrorResponse("You're not member in this Workspace.", "BAD_REQUEST"));
            }
            return Ok(GetWorkspace(int.Parse(workspaceId)));
        }

        [UserValidation]
        [NameValidation]
        [WorkspaceValidation]
        [HttpPatch("{workspaceId}")]
        public IActionResult Patch([FromHeader] string identifier, string workspaceId, [FromBody] WorkspaceAddPayload body)
        {
            var workspace = GetWorkspace(int.Parse(workspaceId));
            
            if (workspace.Owner != identifier)
                return BadRequest(new ErrorResponse("Only owner can rename this Workspace.", "BAD_REQUEST"));
            
            workspace.Name = identifier;
            
            return Ok();
        }
        
        [WorkspaceValidation]
        [HttpDelete("{workspaceId}")]
        public IActionResult Delete([FromHeader] string identifier, string workspaceId)
        {
            var workspace = GetWorkspace(int.Parse(workspaceId));

            if (workspace.Owner != identifier)
                return BadRequest(new ErrorResponse("Only owner can delete this Workspace.", "BAD_REQUEST"));
        
            workspace.IsDeleted = true;
            
            return Ok();
        }
    }
}