using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : EnemyBase
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("追うスピード")]
    [SerializeField]
    private float _moveSpeed = 1f;
    #endregion

    #region private
    private Vector3 _targetPosition;
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
        Vector3 playerUnderPosOffset = new Vector3(0, -0.5f, 0) , enemyLastChasePos = new Vector3(0, 0, -3);
        float chaseStopPos = 0.6f , inActiveZpos = -1;
        Vector3 initPlayerPos = PlayerController.Instance.InitPlayerPos;
        Vector3 currentPlayerPos = Vector3.zero;
        while (true)
        {
            if (gameObject.transform.position.z >= chaseStopPos)
            {
                currentPlayerPos = (PlayerController.Instance.OnDown) ? playerUnderPosOffset : initPlayerPos;
                MoveTowardsTarget(currentPlayerPos);
                Debug.Log($"<color=yellow>{currentPlayerPos}</color>");
            }
            else
                MoveTowardsTarget(enemyLastChasePos);

            if (gameObject.transform.position.z <= inActiveZpos)
            {
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
    #endregion
}