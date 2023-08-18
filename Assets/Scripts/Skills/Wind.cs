using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private Rigidbody _rb;

    private float _moveSpeed = 0.5f;

    private float _currentAttackAmount = 1.0f;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
       
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            IDamagable target = other.GetComponent<IDamagable>();
            target.Damage(_currentAttackAmount);
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region public method
    public void SetAttackAmount(float amount)
    {
        _currentAttackAmount += amount;
    }

    public void SetVelocity(Vector3 enemyDir)
    {
        _rb.velocity = enemyDir * _moveSpeed;
    }
    #endregion

    #region private method
    #endregion
}