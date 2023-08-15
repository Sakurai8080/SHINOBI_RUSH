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
    private float _moveSpeed = 1.0f;
    #endregion

    #region private
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
                transform.position = Vector3.forward;
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
        transform.localScale = new Vector3(transform.localScale.x * amount,transform.localScale.y * amount,transform.localScale.z* amount);
    }
    #endregion

    #region private method
    #endregion
}