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
public class GameManager : MonoBehaviour
{
    #region property
    public static GameManager Instance { get; private set; }

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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneLoaded;
    }
    #endregion

    #region public method
    public void OnGameStart()
    {
        _gameStartSubject.OnNext(Unit.Default);
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void BGMChange(BGMType type)
    {
        AudioManager.PlayBGM(type);
    }
    #endregion

    #region private method
    private void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        switch (nextScene.name)
        {
            case "InGame":
                BGMChange(BGMType.InGame);
                break;
            case "Title":
                BGMChange(BGMType.Title);
                break;
            case "Result":
                BGMChange(BGMType.Result);
                break;
            default:
                Debug.LogError($"<color=red>切り替えられたシーン{nextScene.name}は不明です</color>");
                break;
        }
    }
    #endregion
}