using OpenVTT.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenVTT.Session
{
    [Documentation("To use this object use var cs = new CustomSettings();", Name = "CustomSettings")]
    public class CustomSettings
    {
        [Documentation("What Script is this Settings used in", Name = "ScriptName", IsField = true, DataType = "string")]
        public string ScriptName;
        [Documentation("Optionally: for simple Data use this", Name = "SettingName", IsField = true, DataType = "string")]
        public string SettingName;
        [Documentation("Optionally: for simple Data use this", Name = "SettingValue", IsField = true, DataType = "string")]
        public string SettingValue;
        [Documentation("List of CustomObjects", Name = "ScriptObjects", IsField = true, DataType = "CustomObject")]
        public List<CustomObject> ScriptObjects = new List<CustomObject>();
    }

    [Documentation("To use this object use var co = new CustomObject();", Name = "CustomObject")]
    public class CustomObject
    {
        [Documentation("Name of the Object to store", Name = "ObjectName", IsField = true, DataType = "string")]
        public string ObjectName;
        [Documentation("List of CustomObjectData", Name = "ObjectData", IsField = true, DataType = "CustomObjectData")]
        public List<CustomObjectData> ObjectData = new List<CustomObjectData>();
    }

    [Documentation("To use this object use var cod = new CustomObjectData();", Name = "CustomObjectData")]
    public class CustomObjectData
    {
        [Documentation("Name of the Value to store", Name = "Name", IsField = true, DataType = "string")]
        public string Name;
        [Documentation("Value to store", Name = "Value", IsField = true, DataType = "string")]
        public string Value;

        [Documentation("Returns the Value casted (Works with System-Datatypes)", Name = "GetValue<T>", IsMethod = true, DataType = "T")]
        public T GetValue<T>()
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                var result = (T)converter.ConvertFromString(Value);
                return result;
            }
            catch
            {
                return default;
            }
        }
    }
}
