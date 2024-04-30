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

    /*
    public override object? Read()
    {
        var offset = Offset;
        var token = ReadByte(offset++);
        /*
        if (token >= 0x40 && token < 0x80)
        {
            /*
            let t = c[63 & e] || p.getStructures && R()[63 & e];
            return t ? (t.read || (t.read = M(t, 63 & e)),
                t.read()) : e
            * /

        }
        else 
        * /
        /*
        if (token == 0xC1)
        {
            Seek(offset);
            // Custom C1 handling
            if (_ext98Model != null)
            {
                int start;
                int length = Convert.ToInt32(Read());
                if (length > 0)
                {
                    start = _ext98Model.Position1;
                    _ext98Model.Position1 += length;
                    return _ext98Model.Strings[1].Substring(start, length);
                }
                else
                {
                    start = _ext98Model.Position0;
                    _ext98Model.Position0 -= length;
                    return _ext98Model.Strings[0].Substring(start, length);
                }

            }
            return C1;
        }
        else 
        * /
        if (token == 0xD4)
        {
            token = ReadByte(offset++);
            if (token == 0x72)
            {
                token = ReadByte(offset++);
                Seek(offset);
                return RecordDefinition(63 & token);
            }
        }
        return base.Read();
    }
    */
    /*
    protected object? Z(int e, int? t = null)
    {
        object? data = Read();
        MsgPackrStructure? structure = null;
        try
        {
            structure = Map((IEnumerable<object?>?)data); // .map((e => e.toString()))
        }
        catch
        {
            throw new ArgumentOutOfRangeException(nameof(structure), $"Unable to map structure: {e}:{structure}");
        }
            
        
        var r = e;
        //, r = e;
        if (t != 0)
        {
            e = e < 32 ? -(((t ?? 0) << 5) + e) : ((t ?? 0) << 5) + e;
            structure.HighByte = t;
        }
   
        object? i = null;
        if (CurrentStructures.ContainsKey(e))
        {
            i = CurrentStructures[e];
        }

        /*
        return i && (i.isShared || C) && ((c.restoreStructures || (c.restoreStructures = []))[e] = i),
        * /
        if (i == null)
            CurrentStructures[e] = structure;

        structure.AddReadHandler(CreateStructureReader(structure, r));
        /*
        c[e] = n,
        n.read = M(n, r),
        n.read()
        * /
        return structure.Read?.Invoke();
    }
    */
    /*
    protected override object? RecordDefinition(int id)
    {
        var origOffset = Offset; // save offset in case we need to restore;
        var structure = Read();

        return base.RecordDefinition(id);
    }
    */
}