using System;

namespace ChequerWorkspace.Database.Model
{
    public class Workspace
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Creator { get; set; }

        public string Owner { get; set; }

        public int Maximum { get; set; }

        public bool IsDeleted { get; set; }

        public Workspace(string name, string creator, string owner, int maximum = 10)
        {
            Id = Guid.NewGuid();
            Name = name;
            Creator = creator;
            Owner = owner;
            Maximum = maximum;
        }
    }
}
