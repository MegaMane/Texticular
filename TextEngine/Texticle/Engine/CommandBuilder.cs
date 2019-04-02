using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;

namespace Texticle.Engine
{
    class CommandBuilder
    {
        private string _Verb;
        private GameObject _DirectObject;
        private GameObject _IndirectObject;

        private Dictionary<string, ICommand> _Commands;



        public CommandBuilder(string verb, GameObject directObject, GameObject indirectObject = null)
        {
            _Verb = verb;
            _DirectObject = directObject;
            _IndirectObject = indirectObject;




        }

        public ICommand GetCommand()
        {
            ICommand command = null;
            switch (_Verb)
            {
                case "take":
                    command = new TakeCommand(_DirectObject as ITakeable);
                    break;
                default:
                    break;
            }
            

            return command;

        }

    }
}
