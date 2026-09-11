
using Microsoft.AspNetCore.Cors.Infrastructure;

public class CorsSecurityConfiguration
{

    public static void CorsSetupActions(CorsOptions options)
    {
        options.DefaultPolicyName = "FrontendPolicy";
        options.AddDefaultPolicy(ConfigureCorsPolicy);
    }

    public static void ConfigureCorsPolicy(CorsPolicyBuilder policy)
    {
        
        string[] allowedOrigins =
        {
            "",
            "",
            ""
        };

        string[] allowedMethods =
        {
            "",
            "",
            ""
        };

        string[] allowedHeaders =
        {
            "",
            "",
            ""
        };

        policy.WithOrigins(allowedOrigins);
        policy.WithMethods(allowedMethods);
        policy.WithHeaders(allowedHeaders);

    }

}