using System;
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
        private const int UserMaxWorkspaceCreation = 5;

        [UserValidation]
        [NameValidation]
        [HttpPost]
        public IActionResult Post([FromBody] WorkspaceAddPayload body, [XUserId] string identifier)
        {
            if (MockDatabase.Workspaces.Count(w => w.Owner == identifier) == UserMaxWorkspaceCreation)
                return BadRequest(new ErrorResponse("생성할 수 있는 Workspace 갯수를 초과했습니다.", "TOO_MANY_WORKSPACES"));

            var workspace = new Workspace(body.Name, identifier, identifier);
            var member = new WorkspaceMember(workspace.Id, identifier);

            MockDatabase.AddWorkspace(workspace);
            MockDatabase.WorkspaceMembers.Add(member);

            return Ok(new {workspaceId = workspace.Id});
        }

        [UserValidation]
        [WorkspaceValidation]
        [HttpGet("{workspaceId}")]
        public IActionResult Get([FromRoute] Guid workspaceId, [XUserId] string identifier)
        {
            if (MockDatabase.WorkspaceMembers.All(u => u.WorkspaceId != workspaceId || u.Identifer != identifier))
            {
                return BadRequest(new ErrorResponse("You're not member in this Workspace.", "BAD_REQUEST"));
            }

            return Ok(GetWorkspace(workspaceId));
        }

        [UserValidation]
        [NameValidation]
        [WorkspaceValidation]
        [HttpPatch("{workspaceId}")]
        public IActionResult Patch(
            [FromRoute] Guid workspaceId,
            [XUserId] string identifier,
            [FromBody] WorkspaceAddPayload body)
        {
            var workspace = GetWorkspace(workspaceId);

            if (workspace.Owner != identifier)
                return BadRequest(new ErrorResponse("Only owner can rename this Workspace.", "BAD_REQUEST"));

            workspace.Name = body.Name;

            return Ok();
        }

        [WorkspaceValidation]
        [HttpDelete("{workspaceId}")]
        public IActionResult Delete([FromRoute] Guid workspaceId, [XUserId] string identifier)
        {
            var workspace = GetWorkspace(workspaceId);

            if (workspace.Owner != identifier)
                return BadRequest(new ErrorResponse("Only owner can delete this Workspace.", "BAD_REQUEST"));

            MockDatabase.RemoveWorkspace(workspace);

            return Ok();
        }
    }
}