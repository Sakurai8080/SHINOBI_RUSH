using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Katon : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private float _moveSpeed = 0.5f;
    #endregion

    #region private
    private float _currentAttackAmount = 1.0f;
    private Vector3 _initialScale;
    private Vector3 _currentScale;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _initialScale = transform.localScale;

        _currentScale = _initialScale;
        Debug.Log($"_initialScaleは{_initialScale}");

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
    #endregion

    #region private method
    #endregion
}