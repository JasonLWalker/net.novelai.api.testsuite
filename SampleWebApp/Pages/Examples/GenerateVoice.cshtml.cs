using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;
using RestSharp;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GenerateVoiceModel : PageModel
    {
        [BindProperty]
        public string PromptText { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetVoice(string text, int voice, string seed, string version)
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken });
            var response = await api.GenerateVoiceAsync(new Structs.NaiGenerateVoice()
            {
                text = text,
                voice = voice,
                seed = seed,
                opus = false,
                version = version
            });
            if (response.StatusCode == 200)
            {

                return new FileContentResult(response.output, response.ContentType);
            }

            return new StatusCodeResult(response.StatusCode);
        }
    }
}
