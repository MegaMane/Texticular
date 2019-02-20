using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texticular.Environment
{
    public class TV: Prop
    {
        private static List<string> channels = new List<string>
        {   
            "Whenever I feel like a sweaty slob, there is one assurance that gives me peace of mind. Deodorant. Just one wipe under each arm pit and I am good to go for days.Heck, I don't even need to shower for one whole week.That's how good this shit is.\n ",
            "Ever sit on a toilet and have the never ending wipe?  Well, those days are over. I've invented one wipes.  A new form of toilet paper that contains a solution where with just one wipe, you are fresh and clean. Done deal.\n ",
            "Well, I'm your Vita-vega-vittival girl. Are you tired? Run-down? Listless? Do you pop out at parties? Are you unpoopular? Well, are you? The answer to all your problems is in this little ol' bottle…Vitameatavegemin. That's it. Vitameatavegemin contains vitamins, meat, meg-e-tables, and vinerals. So why don't you join the thousands of happy , peppy people and get a great big bottle of Vita-veaty-vega-meany-minie-moe-amin. I'll tell you what you have to do. You have to take a whole tablesppon after every meal. It's so tasty, too: it's just like candy. So everybody get a bottle of…this stuff.\n ",
            "Static\n "
         };

        public string TurnOffResponse { get; set; } 

        public string Channel { get { return channels[currentChannel]; }  }

        int currentChannel = 0;

        bool isON = false;

        public TV(string locationKey, string name, string description, string keyValue="",  string examine="")
            :base(locationKey, name, description, keyValue, examine)
        {
            TurnOffResponse = "The TV flickers then goes black\n ";
            
            Commands["turn on"] = turnOn;
            Commands["power on"] = turnOn;

            Commands["turn off"] = turnOff;
            Commands["power off"] = turnOff;

            Commands["change channel"] = changeChannel;
        }

        void turnOn (GameController controller)
        {
            controller.InputResponse.Append("You turn on the TV...\n\n ");
            controller.InputResponse.Append( Channel );
            isON = true;
        }

        void turnOff(GameController controller)
        {
            controller.InputResponse.Append(TurnOffResponse + "\n");
            isON = false;
        }

        void changeChannel(GameController controller)
        {
            if (isON)
            {
                if (currentChannel == channels.Count - 1)
                {
                    currentChannel = 0;
                }

                else currentChannel += 1;

                controller.InputResponse.Append("Click...\n\n " + Channel);
            }
            else
            {
                controller.InputResponse.Append("You have to turn the TV on first!\n ");
            }
        }
    }
}
