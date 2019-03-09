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
    class PlayerChoiceState: IGameState
    {

        public int TimesEntered { get; set; } = 0;
        public string UserInput="";
        GameController Controller;
        PlayerChoice ActiveChoice;
        public Dictionary<Choice, PlayerChoice> Choices;


        public PlayerChoiceState(GameController controller)
        {
            Controller = controller;
            Choices = new Dictionary<Choice, PlayerChoice>();
            AddChoices();

        }

        public void OnEnter()
        {
            ActiveChoice = Choices[Controller.ActiveChoice];
            Update(Controller.ElapsedTime.ElapsedMilliseconds);
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
            bool ChoiceMade = ActiveChoice.ChoicePrompt(Controller, UserInput);

            //the criteria for the choice have been met so handle any work and change the game state
            if (ChoiceMade)
            {
                ActiveChoice.ChoiceResult(Controller);
            }

            



        }

        void GetInput()
        {

            Console.Write("\n>> ");
            string userInput = Console.ReadLine();
            this.UserInput = userInput.ToLower().Trim();
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        #region create Player Choices

        public void AddChoices()
        {
            PlayerChoice askPlayerName = new PlayerChoice("PlayerName");

            askPlayerName.ChoicePrompt = delegate (GameController controller, string userInput)
            {
                Player player = controller.Game.Player;

                if (userInput != "")
                {
                    player.FirstName = userInput;
                    return true;
                }

                GameController.InputResponse.Append("What is your name");
                return false;

            };

            askPlayerName.ChoiceResult = delegate (GameController controller)
            {
                StorySequenceState StoryState = (StorySequenceState) controller.GameStates["StoryScene"];
                Player player = controller.Game.Player;
                GameScene scene = StoryState.Scenes[Scene.IntroLetter];
                string scenePage = scene.SceneText.Dequeue();
                scenePage = scenePage.Replace("<firstName>", player.FirstName);
                scene.SceneText.Enqueue(scenePage);
                controller.ActiveStoryScene = Scene.IntroLetter;
                controller.SetGameState("StoryScene");
            };

            Choices[Choice.PlayerName] = askPlayerName;

        }

        #endregion
    }
}
