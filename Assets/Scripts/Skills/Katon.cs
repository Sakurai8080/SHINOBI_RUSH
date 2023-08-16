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
    private float _currentScale = 1.0f;
    private float _currentAttackAmount = 1.0f;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {

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

    private void Update()
    {

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
        transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
    }
    #endregion

    #region private method
    #endregion
}