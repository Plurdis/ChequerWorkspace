using System.Collections.Generic;
using System.Linq;
using ChequerWorkspace.Database.Model;

namespace ChequerWorkspace.Database
{
    public static class MockDatabase
    {
        static MockDatabase()
        {
            Users.Add(new User("Evan Choi", "evan_choi@chequer.io"));
            Users.Add(new User("Kevin So", "kevin_so@chequer.io"));
            Users.Add(new User("Brant Hwang", "brant_hwang@chequer.io"));
            Users.Add(new User("Kies Uhm", "kies_uhm@chequer.io"));
            Users.Add(new User("Tony Jang", "tony_jang@chequer.io"));
        }

        public static IList<User> Users { get; } = new List<User>();

        public static IEnumerable<Workspace> Workspaces => _workspaces.Where(w => !w.IsDeleted);

        public static IList<WorkspaceMember> WorkspaceMembers { get; } = new List<WorkspaceMember>();

        private static readonly List<Workspace> _workspaces = new List<Workspace>();

        public static void AddWorkspace(Workspace workspace)
        {
            _workspaces.Add(workspace);
        }

        public static void RemoveWorkspace(Workspace workspace)
        {
            workspace.IsDeleted = true;
        }
    }
}