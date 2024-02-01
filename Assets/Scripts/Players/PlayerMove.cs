using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// プレイヤーの動作を処理するクラス
/// </summary>
public class PlayerMove : MonoBehaviour
{
    #region property
    public IReactiveProperty<bool> OnDown => _onDown;
    private ReactiveProperty<bool> _onDown = new ReactiveProperty<bool>();
    public IReactiveProperty<bool> OnAvaterd => _onAvatered;
    private ReactiveProperty<bool> _onAvatered = new ReactiveProperty<bool>();

    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("移動中のエフェクト")]
    [SerializeField]
    private ParticleSystem _avaterEffect;

    [Tooltip("プレイヤーのメッシュ")]
    [SerializeField]
    private GameObject _playerMeshies;
    #endregion

    #region private
    private Vector3 _firstPosition = default;
    private Vector3 _firstRotate = default;
    private Vector3 _secondPosition = new Vector3(0,-0.05f,0);
    private Vector3 _secondRotate = new Vector3(0, 0, 180);
    private BoxCollider _col = default;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _firstPosition = transform.position;
        _firstRotate = transform.rotation.eulerAngles;
        _col = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(OnAvaterCoroutine(_secondPosition,_secondRotate));
            _onDown.Value = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(OnAvaterCoroutine(_firstPosition, _firstRotate));
            _onDown.Value = false;
        }
    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void GameObjectActivator(GameObject go,bool activeSwitch)
    {
        go.SetActive(activeSwitch);
    }
    #endregion

    #region coroutine method
    private IEnumerator OnAvaterCoroutine(Vector3 playerPos, Vector3 playerAngles)
    {
        if (!_onAvatered.Value) {
            _onAvatered.Value = true;
            _col.enabled = false;
            transform.localPosition = playerPos;
            transform.localEulerAngles = playerAngles;
            GameObjectActivator(_avaterEffect.gameObject, true);
            _avaterEffect.Play();
            AudioManager.PlaySE(SEType.Avater);
            GameObjectActivator(_playerMeshies.gameObject, false);
            yield return new WaitForSeconds(2);
            _avaterEffect.Stop();
            GameObjectActivator(_avaterEffect.gameObject, false);
            GameObjectActivator(_playerMeshies.gameObject, true);
            _col.enabled = true;
        }
        _onAvatered.Value = false;
    }
    #endregion
}