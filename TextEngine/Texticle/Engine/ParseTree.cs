using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Engine
{
    public class ParseTree
    {
        public String Verb { get; set; } = "???";
        public String DirectObject { get; set; } = "???";
        public String DirectObjectKeyValue { get; set; } = "???";
        public String IndirectObject { get; set; } = "???";
        public String IndirectObjectKeyValue { get; set; } = "???";
        public String UnparsedInput { get; set; } = "???";

        public override string ToString()
        {

            return $"Raw Input: {UnparsedInput}" + "\n" +
                   $"Verb: {(Verb ?? "none")}" + "\n" +
                   $"Direct Object: {(DirectObject ?? "none")}" + "\n" +
                   $"Direct Object Key: {(DirectObjectKeyValue ?? "none")}" + "\n" +
                   $"Indirect Object: {(IndirectObject ?? "none")}" + "\n" +
                   $"Indirect Object Key: {(IndirectObjectKeyValue ?? "none")}" + "\n";
        }

    }
}
