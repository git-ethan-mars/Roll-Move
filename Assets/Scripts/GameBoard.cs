using System.Linq;
using Data;
using Infrastructure.Factory;
using Infrastructure.States;
using UI;
using UnityEngine;
using PlayerData = Data.PlayerData;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Cell[] cells;
    private GameStateMachine _gameStateMachine;
    private IGameFactory _gameFactory;
    private ITurnOrder _order;
    private PlayerData _currentPlayer;
    private Dice _dice;
    private GameObject _plate;
    private GameObject _previousNotification;
    private int _winnerNumber;
    private PlayerData[] _players;
    private const int PenaltyValue = 3;

    public void Construct(GameStateMachine stateMachine, IGameFactory gameFactory, PlayerData[] players)
    {
        _gameStateMachine = stateMachine;
        _gameFactory = gameFactory;
        _order = new TurnOrder(players);
        var validator = new FigurePositionValidator(cells);
        foreach (var player in players)
        {
            var figure = CreateGameFigures(player);
            player.Figure = figure;
        }

        validator.InitializeStartPosition(players);
        validator.CorrectMoveCompleted += OnCorrectMoveCompleted;
        _plate = _gameFactory.CreatePlate();
        _plate.SetActive(false);
        _dice = _gameFactory.CreateDice().GetComponent<Dice>();
        _dice.gameObject.SetActive(false);
        _players = players;
        StartGame();
    }

    private void StartGame() => NextMove();

    private PlayerFigure CreateGameFigures(PlayerData playerData)
    {
        return _gameFactory.CreatePlayerFigure(playerData).GetComponent<PlayerFigure>();
    }

    private void NextMove()
    {
        _currentPlayer = _order.GetNextPlayer();
        _gameFactory.CreateNotification($"{_currentPlayer.Name}, бросайте кубик").GetComponent<NotificationWindow>()
            .WindowClosed += OnWindowClosed;
    }

    private void OnWindowClosed(NotificationWindow notificationWindow)
    {
        notificationWindow.WindowClosed -= OnWindowClosed;
        _plate.SetActive(true);
        _dice.gameObject.SetActive(true);
        _dice.DiceResult += OnDiceResult;
    }

    private void OnDiceResult(int value)
    {
        _dice.DiceResult -= OnDiceResult;
        _currentPlayer.CellIndex = Mathf.Min(_currentPlayer.CellIndex + value, cells.Length - 1);
        _currentPlayer.Figure.CanMove = true;
        _dice.gameObject.SetActive(false);
        _plate.SetActive(false);
        _previousNotification = _gameFactory.CreateNotification($"{_currentPlayer.Name}, сходите на {value} вперед");
    }

    private void OnCorrectMoveCompleted()
    {
        if (_previousNotification != null)
            Destroy(_previousNotification);
        _currentPlayer.Figure.CanMove = false;
        _currentPlayer.Statistic.MoveNumber++;
        if (IsWinner(_currentPlayer))
        {
            _winnerNumber++;
            _currentPlayer.Statistic.Place = _winnerNumber;
            _order.RemovePlayer(_currentPlayer);
            if (IsGameFinished)
            {
                _gameStateMachine.Enter<StatisticState, PlayerStatistic[]>(_players.Select(player => player.Statistic)
                    .ToArray());
            }
            else
            {
                NextMove();
            }
        }
        else
        {
            switch (cells[_currentPlayer.CellIndex])
            {
                case ExtraMoveCell:
                    _currentPlayer.Statistic.BonusNumber++;
                    _gameFactory.CreateNotification($"{_currentPlayer.Name}, бросайте кубик")
                        .GetComponent<NotificationWindow>()
                        .WindowClosed += OnWindowClosed;
                    break;
                case PenaltyCell:
                    _currentPlayer.Statistic.PenaltyNumber++;
                    _currentPlayer.CellIndex = Mathf.Min(_currentPlayer.CellIndex - PenaltyValue, cells.Length - 1);
                    _currentPlayer.Figure.CanMove = true;
                    _previousNotification =
                        _gameFactory.CreateNotification($"{_currentPlayer.Name}, сходите на {PenaltyValue} назад");
                    break;
                default:
                    NextMove();
                    break;
            }
        }
    }

    private bool IsWinner(PlayerData playerData) => playerData.CellIndex == cells.Length - 1;
    private bool IsGameFinished => _winnerNumber == _players.Length;
}