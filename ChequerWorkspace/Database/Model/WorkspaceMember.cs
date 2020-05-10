using System;

namespace ChequerWorkspace.Database.Model
{
    public class WorkspaceMember
    {
        public WorkspaceMember(Guid workspaceId, string identifier)
        {
            WorkspaceId = workspaceId;
            Identifer = identifier;
        }

        public Guid WorkspaceId { get; set; }

        public string Identifer { get; set; }
    }
}