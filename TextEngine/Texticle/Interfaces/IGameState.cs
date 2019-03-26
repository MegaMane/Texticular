using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticle.Interfaces.GameStates
{
    public interface IGameState
    {
        int TimesEntered { get; set; }
        void Update(float elapsedTime);
        void Render();
        void OnEnter();
        void OnExit();
        void GetInput();

    }
}
