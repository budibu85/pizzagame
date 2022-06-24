using BudiBu85.PizzaGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudiBu85.PizzaGame.Service
{
    public class PlayService : IPlayService
    {


        public Match Init()
        {
            var match = new Match();
            match.InitPizzas();
            return match;
        }

        public int Play(Queue<int> m, Player current, Player next, int lastChoice)
        {

            Console.WriteLine($"Tocca al giocatore {current.Name}");

            int number = 0;
            var res = new List<int>() { 1, 2, 3 };
            res.RemoveAll(x => x > m.Count || x == lastChoice);

            //salta il turno
            if (res.Count == 0)
            {
                Console.WriteLine($"Il giocatore {current.Name} è costretto a saltare il turno");
                Console.WriteLine($"Il giocatore {next.Name} ha perso perchè è rimasta una sola pizza");
                next.IsDeath = true;
                return number;
            }

            //controllo la scelta
            do
            {
                Int32.TryParse(Console.ReadLine(), out number);
                if (!res.Contains(number))
                    Console.WriteLine($"Inserisci una scelta valida tra {string.Join(", ", res)}");

            } while (!res.Contains(number));

            //mangia le pizze
            for (int i = 0; i < number; i++)
                m.Dequeue();

            //se pizze finite ho perso
            if (m.Count == 0)
            {
                Console.WriteLine($"Il giocatore {current.Name} ha perso perchè ha mangiato l'ultima pizza");
                current.IsDeath = true;
                return 0;
            }

            //aggiorno lo stato del gioco
            Console.WriteLine($"Il giocatore {current.Name} ha scelto {number} pizze - Sul tavolo ci sono: {m.Count} pizze");


            return Play(m, next, current, number);

        }
    }

    public interface IPlayService
    {
        Match Init();


        int Play(Queue<int> pizzas, Player current , Player next ,int lastChoice);

    }
}
