using Infrastructure.Factory;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly bool _isLocalBuild;
        private readonly AllServices _allServices;

        public BootstrapState(GameStateMachine stateMachine, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _allServices = allServices;
            RegisterServices();
        }

        public void Enter()
        {
            _stateMachine.Enter<RegistrationState>();
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            
        }

        private void RegisterServices()
        {
            _allServices.RegisterSingle<IGameFactory>(new GameFactory());
        }
    }
}