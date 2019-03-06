using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;

namespace Texticular.GameEngine
{
    public class Scene
    {

        public String SceneName { get; set; }
        public Queue<String> SceneText { get; set; }
        public Action<GameController> SceneAction { get; set; }

        public Scene(string sceneName)
        {
            SceneName = sceneName;
            SceneText = new Queue<string>();
        }

    }


        
}
