using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;

public class ItemGenerator : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private Item[] _items = default;

    [SerializeField]
    private Vector3 _generatePointValue = default;
    #endregion

    #region private
    private float _itemGenerateYPos = 0.3f;
    private Transform _playerTrans;
    private Dictionary<ItemType, Objectpool<ItemBase>> _itemPoolDic = new Dictionary<ItemType, Objectpool<ItemBase>>();
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        Setup();
    }
    #endregion

    #region public method
    public void Generate(ItemType type, Vector3 pos, uint limit = 50)
    {
        if (type == ItemType.None)
            return;

        var item = _itemPoolDic.FirstOrDefault(x => x.Key == type).Value.Rent(limit);

        if (item != null)
        {
            item.gameObject.SetActive(true);
            item.transform.localPosition = pos;
            ItemManager.Instance.RedisterItem(item);
        }
        else
            Debug.LogError($"{item}がありません");
    }

    public void ConstantGenerate(ItemType type)
    {
        StartCoroutine(ConstantGenerateCotroutine(type));
    }
    #endregion

    #region private method
    private void Setup()
    {
        _playerTrans = GameObject.FindGameObjectWithTag(GameTag.Player).transform;

        for (int i = 0; i < _items.Length; i++) {
            _itemPoolDic.Add(_items[i].ItemPrefab.ItemType, new Objectpool<ItemBase>(_items[i].ItemPrefab, _items[i].Parent));
        }

    }
    #endregion


    #region coroutine method
    private IEnumerator ConstantGenerateCotroutine(ItemType type)
    {
        var interval = new WaitForSeconds(_items.FirstOrDefault(x => x.ItemPrefab.ItemType == type).GenerateInterval);
        while (true)
        {
            float generateYPos = Random.Range(-_itemGenerateYPos, _itemGenerateYPos) < 0 ? -_itemGenerateYPos : _itemGenerateYPos;

            Vector3 generatePos = new Vector3(_playerTrans.position.x, generateYPos, _playerTrans.position.z + 20);
            Generate(type, generatePos, 5);
            yield return interval;
        }
    }
    #endregion

}


[System.Serializable]
class Item
{
    /// <summary>アイテム名</summary>
    public string ItemName;
    /// <summary>生成するアイテムのプリファブ</summary>
    public ItemBase ItemPrefab;
    /// <summary>アイテムの親オブジェクト</summary>
    public Transform Parent;
    /// <summary>次に生成されるまでの間隔</summary>
    [Range(0.1f, 10f)]
    public float GenerateInterval;
}