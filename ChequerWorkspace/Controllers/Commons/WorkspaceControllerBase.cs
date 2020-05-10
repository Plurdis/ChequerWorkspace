using System;
using System.Linq;
using ChequerWorkspace.Database;
using ChequerWorkspace.Database.Model;
using Microsoft.AspNetCore.Mvc;

namespace ChequerWorkspace.Controllers.Commons
{
    public class WorkspaceControllerBase : ControllerBase
    {
        protected static Workspace GetWorkspace(Guid workspaceId)
        {
            var workspace = MockDatabase.Workspaces.FirstOrDefault(i => i.Id == workspaceId);

            return workspace;
        }
    }
}