namespace Docusign.API.Models.Utils
{
    public class DocusigJWTSettings
    {
        public string ClientId { get; set; }
        public string ImpersonatedUserId { get; set; }
        public string AuthServer { get; set; }
        public string PrivateKeyFile { get; set; }
    }
}
