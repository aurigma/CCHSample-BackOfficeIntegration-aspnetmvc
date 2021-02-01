public class CustomersCanvasOptions 
{
    public const string SectionName = "CustomersCanvas";

    public string IdentityProviderUrl { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ApiUrl { get; set; }
    public int TenantId { get; set; }

    public string DesignEditorVersion { get; set; }
    public int StorefrontId { get; set; }
}