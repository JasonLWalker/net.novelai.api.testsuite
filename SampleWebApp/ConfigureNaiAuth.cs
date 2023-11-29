using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace SampleWebApp
{
    public class ConfigureNaiAuth : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        public ConfigureNaiAuth()
        {

        }

        public void Configure(string? name, CookieAuthenticationOptions options)
        {
            if (name == Program.NaiAuthScheme)
            {

            }
        }

        public void Configure(CookieAuthenticationOptions options)
        {
            Configure(Options.DefaultName, options);
        }
    }
}
