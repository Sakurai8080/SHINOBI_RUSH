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
    public IReactiveProperty<bool> OnAvaterd => _onAvatered;
    private ReactiveProperty<bool> _onAvatered = new ReactiveProperty<bool>();
    #endregion

    #region serialize
    [SerializeField]
    private ParticleSystem _avaterEffect;

    [SerializeField]
    private GameObject _playerMeshies;
    #endregion

    #region private
    private Vector3 _firstPosition = default;
    private Vector3 _firstRotate = default;
    private Vector3 _secondPosition = new Vector3(0,-0.05f,0);
    private Vector3 _secondRotate = new Vector3(0, 0, 180);
    private Vector3 _avaterEffectRotation = new Vector3(0, 0, 270);
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
            StartCoroutine(OnAvaterCroutine(_secondPosition,_secondRotate));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(OnAvaterCroutine(_firstPosition, _firstRotate));
        }
    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void GameObjectActivator(GameObject g,bool b)
    {
        g.SetActive(b);
    }
    #endregion

    #region coroutine method
    private IEnumerator OnAvaterCroutine(Vector3 playerPos, Vector3 playerAngles)
    {
        if (!_onAvatered.Value) {
            _onAvatered.Value = true;
            _col.enabled = false;
            transform.localPosition = playerPos;
            transform.localEulerAngles = playerAngles;
            GameObjectActivator(_avaterEffect.gameObject, true);
            _avaterEffect.Play();
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