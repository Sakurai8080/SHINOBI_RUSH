using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

public class ItemChecker : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    ItemBase _itembase;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _itembase = GetComponent<ItemBase>();
    }

    private void Start()
    { 
        this.OnTriggerEnterAsObservable()
            .TakeUntilDestroy(this)
            .Where(x => x.CompareTag(GameTag.Item))
            .Subscribe(x =>
            {
                 _itembase.Use(PlayerController.Instance);
            });
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}