using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net.novelai.api;
using static net.novelai.api.Structs;

namespace SampleWebApp.Pages.Examples
{
    [Authorize]
    public class GenerateTextModel : PageModel
    {
        [BindProperty]
        public string? PromptText { get; set; } = "";

        [BindProperty]
        public string? GeneratedText { get; set; } = "";

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string? EncryptionKey = User.Claims.FirstOrDefault(c => c.Type == "Nai.EncryptionKey")?.Value?.ToString();
            string? AccessToken = User.Claims.FirstOrDefault(c => c.Type == "Nai.AccessToken")?.Value?.ToString();

            var opts = new NaiGenerateParams
            {
                model = "kayra-v1",
                bad_words_ids = [
                    [3],
                    [49356],
                    [1431],
                    [31715],
                    [34387],
                    [20765],
                    [30702],
                    [10691],
                    [49333],
                    [1266],
                    [19438],
                    [43145],
                    [26523],
                    [41471],
                    [2936],
                    [85, 85],
                    [49332],
                    [7286],
                    [1115]
                ],
                generate_until_sentence = true,
                logit_bias_exp = new BiasParams[] {
                    new BiasParams()
                    {
                        bias = -0.08,
                        ensure_sequence_finish = false,
                        generate_once = false,
                        sequence = new ushort[]{23}
                    },
                    new BiasParams()
                    {
                        bias = -0.08,
                        ensure_sequence_finish = false,
                        generate_once = false,
                        sequence = new ushort[]{21}
                    }
                },
                max_length = 40,
                min_length = 1,
                order = [ 3, 0, 5 ],
                phrase_rep_pen = "medium",
                prefix = "vanilla",
                repetition_penalty = 1,
                repetition_penalty_frequency = 0,
                repetition_penalty_presence = 0,
                repetition_penalty_range = 1024,
                repetition_penalty_whitelist = [
                    49256,
                    49264,
                    49231,
                    49230,
                    49287,
                    85,
                    49255,
                    49399,
                    49262,
                    336,
                    333,
                    432,
                    363,
                    468,
                    492,
                    745,
                    401,
                    426,
                    623,
                    794,
                    1096,
                    2919,
                    2072,
                    7379,
                    1259,
                    2110,
                    620,
                    526,
                    487,
                    16562,
                    603,
                    805,
                    761,
                    2681,
                    942,
                    8917,
                    653,
                    3513,
                    506,
                    5301,
                    562,
                    5010,
                    614,
                    10942,
                    539,
                    2976,
                    462,
                    5189,
                    567,
                    2032,
                    123,
                    124,
                    125,
                    126,
                    127,
                    128,
                    129,
                    130,
                    131,
                    132,
                    588,
                    803,
                    1040,
                    49209,
                    4,
                    5,
                    6,
                    7,
                    8,
                    9,
                    10,
                    11,
                    12
                ],
                return_full_text = false,
                tail_free_sampling = 0.941,
                temperature = 2.5,
                typical_p = 0.969,
                use_cache = false,
                use_string = false,
            };

            NovelAPI? api = NovelAPI.NewNovelAiAPI(new Structs.AuthConfig() { EncryptionKey = EncryptionKey, AccessToken = AccessToken }, opts);
            //api.currentParams = opts;
            
            // [You are Rardon][You are talking to Maru via online chat]{Maru greets you
            GeneratedText += PromptText ?? "";
            if (api != null && !string.IsNullOrWhiteSpace(GeneratedText))
            {
                int offset = Math.Max(GeneratedText.Length - 10000, 0);
                string prompt = GeneratedText.Substring(offset);
                GeneratedText += await api.GenerateAsync(prompt);
            }

            return Page();
        }

    }
}
