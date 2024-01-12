using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : EnemyBase
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private float _moveSpeed = 2f;
    #endregion

    #region private
    private Vector3 _targetPosition;
    private Vector3 _initialTargetPosition;
    private Vector3 _playerUnderPosOffset = new Vector3(0,-0.5f,0);
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
        _targetPosition = new Vector3(_playerTransform.position.x, _playerTransform.position.y, _playerTransform.position.z);
        _initialTargetPosition = _targetPosition;
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
    private void MoveTowardsTarget(Vector3 currentTargetPos)
    {
        transform.LookAt(currentTargetPos);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentTargetPos, _moveSpeed * Time.deltaTime);
    }
    #endregion

    #region coroutine method
    protected override IEnumerator OnActionCoroutine()
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.localPosition, _playerTransform.localPosition);
            Vector3 tempPos;
            if (gameObject.transform.position.z >= 0.6f)
            {
                tempPos = (_playerTransform.position != _initialPlayerPos) ? (_targetPosition + _playerUnderPosOffset) : _initialTargetPosition;
                MoveTowardsTarget(tempPos);
            }
            else
                MoveTowardsTarget(new Vector3(transform.position.x,transform.position.y,-3));

            if (gameObject.transform.position.z <= -1)
                gameObject.SetActive(false);

            yield return null;
        }
    }
    #endregion
}