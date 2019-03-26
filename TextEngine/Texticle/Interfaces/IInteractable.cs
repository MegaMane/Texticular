using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Interfaces
{
    public interface IInteractable
    {
        Dictionary<string, Action<string>> Commands { get; set; }
    }
}
