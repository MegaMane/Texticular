using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Engine.Environment;

namespace Texticular.Engine.Events
{
    public class ItemLocationChangedEventArgs
    {
        public String CurrentLocation { get; set; }
        public String NewLocation { get; set; }
    }
}
