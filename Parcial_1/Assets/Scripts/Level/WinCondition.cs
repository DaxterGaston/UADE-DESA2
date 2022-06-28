using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour, IWinCondition
{
    public List<IObserver<LevelState>> Subscribers { get; }
    public virtual void Subscribe(IObserver<LevelState> observer)
    {
        
    }

    public virtual void Unsubscribe(IObserver<LevelState> observer)
    {
        
    }

    public virtual void NotifyAll(LevelState message, params object[] args)
    {
        
    }

    public virtual void CheckWinCondition()
    {
        
    }

    public virtual bool WinConditionMet()
    {
        return false;
    }
}
