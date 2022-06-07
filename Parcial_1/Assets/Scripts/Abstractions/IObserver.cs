public interface IObserver
{
    void OnNotify(string message, params object[] args);
}
