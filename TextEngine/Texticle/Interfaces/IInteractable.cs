using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;

namespace Texticle.Interfaces
{
    public interface IInteractable
    {
        Dictionary<string, Action<ParseTree>> Commands { get; set; }
    }
}
