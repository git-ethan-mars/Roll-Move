using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NotificationWindow : MonoBehaviour
    {
        public event Action<NotificationWindow> WindowClosed;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button closeButton;
        public void Construct(string text)
        {
            description.SetText(text);
            closeButton.onClick.AddListener(CloseWindow);
        }

        private void CloseWindow()
        {
            WindowClosed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}