using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerRegistrationView : MonoBehaviour
    {
        [SerializeField] private Image figureColor;
        [SerializeField] private TextMeshProUGUI playerName;

        public Color FigureColor => figureColor.color;
    
        public string PlayerName
        {
            set => playerName.SetText(value);
        }
    }
}