using Data;
using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string ErrorWindowPath = "Prefabs/UI/Notification window";
        private const string RegistrationPath = "Prefabs/UI/Registration";
        private const string StatisticsPath = "Prefabs/UI/Statistics";
        private const string HudPath = "Prefabs/UI/Hud";
        private const string FigurePath = "Prefabs/Player figure";
        private const string GameBoardPath = "Prefabs/Game board";
        private const string DicePath = "Prefabs/Dice";
        private const string PlatePath = "Prefabs/Plate";


        public GameObject CreateNotification(string description)
        {
            var errorWindow = Object.Instantiate(Resources.Load<GameObject>(ErrorWindowPath));
            errorWindow.GetComponent<NotificationWindow>().Construct(description);
            return errorWindow;
        }

        public GameObject CreateRegistrationWindow()
        {
            var registrationWindow = Object.Instantiate(Resources.Load<GameObject>(RegistrationPath));
            registrationWindow.GetComponent<Registration>().Construct(this);
            return registrationWindow;
        }

        public GameObject CreateHUD() => Object.Instantiate(Resources.Load<GameObject>(HudPath));


        public GameObject CreatePlayerFigure(PlayerData player)
        {
            var figure = Object.Instantiate(Resources.Load<GameObject>(FigurePath));
            var figureRenderer = figure.GetComponent<MeshRenderer>();
            var sample = new Material(figureRenderer.material);
            sample.color = player.Color;
            figureRenderer.materials = new[] {sample, sample};
            figure.GetComponent<PlayerFigure>().Construct(player);
            return figure;
        }

        public void CreateGameBoard(GameStateMachine stateMachine, PlayerData[] players)
        {
            Object.Instantiate(Resources.Load<GameObject>(GameBoardPath)).GetComponent<GameBoard>()
                .Construct(stateMachine, this, players);
        }

        public GameObject CreateDice()
        {
            return Object.Instantiate(Resources.Load<GameObject>(DicePath));
        }

        public GameObject CreatePlate()
        {
            return Object.Instantiate(Resources.Load<GameObject>(PlatePath), new Vector3(0, 2, 0.5f),
                Quaternion.identity);
        }

        public void ShowStatistics(PlayerStatistic[] statistics)
        {
            Object.Instantiate(Resources.Load<GameObject>(StatisticsPath)).GetComponent<StatisticView>().Construct(statistics);
        }
    }
}