using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleApp_ClintTest_0.Services;

namespace ConsoleApp_ClintTest_0.Students
{
    public partial class Students
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; } 

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("grad")]
        public int Grad { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("hashPassword")]
        public string? HashPassword { get; set; }

        [JsonPropertyName("role")]
        public Role Role { get; set; }

        [JsonPropertyName("refreshTokenHash")]
        public string? RefreshTokenHash { get; set; }

        [JsonPropertyName("refreshTokenExpiresAt")]
        public DateTime? RefreshTokenExpiresAt { get; set; }

        [JsonPropertyName("refreshTokenRevokedAt")]
        public DateTime? RefreshTokenRevokedAt { get; set; }
    }

}
