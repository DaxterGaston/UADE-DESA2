public interface IObserver
{
    void OnNotify(string message, params object[] args);
}

public interface IObserver<T>
{
    void OnNotify(T message, params object[] args);
}
