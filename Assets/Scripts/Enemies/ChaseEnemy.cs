using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : EnemyBase
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private float _moveSpeed = 1.0f;
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion

    #region coroutine method
    protected override IEnumerator OnActionCoroutine()
    {
        while (true)
        {
            transform.LookAt(_playerTransform);
            float distance = Vector3.Distance(transform.localPosition, _playerTransform.localPosition);

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _playerTransform.position, _moveSpeed*Time.deltaTime);
            yield return null;
        }
    }
    #endregion
}