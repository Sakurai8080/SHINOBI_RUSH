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
    [Header("Variable")]
    [Tooltip("剣の攻撃力")]
    [SerializeField]
    private float _currentAttackAmount = 10.0f;
    #endregion

    #region private
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