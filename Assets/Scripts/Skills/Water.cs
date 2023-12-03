﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Water : MonoBehaviour , IPoolable
{
    #region property
    public IObservable<Unit> InactiveObserver => _inactiveSubject;
    #endregion

    #region serialize
    [SerializeField]
    private float _moveSpeed = 0.3f;
    #endregion

    #region private
    private float _currentAttackAmount = 0.5f;
    private float _lifeTime = 2.0f;
    private Coroutine _currentCoroutine;
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<Unit> _inactiveSubject = new Subject<Unit>();
    #endregion

    #region unity methods
    private void Awake()
    {
        _currentCoroutine = StartCoroutine(InActiveCoroutine());
    }

    private void Start()
    {
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                transform.Translate(0, 0, _moveSpeed);
            });
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

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            Debug.Log("水遁があたった");
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

    public void ReturnPool()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region private method
    #endregion

    #region Coroutine method
    private IEnumerator InActiveCoroutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }
    #endregion
}