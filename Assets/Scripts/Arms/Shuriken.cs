using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Shuriken : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("変数")]
    [Tooltip("回転スピード")]
    [SerializeField]
    private float _rotateSpeed = 1000.0f;
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
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ => transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0));
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