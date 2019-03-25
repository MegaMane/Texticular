using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.Engine.Events
{
    public delegate void PlayerLocationChangedEventHandler (object sender, PlayerLocationChangedEventArgs args);

    public delegate void ItemLocationChangedEventHandler(object sender, ItemLocationChangedEventArgs args);
}
