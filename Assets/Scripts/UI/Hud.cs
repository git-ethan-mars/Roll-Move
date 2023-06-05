using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
    
        public void Awake()
        {
            exitButton.onClick.AddListener(Application.Quit);
        }

    }
}