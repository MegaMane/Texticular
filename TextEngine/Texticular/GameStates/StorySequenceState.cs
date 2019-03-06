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
        public Dictionary<string, Scene> Scenes;
        public int TimesEntered { get; set; } = 0;
        public string UserInput;
        GameController Controller;
        Scene ActiveScene;

        //UI Stuff
        private Texticular.UI.Buffer mainBuffer;
        private UserInterface ui;
        private Narrative narrative;

        public StorySequenceState(GameController controller)
        {
            Controller = controller;
            Scenes = new Dictionary<string, Scene>();
            AddScenes();

            //UI Stuff
            Terminal.Init(110, 60, "Busted Ass Text Adventure (Texticular)", 7, 9);
            GameStatistics testStats = new GameStatistics();
            this.ui = new UserInterface(testStats);

            mainBuffer = Terminal.CreateBuffer(80, 41);
            Terminal.SetCurrentConsoleFontEx(8, 10);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
        }

        public void OnEnter()
        {
            ActiveScene = Scenes[Controller.ActiveStoryScene];
            GameController.InputResponse.Append(ActiveScene.SceneText.Dequeue());
            Render();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {

            Console.Clear();
            ui.DrawGameUI(Controller);


            mainBuffer = Terminal.CreateBuffer(80, 41);
            narrative = new Narrative(mainBuffer);
            mainBuffer.DrawFrameLeft(0, 0, 80, 41, ConsoleColor.DarkGray);
            narrative.Write(GameController.InputResponse.ToString(), fg: ConsoleColor.DarkGreen);
            mainBuffer.Blit(0, 2);
            Console.SetCursorPosition(0, 45);

        }

        public void Update(float elapsedTime)
        {
            GetInput();
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

            Console.Write("Press Enter to continue...\n >>");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }


        public void AddScenes()
        {

            Scene intro = new Scene("intro");
            intro.SceneText.Enqueue(@"You wake up disoriented with a pounding headache in a shabby looking hotel room "
                    + "surrounded by a bunch of empty cans.You've got a taste in your mouth like a dirty old "
                    + "rat crawled in and died in there.Disoriented, you roll out of the bed you woke up in, barely "
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
                controller.ActiveChoice = "PlayerName";
                controller.SetGameState("PlayerChoice");

            };

            Scene introLetter = new Scene("intro:Letter");
            introLetter.SceneText.Enqueue($"Dear <firstName>,\n\nThank you so much for signing up to try out our exciting new drink! "
                                + "We hope you don't mind but we've taken the liberty of putting you up for the night "
                                + "in one of our sponsors hotels with a generous supply of Fast Eddie's to keep you company.\n\n "
                                );
            introLetter.SceneAction = delegate (GameController controller)
            {
                controller.SetGameState("Explore");

            };


            Scenes["intro"] = intro;
            Scenes["intro:Letter"] = introLetter;
        }

    }
}
