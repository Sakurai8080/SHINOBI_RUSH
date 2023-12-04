using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private ItemGenerator _generator;
    #endregion

    #region Constant
    #endregion

    #region Event
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
    public void GenerateItem(ItemType type,Vector3 pos)
    {
        _generator.Generate(type, pos, 300);
    }
    #endregion

    #region private method
    #endregion
}