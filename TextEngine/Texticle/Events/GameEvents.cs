using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.GameEngine
{
    //public delegate void PlayerLocationChangedEventHandler (object sender, PlayerLocationChangedEventArgs args);

    public delegate void OnItemLocationChanged(object sender, ItemLocationChangedEventArgs args);
}
