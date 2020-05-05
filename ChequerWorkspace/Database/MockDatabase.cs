﻿using System.Collections.Generic;
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
        
        public static IList<Workspace> Workspaces { get; } = new List<Workspace>();

        public static IList<WorkspaceMember> WorkspaceMembers { get; } = new List<WorkspaceMember>();
    }
}