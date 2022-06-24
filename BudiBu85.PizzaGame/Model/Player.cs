using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudiBu85.PizzaGame.Model
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public bool IsDeath { get; set; } = false;
    }
}
