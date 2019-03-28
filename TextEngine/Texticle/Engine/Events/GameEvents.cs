using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Engine
{
    //public delegate void PlayerLocationChangedEventHandler (object sender, PlayerLocationChangedEventArgs args);

    public delegate void OnItemLocationChanged(object sender, ItemLocationChangedEventArgs args);
    public delegate void OnPlayerLocationChanged(object sender, PlayerLocationChangedEventArgs args);
}
