using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ソードの機能
/// </summary>
public class Sord : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private float _currentAttackAmount = 2.0f;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void OnTriggerEnter(Collider other)
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
    #endregion

    #region private method
    #endregion
}