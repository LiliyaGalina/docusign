namespace Docusign.API.Models.Utils
{
    public class DocusignSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthorizationEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string UserInformationEndpoint { get; set; }
        public string AppUrl { get; set; }
        public string SignerEmail { get; set; }
        public string SignerName { get; set; }
        public string GatewayAccountId { get; set; }
        public string GatewayName { get; set; }
        public string GatewayDisplayName { get; set; }
    }
}
