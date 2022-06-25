using BudiBu85.PizzaGame.Model;
using BudiBu85.PizzaGame.Service;

namespace BudiBu85.PizzaGame
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IPlayService _play;

        public Worker(ILogger<Worker> logger, IPlayService play)
        {
            _logger = logger;
            _play = play;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine(@"__________.__                                                     
\______   \__|____________________     _________    _____   ____  
 |     ___/  \___   /\___   /\__  \   / ___\__  \  /     \_/ __ \ 
 |    |   |  |/    /  /    /  / __ \_/ /_/  > __ \|  Y Y  \  ___/ 
 |____|   |__/_____ \/_____ \(____  /\___  (____  /__|_|  /\___  >
                   \/      \/     \//_____/     \/      \/     \/ ");
            Console.WriteLine();
            Console.WriteLine(@"Regole del gioco
- I giocatori in gioco devono essere 2
- Il numero di pizze viene determinato randomicamente all'inizio del gioco e deve essere sempre maggiore di 10
- Ogni giocatore, durante il proprio turno, deve mangiare 1, 2 o 3 pizze
- Ogni giocatore non puó saltare il proprio turno
- Ogni giocatore non puó ripetere la scelta fatta in precedenza dall'avversario 
- Se un giocatore durante il proprio turno non ha mosse valide allora viene obbligato a saltare il proprio turno.");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine($"Scegli la modalità di gioco");
                Console.WriteLine($"1 - Simulato");
                Console.WriteLine($"2 - Manuale");

                var typeOfgame = 0;
                do
                {
                    Int32.TryParse(Console.ReadLine(), out typeOfgame);
                    if (typeOfgame != 1 && typeOfgame != 2)
                        Console.WriteLine($"Inserisci una scelta valida tra Simulato o Manuale");

                } while (typeOfgame != 1 && typeOfgame != 2);

                var match = _play.Init(typeOfgame);
                Console.WriteLine($"Sul tavolo ci sono: {match.Pizzas.Count} pizze");

                //finche un giocatore non mangia la pizza avvelanta gioco.
                while (!match.PlayerB.IsDeath && !match.PlayerA.IsDeath)
                {
                    //inizio la simulazione passando al metodo pizze,
                    //giocatore numero1,
                    //giocatore numero2,
                    //speficio se simulato o manuale
                    _play.Play(match.Pizzas, match.PlayerA, match.PlayerB, match.TOG);
                }

                Console.WriteLine();
                Console.WriteLine("Play Again!!");

                await Task.FromResult(0);
            }
        }       

    }
}