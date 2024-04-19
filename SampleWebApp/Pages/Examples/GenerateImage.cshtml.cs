using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using net.novelai.api;
using Newtonsoft.Json;
using RestSharp;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GenerateImageModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostImage(NaiImageGenerationRequest imgModel)
        {
            string? encryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? accessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = encryptionKey, AccessToken = accessToken }, urlEndpoint: Structs.IMAGE_ENDPOINT);
            var response = await api.GenerateImageArchiveAsync(imgModel);

            if (response.StatusCode == 200)
            {
                byte[] imageData = NovelAPI.ExtractFileFromByteArchive(response.output, "image_0.png");
                return new FileContentResult(imageData, "image/png");
            }

            return new StatusCodeResult(400);
        }

    }
}
