using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 全ての敵の共通機能を管理する基底クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour , IDamagable , IPoolable
{
    #region property
    public EnemyType EnemyType => _enemyData.EnemyType;
    public IObservable<Unit> InactiveObserver => _inactiveSubject;
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
    private Coroutine _actionCoroutine;
    private Vector3 _initialPosition = default;
    #endregion

    #region protected
    protected Transform _playerTransform;
    protected float _currentHP;
    #endregion

    #region Constant
    #endregion

    #region Event
    /// <summary>非アクティブとなった時に通知を発行するSubject</summary>
    private Subject<Unit> _inactiveSubject = new Subject<Unit>();
    #endregion

    #region unity methods

    protected virtual void Awake()
    {
        _currentMaxHP = _enemyData.HP;
        _currentHP = _currentMaxHP;
        _currentAttackAmount = _enemyData.AttackAmount;
        _playerTransform = GameObject.FindGameObjectWithTag(GameTag.Player).transform;

        _initialPosition = transform.position;
    }

    protected virtual void Start()
    {
        _actionCoroutine = StartCoroutine(OnActionCoroutine());
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        _inactiveSubject.OnNext(Unit.Default);
        transform.position = _initialPosition;
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

    public void ReturnPool()
    {
        gameObject.SetActive(false);
    }

}