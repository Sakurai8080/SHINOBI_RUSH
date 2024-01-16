using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 手裏剣を操作するコンポーネント
/// </summary>
public class Shuriken : MonoBehaviour , IPoolable
{
    #region property
    public IObservable<Unit> InactiveObserver => _inactiveSubject;
    #endregion

    #region serialize
    [Header("変数")]
    [Tooltip("回転スピード")]
    [SerializeField]
    private float _rotateSpeed = 1000.0f;
    #endregion

    #region private
    private float _moveSpeed = 0.5f;

    private float _currentAttackAmount = 1.0f;
    private float _lifeTime = 5.0f;
    private Coroutine _currentCoroutine = null;

    private Rigidbody _rb;
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
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ => transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0));
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
        Vector3 normarizeDir = enemyDir.normalized;
        _rb.velocity = enemyDir * _moveSpeed;
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