using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての敵の共通機能を管理する基底クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour , IDamagable
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
    private float _currentMaxHP;
    private float _currentAttackAmount;
    private Coroutine _actionCroutine;
    #endregion

    #region protected
    protected Transform _playerTransform;
    protected float _currentHP;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods

    protected virtual void Awake()
    {
        _currentMaxHP = _enemyData.HP;
        _currentHP = _currentMaxHP;
        _currentAttackAmount = _enemyData.AttackAmount;
        _playerTransform = GameObject.FindGameObjectWithTag(GameTag.Player).transform;
    }

    protected virtual void Start()
    {
        _actionCroutine = StartCoroutine(OnActionCoroutine());
    }
    #endregion

    #region public method
    public virtual void Damage(float amount)
    {
        _currentHP -= amount;
        Debug.Log(_currentHP);

        if (_currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
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