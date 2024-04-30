using System.Reflection.PortableExecutable;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace SampleWebApp;

public class NovelAiMsgPackrReader : MsgPackrUnpack
{
    public NovelAiMsgPackrReader(MsgUnpackerOptions? options = null) : base(options)
    {
        AddExtension<Ext20>();
        AddExtension<Ext30>();
        AddExtension<Ext31>();
        AddExtension<Ext40>();
        AddExtension<Ext41>();
        AddExtension<Ext42>();
    }

    private class Ext20 : MsgPackrExtension
    {
        public Ext20() : base(20)
        {
            AddReadHandler((object?[]? args = null) =>
            {
                return args?.Length == 1 && args[0] is JToken ? args[0] : null;
            });
        }
    }

    private class Ext30 : MsgPackrExtension
    {
        public Ext30() : base(30)
        {
            AddReadHandler((object[]? args = null) =>
            {
                return args?.Length == 1 && args[0] is JToken ? args[0] : null;
            });
        }
    }

    private class Ext31 : MsgPackrExtension { public Ext31() : base(31) => AddUnpackHandler((byte[]? bytes) => null); }
    private class Ext40 : MsgPackrExtension { public Ext40() : base(40) => AddUnpackHandler((byte[]? bytes) => null); }
    private class Ext41 : MsgPackrExtension { public Ext41() : base(41) => AddUnpackHandler((byte[]? bytes) => null); }
    private class Ext42 : MsgPackrExtension { public Ext42() : base(42) => AddUnpackHandler((byte[]? bytes) => null); }
}