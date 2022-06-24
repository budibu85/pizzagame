using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudiBu85.PizzaGame.Model
{
    public class Match
    {
        public Queue<int> Pizzas { get; set; }
        public Player PlayerA { get; set; } = new Player("A");
        public Player PlayerB { get; set; } = new Player("B");

        public Match()
        {
            Pizzas = new Queue<int>();
        }

        public void InitPizzas()
        {
            var number = new Random().Next(10, 30);

            for (int i = 1; i <= number; i++)
                Pizzas.Enqueue(i);
        }
    }
}
