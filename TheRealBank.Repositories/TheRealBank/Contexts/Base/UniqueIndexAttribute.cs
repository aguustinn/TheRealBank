using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRealBank.Contexts.Base
{
    public sealed class UniqueIndexAttribute : Attribute
    {
        public string[] Fields { get; }
        public UniqueIndexAttribute(params string[] fields)
        {
            Fields = fields;
        }
    }
}
