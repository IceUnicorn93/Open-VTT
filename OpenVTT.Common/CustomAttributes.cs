using OpenVTT.Logging;
using System;

namespace OpenVTT.Common
{
    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Enum |
        AttributeTargets.Method | 
        AttributeTargets.Field | 
        AttributeTargets.Property, AllowMultiple = true)]
    internal class Documentation : Attribute
    {
        public string Description = "";
        public string Parameters = "";
        public string ReturnType = "";
        public string DataType = "";
        public string Name = "";
        public bool IsMethod = false;
        public bool IsField = false;
        public bool IsProperty = false;
        public bool IsStatic = false;

        public Documentation(string description)
        {
            Description = description;
        }

        public string GetDescription()
        {
            Logger.Log("Class: Documentation | GetDescription");

            return Description;
        }
    }
}
