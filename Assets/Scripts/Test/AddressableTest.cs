using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableTest : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private AsyncOperationHandle<GameObject> _prefabHandle;
    private GameObject testObj;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {

    }

    private async void Start()
    {
        //直接インスタンス化する場合。プレハブ専用。
        //AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Group1/shiho");

        //LoadAsyncで読み込み
        _prefabHandle = Addressables.LoadAsset<GameObject>("Group1/shiho");

        //読み込み完了までawait
        GameObject prefab = await _prefabHandle.Task;

        //読み込んだプレハブをインスタンス化
        testObj = Instantiate(prefab);
        testObj.name = "shiho";
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        Destroy(testObj);

        //使い終わったらhandleをリリース。使い終わったら必ずリリース。0になったらアセットがアンロードになる。
        Addressables.Release(_prefabHandle);
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}