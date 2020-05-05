using System.Linq;
using ChequerWorkspace.Database;
using ChequerWorkspace.Database.Model;
using ChequerWorkspace.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChequerWorkspace.Controllers.Commons
{
    public class WorkspaceControllerBase : ControllerBase
    {
        protected Workspace GetWorkspace(int workspaceId)
        {
            var workspace = MockDatabase.Workspaces
                .FirstOrDefault(i => 
                    i.Id == workspaceId && !i.IsDeleted);

            return workspace;
        }
    }
}