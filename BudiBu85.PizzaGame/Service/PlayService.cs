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

        /// <summary>
        /// Inizializzo il match generando la coda randomica
        /// </summary>
        /// <returns></returns>
        public Match Init(int tog)
        {
            var match = new Match() { TOG = (TypeOfGame)tog };
            match.InitPizzas();
            return match;
        }

        /// <summary>
        /// Metodo simulazione
        /// </summary>
        /// <param name="remainingPizzas"></param>
        /// <param name="current"></param>
        /// <param name="next"></param>
        /// <param name="lastChoice"></param>
        /// <returns></returns>
        public void Play(Queue<int> remainingPizzas, Player current, Player next, TypeOfGame tog, int lastChoice = 0)
        {

            Console.WriteLine($"Tocca al giocatore {current.Name}");

            int currentChoice = 0;
            //creo le possibili scelte dell'utente e rimuovo quelle non possibili
            var res = new List<int>() { 1, 2, 3 };
            res.RemoveAll(x => x > remainingPizzas.Count || x == lastChoice);

            if (res.Count == 1 && remainingPizzas.Count == res.FirstOrDefault())
            {
                Console.WriteLine($"Il giocatore {current.Name} ha perso perchè la sua unica scelta è mangiare la pizza avvelenata");
                current.IsDeath = true;
                return;
            }

            //salta il turno
            if (res.Count == 0)
            {
                Console.WriteLine($"Il giocatore {current.Name} è costretto a saltare il turno");
                Console.WriteLine($"Il giocatore {next.Name} ha perso perchè è rimasta una sola pizza");
                next.IsDeath = true;
                return;
            }           

            //controllo la scelta e finchè non è corretta resto in attesa 
            do
            {
                switch (tog)
                {
                    //se manuale faccio scegliere all'utente
                    case TypeOfGame.Manuale:
                        Int32.TryParse(Console.ReadLine(), out currentChoice);
                        if (!res.Contains(currentChoice))
                            Console.WriteLine($"Inserisci una scelta valida tra {string.Join(", ", res)}");
                        break;

                    //se simulato prendo randomicamente una scelta possibile tra quelle esistenti
                    case TypeOfGame.Simulato:
                        var index = new Random().Next(0, res.Count);
                        currentChoice = res[index];
                        break;
                    default:
                        return;
                }

            } while (!res.Contains(currentChoice));

            //mangia le pizze
            for (int i = 0; i < currentChoice; i++)
                remainingPizzas.Dequeue();

            //se pizze finite ho perso
            if (remainingPizzas.Count == 0)
            {
                Console.WriteLine($"Il giocatore {current.Name} ha perso perchè ha mangiato l'ultima pizza");
                current.IsDeath = true;
                return;
            }

            //aggiorno lo stato del gioco
            Console.WriteLine($"Il giocatore {current.Name} ha scelto {currentChoice} pizze - Sul tavolo ci sono: {remainingPizzas.Count} pizze");

            //ricorsivamente chiamo la funzione di gioco invertendo i giocatori
            Play(remainingPizzas, next, current, tog, currentChoice);

        }
    }

    public interface IPlayService
    {
        Match Init(int tog);

        void Play(Queue<int> remainingPizzas, Player current, Player next, TypeOfGame tog, int lastChoice = 0);

    }
}
