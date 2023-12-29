using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    #region property
    public IObservable <Unit> ItemGetObserver => _itemGetSubject;
    #endregion

    #region serialize
    #endregion

    #region private
    private ItemGenerator _generator;
    #endregion

    #region Constant
    #endregion

    #region Event
    Subject<Unit> _itemGetSubject = new Subject<Unit>();
    #endregion

    #region unity methods
    private void Awake()
    {
        _generator = GetComponent<ItemGenerator>();
    }

    private void Start()
    {
        GameManager.Instance.GameStartObserver
                            .TakeUntilDestroy(this)
                            .Subscribe(_ =>
                            {
                                _generator.ConstantGenerate(ItemType.Scroll);
                            });
    }
    #endregion

    #region public method
    public void RedisterItem(ItemBase item)
    {
        item.ItemUseObserver
            .Subscribe(_ => _itemGetSubject.OnNext(Unit.Default))
            .AddTo(this);
    }

    public void GenerateItem(ItemType type,Vector3 pos)
    {
        _generator.Generate(type, pos, 300);
    }
    #endregion

    #region private method
    #endregion
}