using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Katon : MonoBehaviour , IPoolable
{
    #region property
    public IObservable<Unit> InactiveObserver => _inactiveSubject;
    #endregion

    #region serialize
    [SerializeField]
    private float _moveSpeed = 0.5f;
    #endregion

    #region private
    private float _currentAttackAmount = 1.0f;
    private Vector3 _initialScale;
    private Vector3 _currentScale;
    private Coroutine _currentCoroutine;
    private float _lifeTime = 5.0f;
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<Unit> _inactiveSubject = new Subject<Unit>();
    #endregion

    #region unity methods
    private void Awake()
    {
        _initialScale = transform.localScale;

        _currentScale = _initialScale;
        Debug.Log($"_initialScaleは{_initialScale}");
    }

    private void OnEnable()
    {
        _currentCoroutine = StartCoroutine(InActiveCoroutine());
        _currentScale = _initialScale;
    }

    private void OnDisable()
    {
        if (_currentCoroutine!= null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
        transform.localPosition = Vector3.zero;
        _inactiveSubject.OnNext(Unit.Default);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            Debug.Log("火遁があたった");
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

    public void SizeChange(float amount)
    {
         _currentScale *= amount;
         Debug.Log($"Size変更は{_currentScale}");
         transform.localScale = _currentScale;
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