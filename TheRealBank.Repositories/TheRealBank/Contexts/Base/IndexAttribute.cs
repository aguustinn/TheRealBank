using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class IndexAttribute : Attribute
{
    public string[] Fields { get; }
    public IndexAttribute(params string[] fields)
    {
        Fields = fields;
    }
}
