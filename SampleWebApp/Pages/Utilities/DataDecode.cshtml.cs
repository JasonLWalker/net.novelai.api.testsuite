using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using net.novelai.api;
using net.novelai.api.msgpackr;
using Newtonsoft.Json;

namespace SampleWebApp.Pages.Utilities
{
    [Authorize]
    public class DataDecodeModel : PageModel
    {
        [BindProperty]
        public string? TextInput { get; set; } = "";

        [BindProperty]
        public string? TextOutput { get; set; } = "";

        [BindProperty]
        public string MetaKey { get; set; } = "";

        [BindProperty]
        public IEnumerable<SelectListItem> Keys { get; set; }


        public void OnGet()
        {
            string? encryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? accessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = encryptionKey, AccessToken = accessToken });
            Keys = api.keys.keystore.Select(o => new SelectListItem(o.Key, o.Key));
        }

        public async Task<IActionResult> OnPost()
        {
            string? encryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? accessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = encryptionKey, AccessToken = accessToken });
            Keys = api.keys.keystore.Select(o => new SelectListItem(o.Key, o.Key));

            TextOutput = api.DecodeData(MetaKey, TextInput);

            if (TextOutput == null)
            {
                byte[] data = Convert.FromBase64String(TextInput);

                var unpacker = new NovelAiMsgUnpacker(new MsgUnpackerOptions()
                {
                    //MapsAsObjects = true,
                    UseRecords = false
                });
                var json = unpacker.Unpack(data, null);

                TextOutput = JsonConvert.SerializeObject(json ?? new { }, Formatting.Indented);

            }

            return Page();
        }

    }
}
