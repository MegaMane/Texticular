﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticular.GameEngine;

namespace Texticular.Environment
{
    public class Container : StoryItem
    {
        public List<StoryItem> Items;
        public bool IsOpen { get; set; } = false;

        public Container(string locationKey, string name, string description, string keyValue = "", string examine = "", Container container = null)
            : base(name, description, locationKey, isPortable: false, examine: examine, weight: 99, keyValue: keyValue)
        {
            Items = new List<StoryItem>();

            Commands["open"] = openContainer;

            Commands["close"] = closeContainer;
            Commands["shut"] = closeContainer;
        }

        void openContainer(GameController controller)
        {

            IsOpen = true;
            controller.InputResponse.Append($"You open the {Description} and look inside.\n\n ");
            foreach (StoryItem item in Items)
            {
                controller.InputResponse.Append($"{item.Name}:{item.Description}\n ");
            }
            

        }


        void closeContainer(GameController controller)
        {
            IsOpen = false;
            controller.InputResponse.Append($"You shut the {Description}\n ");
        }




    }

}
