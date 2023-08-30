using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region property
    public IObservable<Unit> GameStartObserver => _gameStartSubject;
    #endregion

    #region serialize
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    Subject<Unit> _gameStartSubject = new Subject<Unit>();
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
    public void OnGameStart()
    {
        _gameStartSubject.OnNext(Unit.Default);
    }
    #endregion

    #region private method
    #endregion
}