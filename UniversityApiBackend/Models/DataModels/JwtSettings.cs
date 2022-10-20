namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool? ValidateIssuerSigningkey { get; set; }
        public string IssuerSigningkey { get; set; } = string.Empty;
        public bool ValidateIssuer { get; set; } = true;
        public string? ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
    }
}
