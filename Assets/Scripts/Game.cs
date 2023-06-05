using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;

public class Game : MonoBehaviour
{
    public void Awake()
    {
        var gameStateMachine = new GameStateMachine(new AllServices());
        gameStateMachine.Enter<BootstrapState>();
    }
}
