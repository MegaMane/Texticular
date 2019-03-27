using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Texticle.Events
{
    public class ItemLocationChangedEventArgs
    {
        public String CurrentLocation { get; set; }
        public String NewLocation { get; set; }
    }
}
