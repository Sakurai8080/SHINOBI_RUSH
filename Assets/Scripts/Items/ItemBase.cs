using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public abstract class ItemBase : MonoBehaviour, IPoolable 
{
    #region property
    public ItemType ItemType => _itemData.ItemType;

    public IObservable<Unit> InactiveObserver => _inactiveSubject;
    public IObservable<Unit> ItemUseObserver => _itemUseSubject;
    #endregion

    #region serialize
    [SerializeField]
    private ItemData _itemData = default;

    [Tooltip("アイテムが非表示になるプレイヤーとの距離")]
    [SerializeField]
    protected float _hideDistance = -1f;
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
    private Subject<Unit> _itemUseSubject = new Subject<Unit>();
    #endregion

    #region unity methods
    protected virtual void Awake()
    {
        _playerTrans = GameObject.FindGameObjectWithTag(GameTag.Player).transform;
        _rd = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(3f))
                  .TakeUntilDestroy(this)
                  .Where(_ => gameObject.activeSelf)
                  .Subscribe(_ =>
                  {
                      if (transform.position.z - _playerTrans.position.z <= _hideDistance)
                          Return();
                  });
    }

    protected virtual void OnEnable()
    {
        _itemPosition = transform.position;
    }

    protected virtual void OnDisable()
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
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Player))
        {
            this.gameObject.SetActive(false);
            _itemUseSubject.OnNext(Unit.Default);
        }
    }

    /// <summary>
    /// アイテムを戻す処理
    /// </summary>
    public abstract void Return();
    #endregion

    #region private method
    #endregion
}