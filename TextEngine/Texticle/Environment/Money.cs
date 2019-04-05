using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;
using Texticle.Actors;

namespace Texticle.Environment
{
    public class Money:GameObject, ITakeable, IViewable, IConsumable
    {
        //public string LocationKey { get; set; }
        int Value { get; set; }
        string ExamineResponse { get; set; }
        public string ConsumeText { get; set; }

        public Money()
            : base(name:"Money", description:"Some money. You can buy stuff with it!\n ")
        {
            Value = GetRandomInt(1, 20);
            ExamineResponse = $"{Value} dollars that seem to have a strong urge to jump into your wallet.\n ";
            ConsumeText = "You happily stuff the money in your wallet, after all it's what the money would have wanted.\n ";
        }

        public Money(string locationKey, int value, string name= "Money", string description= "Money", string keyValue = "")
            : base(name, description,keyValue)
        {
            LocationKey = LocationKey;
            Value = value;
            ConsumeText = "You happily stuff the money in your wallet, after all it's what the money would have wanted.\n ";
        }

        public string Take()
        {
            ActionResponse.Clear();

            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;

            if (this.LocationKey == player.LocationKey)
            {
                player.Money += this.Value;
                ActionResponse.Append(Consume());
            }

            else
            {

                ActionResponse.Append($"There is no {Name} here to take.\n ");
            }

            return ActionResponse.ToString();
        }

        public string Put(string target)
        {
            ActionResponse.Clear();

            target = target.ToLower();
            if(string.Equals(target,"pocket") || 
                string.Equals(target, "inventory") || 
                string.Equals(target, "wallet") ||
                string.Equals(target, "backpack"))
            {
                Take();
            }

            else
            {
                ActionResponse.Append("Try putting it in your wallet instead.\n ");
            }

            return ActionResponse.ToString();
        }

        public string Look()
        {
            throw new NotImplementedException();
        }

        public string Examine()
        {
            return ExamineResponse.ToString();
        }

        public string Consume()
        {

            this.LocationKey = null;
            GameObject.Consume(this.KeyValue);

            return ConsumeText;
        }
    }
}
