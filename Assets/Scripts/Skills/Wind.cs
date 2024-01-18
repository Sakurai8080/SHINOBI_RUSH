using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 風遁用の風の機能
/// </summary>
public class Wind : MonoBehaviour , IPoolable
{
    #region property
    public IObservable<Unit> InactiveObserver => _inactiveSubject;
    #endregion

    #region serialize
    #endregion

    #region private
    private float _currentAttackAmount = 1.0f;
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<Unit> _inactiveSubject = new Subject<Unit>();
    #endregion

    #region unity methods
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            IDamagable target = other.GetComponent<IDamagable>();
            target.Damage(_currentAttackAmount);
        }
    }
    #endregion

    #region public method
    public void SetAttackAmount(float amount)
    {
        _currentAttackAmount += amount;
    }

    public void SizeUp(float scaleFactor)
    {
        transform.localScale *= scaleFactor;
    }

    public void ReturnPool()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region private method
    #endregion

    #region coroutine method

    private IEnumerator InActiveCoroutine()
    {
        yield return null;
    }
    #endregion
}