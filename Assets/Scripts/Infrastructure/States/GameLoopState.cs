using Data;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameLoopState : IPayloadedState<PlayerData[]>
    {
        private readonly IGameFactory _gameFactory;
        private readonly GameStateMachine _stateMachine;
        private GameObject _hud;

        public GameLoopState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter(PlayerData[] players)
        {
            _hud = _gameFactory.CreateHUD();
            _gameFactory.CreateGameBoard(_stateMachine, players);
        }

        public void Exit()
        {
            Object.Destroy(_hud);
        }
    }
}