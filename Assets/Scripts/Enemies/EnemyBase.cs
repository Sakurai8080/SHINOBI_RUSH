using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての敵の共通機能を管理する基底クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("変数")]
    [Tooltip("エネミーの種類")]
    [SerializeField]
    private EnemyData _enemyData = default;
    #endregion

    #region private
    private Coroutine _actionCroutine;
    #endregion

    #region protected
    protected Transform _playerTransform;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods

    protected virtual void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag(GameTag.Player).transform;
    }

    protected virtual void Start()
    {
        _actionCroutine = StartCoroutine(OnActionCoroutine());
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion

    #region coroutine method
    /// <summary>
    /// 敵ごとの行動
    /// </summary>
    protected abstract IEnumerator OnActionCoroutine();
    #endregion

}