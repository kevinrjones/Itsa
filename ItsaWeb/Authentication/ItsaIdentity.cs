namespace ItsaWeb.Authentication
{
    public class ItsaIdentity : IItsaIdentity
    {
        public ItsaIdentity(string name)
        {
            Name = name;            
            IsAuthenticated = true;
        }
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string UserId { get; private set; }
    }
}