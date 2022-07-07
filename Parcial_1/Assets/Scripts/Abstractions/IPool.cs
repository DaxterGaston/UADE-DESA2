namespace Assets.Scripts.Abstractions
{
    public interface IPool<T>
    {
        void Store(T item);
        T GetInstance();
        int IsAvailable { get; }

        void SetAllUsedAsAvailable();

        bool CanSetUsedAsAvailable { get; }
    }
}
