using System;
using UniRx;

public interface IPoolable
{
    IObservable<Unit> InactiveObserver { get; }
    void ReturnPool();
}