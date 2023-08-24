using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public interface IPoolable
{
    IObservable<Unit> InactiveObserver { get; }

    void ReturnPool();
}