using System;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] private Text _killCountText;

    [SerializeField] private ArenaWinCondition _winCondition;
    
    public void Update()
    {
        _killCountText.text = $"{_winCondition.KillCount}/{_winCondition.WinScore}";
    }
}