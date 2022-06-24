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
            var match = _play.Init();

            Console.WriteLine($"Sul tavolo ci sono: {match.Pizzas.Count} pizze");

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

            //setto il tipo di gioco
            match.TOG = (TypeOfGame)typeOfgame;

            //finche un giocatore non mangia la pizza avvelanta gioco.
            while (!match.PlayerB.IsDeath && !match.PlayerA.IsDeath)
            {
                //inizio la simulazione passando al metodo pizze,
                //giocatore numero1,
                //giocatore numero2,
                //speficio se simulato o manuale
                _play.Play(match.Pizzas, match.PlayerA, match.PlayerB, match.TOG);
            }

            await Task.FromResult(0);
        }


        

    }
}