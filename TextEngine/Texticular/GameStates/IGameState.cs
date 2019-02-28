using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.GameStates
{
    public interface IGameState
    {
        void Update(float elapsedTime);
        void Render();
        void OnEnter();
        void OnExit();
    }
}
