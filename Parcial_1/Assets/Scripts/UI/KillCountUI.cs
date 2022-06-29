using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killCountText;

    [SerializeField] private ArenaWinCondition _winCondition;
    
    public void Update()
    {
        _killCountText.text = $"{_winCondition.KillCount}/{_winCondition.WinScore}";
    }
}