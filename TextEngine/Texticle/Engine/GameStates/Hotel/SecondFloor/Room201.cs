using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Environment;

namespace Texticle.Engine.GameStates.Hotel.SecondFloor
{
    public class Room201 : Room, IGameState
    {
        public int TimesEntered { get; set; } = 0;
        public string UserInput { get; set; } = "";
        public GameController Controller;

        public Room201(string keyValue):base(keyValue)
        {

        }



        public void GetInput()
        {
            Console.Write("\n>> ");
            string userInput = Console.ReadLine();
            UserInput = userInput.ToLower().Trim();
        }

        public void OnEnter()
        {

            Look(null);
            Update(Controller.ElapsedTime.ElapsedMilliseconds);
            
        }

        public void OnExit()
        {

        }

        public void Render()
        {
            Console.WriteLine(GameLog.InputResponse.ToString());
        }

        public void Update(float elapsedTime)
        {
            throw new NotImplementedException();
            //parse input
                //if verb is go|| move and object is Movement direction
                    //then call go
                //else
                    //find objects
                       //if verb is go|| move
                            //call go
                       // els if verb is state transition command
                            //call transition function
                       // else 
                            //call command on object
                                //if indirect object is null pass direct object else pass indirect object
                            //you can't do that to this object
                    //object not found
            //I don't understand that command
                    


        }


        private string Go(string desiredDestination)
        {
            ActionResponse.Clear();


            string direction = "";
            Direction desiredDirecton;


            if (desiredDestination == null)
            {
                ActionResponse.Append("Go Where?\n");
            }

            else
            {
                Door destination;
                bool exitFound = Exits.TryGetValue(desiredDestination, out destination);

                try
                {
                    desiredDirecton = (Direction)Enum.Parse(typeof(Direction), direction);

                    if (Exits.ContainsKey(direction))
                    {
                        ActionResponse.Append("You can't move in that direction\n");
                    }

                    else if (Exits[direction].IsLocked)
                    {
                        ActionResponse.AppendFormat("{0} is locked, maybe if you had a key...\n", Exits[direction].Name);
                    }

                    else
                    {
                        
                        //move the player and transition the game state
                        //if (direction.ToLower() == "west")
                        //{
                        //    CurrentScene = SceneList.transitionBathroom;
                        //}

                        //if (direction.ToLower() == "east")
                        //{
                        //    CurrentScene = SceneList.transitionHallway;
                        //}
                        //player.PlayerLocation = Controller.Game.Rooms[Room.Exits[direction].DestinationKey];


                    }
                }

                catch (System.ArgumentException e)
                {
                    ActionResponse.AppendFormat("{0} is not a valid direction. Type Help for more.\n", direction);
                }

                

            }

            return ActionResponse.ToString();
        }
    }
}
