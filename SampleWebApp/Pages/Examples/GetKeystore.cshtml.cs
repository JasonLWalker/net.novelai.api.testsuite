using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;
using net.novelai.authentication;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GetKeystoreModel : PageModel
    {
        [BindProperty]
        public IDictionary<string, string> Keystore { get; set; } = new Dictionary<string, string>(){};

        public async Task<IActionResult> OnGet()
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken });
            var keys = api?.GetKeystore();
            if (keys != null)
            {
                foreach (var entry in keys)
                {
                    Keystore[entry.Key] = Auth.ByteArrayToString(entry.Value);
                }
            }

            return Page();
        }
    }
}
