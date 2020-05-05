using System.Collections.Generic;

namespace ChequerWorkspace.Models.Payloads
{
    public class WorkspaceRemoveUsersPayload
    {
        public IEnumerable<string> RemoveUsers { get; set; }
    }
}