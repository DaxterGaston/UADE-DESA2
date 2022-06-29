using System;
using UnityEngine;

public class LevelController : MonoBehaviour, ILevelController, IObserver<LevelState>
{

    [SerializeField] private WinCondition _winCondition;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Door _exitDoor;
    public IWinCondition WinCondition { get; }

    [SerializeField] private string _nextLevel;

    private void Start()
    {
        _winCondition.Subscribe(this);
        _exitDoor.Subscribe(this);
        LevelStart();
    }
    
    public void LevelStart()
    {
        
    }

    public void LevelEnd()
    {
        _exitDoor.OpenDoor();
    }

    public void NextLevel()
    {
        _levelLoader.LoadNextLevel(_nextLevel);
    }


    public void OnNotify(LevelState message, params object[] args)
    {
        if (message == LevelState.WinConditionMet)
        {
            LevelEnd();
        } 
        else if (message == LevelState.EnteredDoor)
        {
            NextLevel();
        }
    }
}