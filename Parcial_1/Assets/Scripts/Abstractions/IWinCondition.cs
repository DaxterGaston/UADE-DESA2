
public interface IWinCondition : IObservable<LevelState>
{
    public void CheckWinCondition();
    public bool WinConditionMet();
}