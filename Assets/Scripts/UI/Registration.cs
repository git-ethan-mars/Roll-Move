using System;
using System.Collections.Generic;
using Data;
using Infrastructure.Factory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Registration : MonoBehaviour
    {
        public event Action<PlayerData[]> RegistrationFinished;
        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private Button startButton;
        [SerializeField] private PlayerRegistrationView[] playerViews;
        [SerializeField] private TMP_InputField nameField;
        private IGameFactory _uiFactory;
        private const int MinPlayerNumber = 2;
        private const int MaxPlayerNumber = 4;
        private List<PlayerData> _registeredPlayers;
        private int _currentPlayerIndex;

        public void Construct(IGameFactory uiFactory)
        {
            _uiFactory = uiFactory;
            addButton.onClick.AddListener(AddPlayer);
            removeButton.onClick.AddListener(RemovePlayer);
            startButton.onClick.AddListener(StartGame);
            _registeredPlayers = new List<PlayerData>();
        }


        private void AddPlayer()
        {
            if (_currentPlayerIndex == MaxPlayerNumber)
            {
                CreateNotificationWindow("Нельзя добавить больше игроков");
                return;
            }

            if (IsNameValid(nameField.text))
            {
                var playerName = nameField.text;
                playerViews[_currentPlayerIndex].PlayerName = playerName;
                var color = playerViews[_currentPlayerIndex].FigureColor;
                _registeredPlayers.Add(new PlayerData(playerName, color));
                _currentPlayerIndex++;
                nameField.text = "";
            }
            else
            {
                CreateNotificationWindow("Введено неверное имя");
            }
        }

        private void RemovePlayer()
        {
            if (_currentPlayerIndex != 0)
            {
                _currentPlayerIndex--;
                playerViews[_currentPlayerIndex].PlayerName = "Пусто";
                _registeredPlayers.RemoveAt(_registeredPlayers.Count - 1);
            }
            else
            {
                CreateNotificationWindow("Нет игроков");
            }
        }

        private void StartGame()
        {
            if (MinPlayerNumber <= _currentPlayerIndex)
            {
                RegistrationFinished?.Invoke(_registeredPlayers.ToArray());
                Destroy(gameObject);
            }
            else
            {
                CreateNotificationWindow("Недостаточное количество игроков");
            }
        }

        private void CreateNotificationWindow(string description)
        {
            _uiFactory.CreateNotification(description).GetComponent<NotificationWindow>().WindowClosed += OnErrorWindowClosed;
            SwitchButtons(false);
        }

        private void SwitchButtons(bool state)
        {
            addButton.interactable = state;
            removeButton.interactable = state;
            startButton.interactable = state;
        }

        private void OnErrorWindowClosed(NotificationWindow notificationWindow)
        {
            SwitchButtons(true);
            notificationWindow.WindowClosed -= OnErrorWindowClosed;
        }


        private bool IsNameValid(string playerName) => playerName != "";
    }
}