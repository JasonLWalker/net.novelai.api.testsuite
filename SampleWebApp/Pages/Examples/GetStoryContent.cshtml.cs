using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;
using net.novelai.api.msgpackr;
using Newtonsoft.Json.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GetStoryContentModel : PageModel
    {
        [BindProperty] 
        public string? StoryId { get; set; } = null;

        [BindProperty]
        public JToken? StoryContent { get; set; } = null;

        [BindProperty]
        public IEnumerable<Structs.RemoteStoryMeta> Stories { get; set; } = new Structs.RemoteStoryMeta[]{};

        public async Task<IActionResult> OnGet([FromRoute] string? storyId = null)
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();
            StoryId = storyId;

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken });
            if (string.IsNullOrWhiteSpace(StoryId))
            {
                Stories = await api.GetStories();
            }
            else
            {
                StoryContent = await api.GetStoryContent(storyId);
            }
            return Page();
        }
    }
}