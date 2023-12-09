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

        public async Task<IActionResult> OnGetVoice(string text)
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken });
            var client = api.client;
            RestRequest request = new RestRequest("ai/generate-voice");
            request.Method = Method.Get;
            request.AddParameter("text", text, true);
            request.AddParameter("voice", "-1", true);
            request.AddParameter("seed", "Aini", true);
            request.AddParameter("opus", "false", true);
            request.AddParameter("version", "v2", true);
            request.AddHeader("User-Agent", NovelAPI.AGENT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + api.keys.AccessToken);

            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
                return new FileContentResult(response.RawBytes ?? new byte[]{}, response.ContentType);
            
            return new StatusCodeResult((int)response.StatusCode);
        }
    }
}
