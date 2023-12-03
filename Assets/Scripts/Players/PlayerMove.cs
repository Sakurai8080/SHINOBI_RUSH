using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;
using UniRx.Triggers;

public class PlayerMove : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private Vector3 _firstPosition = default;
    private Vector3 _firstRotate = default;

    private Vector3 _secondPosition = new Vector3(0,-0.05f,0);
    private Vector3 _secondRotate = new Vector3(0, 0, 180);

    private Coroutine _currentCoroutine;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.localPosition = _secondPosition;
            transform.localEulerAngles = _secondRotate;
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.localPosition = _firstPosition;
            transform.localEulerAngles = _firstRotate;
        }
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion

    //IEnumerator Activation()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    Debug.Log("プレイヤーtrue");
    //}
}