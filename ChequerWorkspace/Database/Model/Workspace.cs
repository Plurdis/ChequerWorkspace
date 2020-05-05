using System.Collections;
using System.Collections.Generic;

namespace ChequerWorkspace.Database.Model
{
    public class Workspace
    {
        private static int _autoIncrease = 0;
        public Workspace(string name, string creator, string owner, int maximum = 10)
        {
            Id = ++_autoIncrease;
            
            Name = name;
            Creator = creator;
            Owner = owner;
            Maximum = maximum;
        }
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Creator { get; set; }
        
        public string Owner { get; set; }
        
        public int Maximum { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
