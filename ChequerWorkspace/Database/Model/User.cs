namespace ChequerWorkspace.Database.Model
{
    public class User
    {
        public User(string name, string identifier)
        {
            Name = name;
            Identifier = identifier;
        }
        
        public string Name { get; }
        
        public string Identifier { get; }
    }
}
