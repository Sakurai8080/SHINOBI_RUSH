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

    [SerializeField]
    private float _doLookAtPos = 1.0f;
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
        Vector3 targetPosition = new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z + 1);
        float distance = Vector3.Distance(transform.localPosition, _playerTransform.localPosition);
        while (true)
        {
            if (distance > _doLookAtPos)
                transform.LookAt(_playerTransform);

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, _moveSpeed*Time.deltaTime);
            yield return null;
        }
    }
    #endregion
}