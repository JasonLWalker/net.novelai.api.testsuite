using System.Text;
using Newtonsoft.Json.Linq;

namespace SampleWebApp;

public delegate JObject? MsgPackrStructureReadCallback(object?[]? args = null);

public class MsgPackrStructure
{
    protected MsgPackrUnpack? Reader { get; private set; }
    public IEnumerable<string?>? Keys { get; private set; }
    public int? HighByte { get; set; }
    public MsgPackrStructureReadCallback? Read { get; protected set; }

    public void AddReadHandler(MsgPackrStructureReadCallback? callback)
    {
        Read = callback;
    }

    public JObject? GenericStructureReadCallback(object?[]? args = null)
    {
        /*
    	function readObject() {
           	// This initial function is quick to instantiate, but runs slower. After several iterations pay the cost to build the faster function
           	if (readObject.count++ > inlineObjectReadThreshold) {
           		let readObject = structure.read = (new Function('r', 'return function(){return ' + (currentUnpackr.freezeData ? 'Object.freeze' : '') +
           			'({' + structure.map(key => key === '__proto__' ? '__proto_:r()' : validName.test(key) ? key + ':r()' : ('[' + JSON.stringify(key) + ']:r()')).join(',') + '})}'))(read)
           		if (structure.highByte === 0)
           			structure.read = createSecondByteReader(firstId, structure.read)
           		return readObject() // second byte is already read, if there is one so immediately read object
           	}
           	let object = {}
           	for (let i = 0, l = structure.length; i < l; i++) {
           		let key = structure[i]
           		if (key === '__proto__')
           			key = '__proto_'
           		object[key] = read()
           	}
           	if (currentUnpackr.freezeData)
           		return Object.freeze(object);
           	return object
           }
           readObject.count = 0
           return readObject
        */
        var json = new StringBuilder("{");
        int count = 0;
        if (Keys != null)
        {
            foreach (string? key in Keys)
            {
                if (key != null)
                {
                    if (count++ > 0)
                        json.Append(",");
                    json.Append($"\"{key}\":null");
                }
            }
        }
        json.Append("}");
        var obj = JObject.Parse(json.ToString());
        if (Keys != null)
        {
            foreach (string? key in Keys)
            {
                if (key != null)
                {
                    //if (key == "__proto__")
                    //    key = "__proto_";

                    var value = Reader?.Read();
                    if (value != null)
                    {
                        obj[key] = Reader.GetJToken(value);
                    }
                }
            }
        }

        // Todo: Implement Second Byte Reader
        /*
        if (structure.highByte === 0) {
           	return createSecondByteReader(firstId, readObject)
        }
        */
        return obj;
    }

    public static T GetNewInstance<T>(MsgPackrUnpack reader, IEnumerable<string?>? keys) where T : MsgPackrStructure, new()
    {
        return new T
        {
            Reader = reader,
            Keys = keys
        };
    }

}