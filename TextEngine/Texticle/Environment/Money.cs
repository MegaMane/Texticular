using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;
using Texticle.Actors;

namespace Texticle.Environment
{
    public class Money:GameObject, ITakeable, IExaminable, IConsumable
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

        public void Take()
        {
            Player player = GameObject.GetComponent<Player>("player");
            Room currentLocation = player.PlayerLocation;

            if (this.LocationKey == player.LocationKey)
            {
                player.Money += this.Value;
                Consume();
            }

            else
            {

                GameLog.InputResponse.AppendFormat($"There is no {Name} here to take.\n ");
            }
        }

        public void Put(string target)
        {
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
                GameLog.Append("Try putting it in your wallet instead.\n ");
            }

        }

        public void Look()
        {
            GameLog.Append(Description);
        }

        public void Examine()
        {
            GameLog.Append(ExamineResponse);
        }

        public void Consume()
        {
            GameLog.Append(ConsumeText);
            this.LocationKey = null;
            GameObject.Consume(this.KeyValue);
        }
    }
}
