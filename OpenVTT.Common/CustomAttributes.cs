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
        string Description;

        public Documentation(string description)
        {
            Description = description;
        }

        public string GetDescription() => Description;
    }
}
