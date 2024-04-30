using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;
using Newtonsoft.Json.Linq;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GetShelfModel : PageModel
    {
        [BindProperty]
        public string? ShelfId { get; set; } = null;

        [BindProperty]
        public IEnumerable<JToken> Shelves { get; set; } = new JToken[]{};

        public IEnumerable<Structs.RemoteStoryMeta> Stories { get; set; } = new Structs.RemoteStoryMeta[]{};


        public async Task<IActionResult> OnGet([FromRoute] string? shelfId = null)
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            ShelfId = shelfId;

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken });
            Stories = await api.GetStories();
            Shelves = await api.GetUserShelf(shelfId);
            
            return Page();
        }
    }
}
