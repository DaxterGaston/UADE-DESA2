using Assets.Scripts.Abstractions;
using UnityEngine;

public class TakeDamageCommand : ICommand
{
    private IHealth _target;
    private int _amount;

    public TakeDamageCommand(IHealth target, int amount)
    {
        _target = target;
        _amount = amount;
    }

    public void Execute()
    {
        _target.Damage(_amount);
    }
}
