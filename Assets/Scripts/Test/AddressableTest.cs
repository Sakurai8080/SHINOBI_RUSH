using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableTest : MonoBehaviour
{
    #region property
    public GameObject FireObj => _fireObj;

    public static AddressableTest Instance { get; private set; }
    #endregion

    #region serialize
    #endregion

    #region private
    private AsyncOperationHandle<GameObject> _fire;
    private GameObject _fireObj;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        //直接インスタンス化する場合。プレハブ専用。
        //AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Group1/shiho");

        //LoadAsyncで読み込み
        _fire = Addressables.LoadAsset<GameObject>("Fire");

        //読み込み完了までawait
        GameObject prefab = await _fire.Task;

        //読み込んだプレハブをインスタンス化
        _fireObj = Instantiate(prefab);
        _fireObj.name = "Fire";
    }

    private void OnDestroy()
    {
        Destroy(_fireObj);

        //使い終わったらhandleをリリース。使い終わったら必ずリリース。0になったらアセットがアンロードになる。
        Addressables.Release(_fire);
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}