using Data;
using Infrastructure.Factory;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class RegistrationState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private GameObject _registrationWindow;
        private readonly IGameFactory _gameFactory;
        private GameObject _hud;

        public RegistrationState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _registrationWindow = _gameFactory.CreateRegistrationWindow();
            _registrationWindow.GetComponent<Registration>().RegistrationFinished += OnRegistrationFinished;
            _hud = _gameFactory.CreateHUD();
        }

        public void Exit()
        {
            Object.Destroy(_hud);
        }

        private void OnRegistrationFinished(PlayerData[] registeredPlayers)
        {
            _registrationWindow.GetComponent<Registration>().RegistrationFinished -= OnRegistrationFinished;
            _gameStateMachine.Enter<GameLoopState, PlayerData[]>(registeredPlayers);
        }
    }
}