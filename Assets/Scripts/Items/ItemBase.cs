using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class ItemBase : MonoBehaviour, IPoolable
{
    #region property
    public IObservable<Unit> InactiveObserver => _inactiveSubject;
    #endregion

    #region serialize
    [SerializeField]
    private ItemData _itemData = default;
    #endregion

    #region private
    private Vector3 _itemPosition;
    private Rigidbody _rd;
    private Transform _playerTrans;
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<Unit> _inactiveSubject = new Subject<Unit>();
    #endregion

    #region unity methods
    private void Awake()
    {
        _playerTrans = GameObject.FindGameObjectWithTag(GameTag.Player).transform;
        _rd = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        _itemPosition = transform.position;
    }

    private void OnDisable()
    {
        _inactiveSubject.OnNext(Unit.Default);
    }

    #endregion

    #region public method
    public void ReturnPool()
    {
        gameObject.SetActive(false);
    }
    #endregion


    #region abstract method
    /// <summary>
    /// アイテム使用
    /// </summary>
    /// <param name="player"></param>
    public abstract void Use(PlayerController player);

    /// <summary>
    /// アイテムを戻す処理
    /// </summary>
    public abstract void Return();
    #endregion

    #region private method
    #endregion
}