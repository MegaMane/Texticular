using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.Environment;
using Texticular.GameEngine;
using Texticular.GameStates;
using Texticular.UI;

namespace Texticular.GameStates
{
    class StorySequenceState: IGameState
    {
        public int TimesEntered { get; set; } = 0;
        public string UserInput;
        GameController Controller;
        GameScene ActiveScene;
        public Dictionary<Scene, GameScene> Scenes;

        public StorySequenceState(GameController controller)
        {

            Controller = controller;
            Scenes = new Dictionary<Scene, GameScene>();
            AddScenes();

        }

        public void OnEnter()
        {
            ActiveScene = Scenes[Controller.ActiveStoryScene];
            if(TimesEntered > 1)
            {
                //the game starts with the story so update shouldn't run until subsequent times the game state is entered
                Update(Controller.ElapsedTime.ElapsedMilliseconds);
            }
            
        }

        public void OnExit()
        {
            //code to cleanup state here
        }

        public void Render()
        {
            Controller.UI.DrawGameUI();
            GetInput();

        }

        public void Update(float elapsedTime)
        {

            GameController.InputResponse.Clear();
            
            //get the next page of the story
            if(ActiveScene.SceneText.Count > 0)
            {
               GameController.InputResponse.Append(ActiveScene.SceneText.Dequeue());
            }
            else
            {
                //the scene has come to an end so handle any work and change the game state
                ActiveScene.SceneAction(Controller);
            }
         

        }

        void GetInput()
        {

            Console.Write("Press Enter to continue...\n>>");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        #region create Game Scenes

        public void AddScenes()
        {

            GameScene intro = new GameScene("intro");
            intro.SceneText.Enqueue(@"You wake up with a pounding headache in a shabby looking hotel room "
                    + "surrounded by a bunch of empty cans.You've got a taste in your mouth like a dirty old "
                    + "rat crawled in and died in there. Disoriented, you roll out of the bed you woke up in, barely "
                    + "avoiding some questionable stains on the sheets, as you stumble to your feet sending cans flying "
                    + "like bowling pins in your wake.You bend over to take a closer look at the pile of crushed aluminum. "
                    + "You read one of the labels: \"Fast Eddie's Colon Cleanse: When in doubt flush it out!\"\"Side effects "
                    + "may include: Dizzines, vomiting, diarrhea, cold sweats, hallucinations, intense panic, paranoia, permanent "
                    + "tongue discoloration, mild brain damage, amnesia, bowel implosion, and occasionally hiccups\". The can has "
                    + "a purple iridescent sludge oozing out of it that's really similar to the shade of purple that your hands "
                    + "are. Come to think of it, you vaguely remember signing up for a test group that was supposed to try out "
                    + "a new health drink.Looks like your part time job as a barrista just wasn't paying the bills, nothing "
                    + "like easy money! The thing is you don't remember anything about going to a hotel last night, and you "
                    + "definitely don't remember anything about drinking a 24 pack of Fast Eddie's Colon Cleanse. Your stomach "
                    + "starts to feel a little uneasy, but never mind that, it's time to spend some of that hard earned cash! "
                    + "You reach into your wallet and realize in that moment that you don't even remember your name. You look at "
                    + "your license and focus your still hazy eyes and barely make out that it says...\n\n ");
            intro.SceneAction = delegate (GameController controller)
            {
                controller.ActiveChoice = Choice.PlayerName;
                controller.SetGameState("PlayerChoice");

            };

            GameScene introLetter = new GameScene("IntroLetter");
            introLetter.SceneText.Enqueue($"Dear <firstName>,\n\n Thank you so much for signing up to try out our exciting new drink! "
                                + "We hope you don't mind but we've taken the liberty of putting you up for the night "
                                + "in one of our sponsors hotels with a generous supply of Fast Eddie's to keep you company.\n\n "
                                );
            introLetter.SceneAction = delegate (GameController controller)
            {
                controller.SetGameState("Explore");

            };

            GameScene Bathroom201FirstVisit = new GameScene("Bathroom201FirstVisit");
            Bathroom201FirstVisit.SceneText.Enqueue(@"You crack open the door to the bathroom and it looks like it's seen better days. From the smell of it, it looks like "
                     + "someone beat you to it and narrowly escaped a hard fought battle with an eight pound burrito. The {sink} is old and yellowed. "
                     + "and caked with brown muck in the corners. The {mirror} is cracked and something is written on it red. You can't quite "
                     + "make it out. But you don't care...you've gotta take a shit! You rush to be the first in line to make a deposit in the "
                     + "porcelain bank {toilet}. But just as you are about to Drop it like it's hot you notice there is an an angry {Great Dane} "
                     + "guarding the toilet and he looks hungry! You quickly shut the door and somehow manage to not lose your shit (literally). "
                     + "Looks like you have to find somewhere else to go if you value your junk...and your life.");
            Bathroom201FirstVisit.SceneAction = delegate (GameController controller)
            {
                //put the player back in the previous room as the story suggests the player quickly shuts the door 
                //and leaves the bathroom
                Player player = GameObject.GetComponent<Player>("player");
                Room destination = GameObject.GetComponent<Room>("room201");

                controller.Game.Player.PlayerLocation = destination;
                controller.SetGameState("Explore");

            };



            GameScene Bathroom201GameOver = new GameScene("GameOverBathroom201");
            Bathroom201GameOver.SceneText.Enqueue(@"You blatantly ignore the fact that their is a vicious Great Dane in the bathroom. "
                     + "This brown baby is coming now! Unfortunately the dog does not ignore you and decides now would be a good time to rip your throat out."
                     );
            Bathroom201GameOver.SceneAction = delegate (GameController controller)
            {
                //Game Over
                controller.SetGameState("PlayerQuit");

            };

            Scenes[Scene.Intro] = intro;
            Scenes[Scene.IntroLetter] = introLetter;
            Scenes[Scene.Bathroom201FirstVisit] = Bathroom201FirstVisit;

            Scenes[Scene.GameOverBathroom201] = Bathroom201GameOver;




        }

        #endregion


    }
}
