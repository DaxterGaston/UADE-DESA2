
public interface ILevelController
{
    public IWinCondition WinCondition { get; }
    public void LevelStart();
    public void LevelEnd();

    public void NextLevel();
}
