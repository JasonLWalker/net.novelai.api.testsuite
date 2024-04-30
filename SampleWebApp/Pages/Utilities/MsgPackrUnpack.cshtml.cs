using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;
using Newtonsoft.Json;
using static net.novelai.api.Structs;

namespace SampleWebApp.Pages.Utilities
{
    [Authorize]
    public class MsgPackrUnpackModel : PageModel
    {
        [BindProperty]
        public string? JsonText { get; set; } = "";

        [BindProperty]
        public string? PackedText { get; set; } = "";

        [BindProperty]
        public string? UnpackedJson { get; set; } = "";

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            byte[] data = Convert.FromBase64String(PackedText);

            var unpacker = new MsgPackrUnpack(new MsgUnpackerOptions()
            {
                //MapsAsObjects = true,
                UseRecords = false
            });
            var json = unpacker.Unpack(data, null);

            UnpackedJson = JsonConvert.SerializeObject(json ?? new {}, Formatting.Indented);
            return Page();
        }

    }
}