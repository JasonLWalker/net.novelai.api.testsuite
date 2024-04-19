using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GetStoryModel : PageModel
    {
        [BindProperty] 
        public string StoryId { get; set; } = "";
        [BindProperty]
        public Structs.StoryMeta? Story { get; set; } 

        public async Task<IActionResult> OnGet([FromRoute] string storyId)
        {
            string? encryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? accessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = encryptionKey, AccessToken = accessToken });
            Story = await api.GetStory(StoryId = storyId);
            return Page();
        }
    }
}
