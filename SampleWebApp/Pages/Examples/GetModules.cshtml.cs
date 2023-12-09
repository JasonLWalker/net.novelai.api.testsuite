using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GetModulesModel : PageModel
    {
        [BindProperty]
        public IEnumerable<string> Modules { get; set; } = new string[]{};

        public async Task<IActionResult> OnGet()
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken });
            Modules = await api.GetModules();
            return Page();
        }
    }
}
