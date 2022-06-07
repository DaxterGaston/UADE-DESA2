using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Player
{
    public class GUIController : MonoBehaviour, IObserver
    {
        [SerializeField]
        TextMeshProUGUI _ammoText;
        [SerializeField]
        TextMeshProUGUI _healthText;

        public void OnNotify(string message, params object[] args)
        {
            if (message == "HEALTH_CHANGE")
            {
                _healthText.text = args[0] as string;
            }
            if (message == "AMMO_CHANGE")
            {
                _ammoText.text = args[0] as string;
            }
        }

        
    }
}