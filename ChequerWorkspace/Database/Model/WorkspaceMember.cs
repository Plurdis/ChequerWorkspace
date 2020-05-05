namespace ChequerWorkspace.Database.Model
{
    public class WorkspaceMember
    {
        public WorkspaceMember(string identifier, int workspaceId)
        {
            Identifer = identifier;
            WorkspaceId = workspaceId;
        }
        
        public string Identifer { get; set; }
        
        public int WorkspaceId { get; set; }
    }
}