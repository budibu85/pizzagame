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

            _logger.LogInformation($"Sul tavolo ci sono: {match.Pizzas.Count} pizze");

            //finche un giocatore non mangia la pizza avvelanta gioco.
            while (!match.PlayerB.IsDeath && !match.PlayerA.IsDeath)
            {
                //inizio la simulazione passando al metodo pizze,
                //giocatore numero1,
                //giocatore numero2,
                _play.Play(match.Pizzas, match.PlayerA, match.PlayerB);
            }

            await Task.FromResult(0);
        }


    }
}