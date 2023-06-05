using Data;
using Infrastructure.Factory;

namespace Infrastructure.States
{
    public class StatisticState : IPayloadedState<PlayerStatistic[]>
    {
        private readonly IGameFactory _gameFactory;

        public StatisticState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter(PlayerStatistic[] statistics)
        {
            _gameFactory.ShowStatistics(statistics);
            _gameFactory.CreateHUD();
        }

        public void Exit()
        {
        }
    }
}