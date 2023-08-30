using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Wind : MonoBehaviour , IPoolable
{
    #region property
    public IObservable<Unit> InactiveObserver => _inactiveSubject;
    #endregion

    #region serialize
    [SerializeField]
    private float _scaleChangeAmount = 1.0f;
    #endregion

    #region private
    private Rigidbody _rb;
    private float _moveSpeed = 0.5f;
    private float _currentAttackAmount = 1.0f;
    private Coroutine _currentCoroutine = default;
    private Vector3 _initialScale;
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<Unit> _inactiveSubject = new Subject<Unit>();
    #endregion

    #region unity methods
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    private void OnEnable()
    {
        _currentCoroutine = StartCoroutine(InActiveCoroutine());
    }

    private void OnDisable()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
        transform.localPosition = Vector3.zero;
        _inactiveSubject.OnNext(Unit.Default);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            IDamagable target = other.GetComponent<IDamagable>();
            target.Damage(_currentAttackAmount);
            _currentCoroutine = StartCoroutine(ExplosionCoroutine());
            StartCoroutine(InActiveCoroutine());
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

    public void ReturnPool()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region private method
    #endregion

    #region coroutine method
    private IEnumerator ExplosionCoroutine()
    {
        Vector3 currentScale = _initialScale;
        float targetScaleMagnitude = _initialScale.magnitude * 10; 

        while(currentScale.magnitude <= targetScaleMagnitude)
        {
            currentScale += _initialScale * _scaleChangeAmount * Time.deltaTime;
            transform.localScale = currentScale;
        }
        yield return null;
        gameObject.SetActive(false);
        transform.localScale = _initialScale;
    }

    private IEnumerator InActiveCoroutine()
    {
        yield return null;
    }
    #endregion
}