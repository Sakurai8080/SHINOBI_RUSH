using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Water : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private float _moveSpeed = 0.3f;
    #endregion

    #region private
    private float _currentAttackAmount = 0.5f;
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
    #endregion

    #region private method
    #endregion
}