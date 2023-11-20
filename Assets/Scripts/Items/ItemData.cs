using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/ItemData")]
public class ItemData : ScriptableObject
{
    #region property
    public ItemType ItemType => _itemType;
    public uint ReserveAmount => _reserveAmount;
    #endregion

    #region serialize
    [SerializeField]
    private ItemType _itemType = default;

    [SerializeField]
    private uint _reserveAmount = 5;
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {

    }

    private void Start()
    {

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

public enum ItemType
{
    None,
    EXP,
    Heal,
    PowerUp
}