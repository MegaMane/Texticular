using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEngine
{
    public partial class GameController
    {
       // Dictionary<string, InteractableObject> UseDictionary;


        void use(string[] parameters)
        {
            Player player = game.Player;
            Room currentRoom = player.PlayerLocation;


            foreach (string item in parameters)
            {
                var examineInventory = ItemsinInventory.Where(p => p.Name.ToLower() == item).ToList();

                if (examineInventory.Count > 0)
                {
                    foreach (StoryItem inventoryItem in examineInventory)
                    {
                        if (inventoryItem is Consumable)
                        {
                            Consumable consumableItem = (Consumable)inventoryItem;
                            InputResponse.AppendFormat($"{consumableItem.actions["use"]} \n");
                            player.Health += consumableItem.healthBoost;
                        }

                        else if (inventoryItem is ItemKey)
                        {
                            UseKey((ItemKey)inventoryItem, currentRoom);
                        }
                    }
                }


                else
                {
                    InputResponse.AppendFormat("There is no {0} here.\n", item);
                }
            }
        }


        public void UseKey(ItemKey key, Room currentRoom)
        {
            foreach(Exit door in currentRoom.Exits)
            {
                if (door.IsLocked && door.ExitKey.ID == key.ID)
                {
                    door.IsLocked = false;
                    InputResponse.AppendFormat($"You use the {key.Name} and the {door.Name} unlocks.");
                }
            }
        }
    }
}
