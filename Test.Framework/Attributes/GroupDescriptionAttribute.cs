using System;
using System.ComponentModel;

namespace Test.Framework.Attributes
{
    public class GroupDescriptionAttribute : DescriptionAttribute
    {
        public GroupDescriptionAttribute(string description, string group)
            : base(description)
        {
            this.Group = group;
        }

        public string Group { get; private set; }
    }
}
