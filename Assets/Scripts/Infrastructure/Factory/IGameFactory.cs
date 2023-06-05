using Data;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateNotification(string description);
        GameObject CreateRegistrationWindow();
        GameObject CreateHUD();
        GameObject CreatePlayerFigure(PlayerData player);
        void CreateGameBoard(GameStateMachine stateMachine, PlayerData[] players);
        GameObject CreateDice();
        GameObject CreatePlate();
        void ShowStatistics(PlayerStatistic[] statistics);
    }
}