using System;

namespace backend_tcc.lib
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DescricaoEnumeradorAttribute : Attribute
    {
        public DescricaoEnumeradorAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}
