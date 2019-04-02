using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;

namespace Texticle.Engine
{
    //Itakeable storyItem = new Coins();
    //new TakeCommand(storyItem)

    /*
     * 
    class TakeCommand<T>:ICommand where T : GameObject,ITakeable {
            T Target;

            public TakeCommand(T objectType, string objectKey) {
                Target = GameObject.GetComponent<T>(objectKey);
            }

                public void Execute()
        {
            Target.Take();
        }

     *
     */


    public class TakeCommand:ICommand
    {
        public ITakeable Target { get; set; }
        public TakeCommand(ITakeable target)
        {
            Target = target;
        }

        public void Execute()
        {
            Target.Take();
        }
    }
}
