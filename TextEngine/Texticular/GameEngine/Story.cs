using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.GameEngine
{
    public class Story
    {

        public Dictionary<string, Scene> Scenes { get; } 
        public string CurrentScene { get; set; } = "playerControlled";



        public Story() {
            Scenes = new Dictionary<string, Scene>();

            #region intro

            Scene intro = new Scene("intro");

            intro.SceneText.Add(@"You wake up disoriented with a pounding headache in a shabby looking hotel room " 
                                +"surrounded by a bunch of empty cans.You've got a taste in your mouth like a dirty old "
                                +"rat crawled in and died in there.Disoriented, you roll out of the bed you woke up in, barely "
                                +"avoiding some questionable stains on the sheets, as you stumble to your feet sending cans flying "
                                +"like bowling pins in your wake.You bend over to take a closer look at the pile of crushed aluminum. "
                                +"You read one of the labels: \"Fast Eddie's Colon Cleanse: When in doubt flush it out!\"\"Side effects "
                                +"may include: Dizzines, vomiting, diarrhea, cold sweats, hallucinations, intense panic, paranoia, permanent "
                                +"tongue discoloration, mild brain damage, amnesia, bowel implosion, and occasionally hiccups\". The can has "
                                +"a purple iridescent sludge oozing out of it that's really similar to the shade of purple that your hands "
                                +"are. Come to think of it, you vaguely remember signing up for a test group that was supposed to try out "
                                +"a new health drink.Looks like your part time job as a barrista just wasn't paying the bills, nothing "
                                +"like easy money! The thing is you don't remember anything about going to a hotel last night, and you "
                                +"definitely don't remember anything about drinking a 24 pack of Fast Eddie's Colon Cleanse. Your stomach "
                                +"starts to feel a little uneasy, but never mind that, it's time to spend some of that hard earned cash! "
                                +"You reach into your wallet and realize in that moment that you don't even remember your name. You look at "
                                +"your license and focus your still hazy eyes and barely make out that it says...\n\n ");

            intro.SceneText.Add("Dear<Name>,\n\nThank you so much for signing up to try out our exciting new drink!"
                                +"We hope you don't mind but we've taken the liberty of putting you up for the night "
                                +"in one of our sponsors hotels with a generous supply of Fast Eddie's to keep you company.\n\n "
                                );

            intro.SceneAction = playIntro;

            void playIntro(GameController controller)
            {
                Player player = controller.Game.Player;
                Scene scene = controller.story.Scenes["intro"];

                GameController.InputResponse.Clear();
                GameController.InputResponse.Append(scene.SceneText[0]);
                GameController.InputResponse.Append("What is your name?\n\n ");
                while (player.FirstName == "")
                {
                    controller.Render();
                    controller.GetInput();
                    player.FirstName = controller.UserInput;
                }

                
                GameController.InputResponse.Clear();

                GameController.InputResponse.Append(scene.SceneText[1].Replace("<Name>", " " + player.FirstName));
                controller.Render();


            }
            #endregion

            Scenes["intro"] = intro;

        }



        public void PlayScene(GameController controller)
        {
            foreach (string scene in Scenes[CurrentScene].SceneText)
            {
                GameController.InputResponse.Clear();
                GameController.InputResponse.Append(scene);
                controller.Render();
                if (scene.Substring(0,1) == "?")
                {
                    controller.GetInput();
                }
                
            }


            CurrentScene = "playerControlled";
        }


    }



    public class Scene
    {

        public String SceneName { get; set; }
        public List<String> SceneText { get; set;  }
        public Action<GameController> SceneAction { get; set; }
        public int SceneIndex { get; set; } = 0;

        public Scene(String sceneName)
        {
            SceneName = sceneName;
            SceneText = new List<string>();
            
        }
    }
        
}
