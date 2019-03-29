using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texticle.Engine;

namespace Texticle.Environment
{
    public class VendingMachine : GameObject
    {
        public Dictionary<string, Tuple<StoryItem,int>> Items { get; set; }

        private int _moneyInserted;
        public int MoneyInserted { get { return _moneyInserted; } set { _moneyInserted = value; HasMoney = true; } }
        public bool HasMoney { get; set; }

        public string CurrentSelection { get; set; }


        //public VendingMachine(string name, string description, string locationKey, string keyValue, int timeVisited)
        //    : base(name, description, examineResponse: "", LocationKey: locationKey, KeyValue: keyValue)
        //{
        //    TimesVisited = timeVisited;
        //    Commands["look"] = look;
        //    Commands["examine"] = look;
        //    Items = new Dictionary<string, Tuple<StoryItem, int>>();
        //}

        //public void Vend(string currentSelection)
        //{
        //    if(HasMoney)
        //    {
        //        Player player = GameObject.GetComponent<Player>("player");
        //        CurrentSelection = currentSelection;
        //        Tuple<StoryItem, int> selectedItem;
        //        bool selectedItemExists = Items.TryGetValue(CurrentSelection, out selectedItem);
        //        if (selectedItemExists)
        //        {
        //            StoryItem vendedItem = selectedItem.Item1;
        //            int itemCost = selectedItem.Item2;

        //            if (itemCost <= MoneyInserted)
        //            {
        //                Items.Remove(CurrentSelection);
        //                GetChange(itemCost, player);
        //                GameController.InputResponse.Append($"With a thud the {vendedItem} tumbles out into your eager little hands.");
        //                player.BackPack.AddItem(vendedItem);
        //            }

        //            else
        //            {
        //                GameController.InputResponse.Append($"{selectedItem} costs {itemCost}. You don't have enough money for that!");
        //            }

        //        }

        //        else
        //        {
        //            GameController.InputResponse.Append("Invalid Selection");
        //        }
                
        //    }

        //    GameController.InputResponse.Append("No money inserted.");

        //}

        //public void CoinReturn(Player player)
        //{
        //    player.Money += MoneyInserted;
        //    MoneyInserted = 0;
        //    HasMoney = false;
        //}

        //public void GetChange(int itemCost, Player player)
        //{
        //    player.Money += MoneyInserted - itemCost;
        //    MoneyInserted = 0;
        //    HasMoney = false;
        //}

        //public void InsertMoney(int money)
        //{
        //    MoneyInserted += money;
        //}

        //public void ViewSelection()
        //{
        //    GameController.InputResponse.Append("-------------------------------------------------------- \n");
        //    GameController.InputResponse.Append("                      Vending Machine                    \n");
        //    GameController.InputResponse.Append("-------------------------------------------------------- \n\n");

        //    foreach (KeyValuePair<string, Tuple<StoryItem, int>> itemSelection in Items)
        //    {
        //        StoryItem item = itemSelection.Value.Item1;
        //        int itemCost = itemSelection.Value.Item2;
        //        GameController.InputResponse.Append($"{itemSelection.Key}: {item.Name} {itemCost}\n");
        //    }
        //}

        //public override string ToString()
        //{
        //    return base.ToString() + $"TimesVisited: {TimesVisited}\n\n";
        //}

        //void look(ParseTree tokens)
        //{
        //    VendingMachine machine = this;
        //    //Game game = controller.Game;

        //    //location description
        //    GameController.InputResponse.AppendFormat(machine.Description);

        //}
    }
}
