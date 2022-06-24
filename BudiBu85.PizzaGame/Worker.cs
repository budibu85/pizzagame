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
            var lastChoice = 0;

            while (!match.PlayerB.IsDeath && !match.PlayerA.IsDeath)
            {
                _play.Play(match.Pizzas, match.PlayerA,match.PlayerB,lastChoice);
            }
        }

       
    }
}