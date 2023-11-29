using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;

namespace SampleWebApp.Pages.Utilities
{
    [Authorize]
    public class DataDecodeModel : PageModel
    {
        [BindProperty]
        public string? TextInput { get; set; } = "";

        [BindProperty]
        public string? TextOutput { get; set; } = "";

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken });



            return Page();
        }

    }
}
