using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;

namespace Texticular.GameEngine
{
    public class LocationChangedEventArgs:EventArgs
    {
        public Room CurrentLocation { get; set; }
        public Room NewLocation { get; set; }
    }
}
