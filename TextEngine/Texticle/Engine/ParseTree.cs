using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Engine
{
    public class ParseTree
    {
        public String Verb { get; set; } = null;
        public String DirectObject { get; set; } = null;
        public String DirectObjectKeyValue { get; set; } = null;
        public String IndirectObject { get; set; } = null;
        public String IndirectObjectKeyValue { get; set; } = null;
        public String UnparsedInput { get; set; } = null;

    }
}
