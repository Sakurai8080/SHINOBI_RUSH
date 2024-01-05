using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体を管理するクラス
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region property
    public IObservable<bool> IsGameEndObsever => _isGameEnd;
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
    Subject<bool> _isGameEnd = new Subject<bool>();
    #endregion

    #region unity methods
    #endregion

    #region public method
    public void OnGameStart()
    {
        _gameStartSubject.OnNext(Unit.Default);
    }

    public void OnGameEnd()
    {
        _isGameEnd.OnNext(true);
        SceneManager.LoadScene("Result");
    }
    #endregion

    #region private method
    #endregion
}