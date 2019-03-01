using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.GameStates
{
    public class StateStack
    {
        Dictionary<String, IGameState> mStates = new Dictionary<String, IGameState>();
        Stack<IGameState> mStack = new Stack<IGameState>();

        public void Update(float elapsedTime)
        {
            IGameState top = mStack.Peek();
            top.Update(elapsedTime);
        }

        public void Render()
        {
            IGameState top = mStack.Peek();
            top.Render();
        }

        public void Push(String name)
        {
            IGameState state = mStates[name];
            mStack.Push(state);
        }

        public IGameState Pop()
        {
            return mStack.Pop();
        }
    }
}
