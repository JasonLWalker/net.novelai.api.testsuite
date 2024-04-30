namespace SampleWebApp;

public class MsgPackrKeyCacheEntry : List<long>
{
    public long Bytes { get; set; }
    public string? String { get; set; }

}