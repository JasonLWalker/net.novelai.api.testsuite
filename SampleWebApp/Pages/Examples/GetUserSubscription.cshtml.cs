using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GetUserSubscriptionModel : PageModel
    {
        [BindProperty] public NaiSubscriptionResponse UserSubscription { get; set; } = new NaiSubscriptionResponse();

        public async Task<IActionResult> OnGet([FromRoute] string storyId)
        {
            string? encryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? accessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = encryptionKey, AccessToken = accessToken });
            UserSubscription = await api.GetUserSubscriptionAsync();
            return Page();
        }
    }
}
