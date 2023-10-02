using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class SignalManager : SingletonMonoBehaviour<SignalManager>
{
    #region property
    public IObservable<Unit> CameraMoveObserver => _cameraMoveSubject;
    #endregion

    #region serialize
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<Unit> _cameraMoveSubject = new Subject<Unit>();
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
    public void CameraMoveChange()
    {
        _cameraMoveSubject.OnNext(Unit.Default);
    }

    public void GameStartSignal()
    {
        GameManager.Instance.OnGameStart();
    }
    #endregion

    #region private method
    #endregion
}