using net.novelai.api;

namespace SampleConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            NovelAPI? api = NovelAPI.NewNovelAiAPI();
            if (api != null)
            {
                string response = await api.GenerateAsync("Hello World!");
                Console.WriteLine(response);
            }
        }
    }
}
