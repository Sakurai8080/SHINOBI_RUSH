﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーのステータスを管理するコンポーネント
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region property
    public IObservable<float> ChangeHPObserver => _changeHPSubject;
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("ゲーム開始時の最大HP")]
    [SerializeField]
    private float _startMaxHP = 50f;
    #endregion

    #region private
    /// <summary>現在の最大HP</summary>
    private float _currentMaxHp;
    /// <summary>現在のHP</summary>
    private float _currenHP;
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<float> _changeHPSubject = new Subject<float>();
    #endregion

    #region unity methods
    private void Awake()
    {
        Setup();
    }
    #endregion

    #region public method
    public bool Damage(float amount)
    {
        _currenHP -= amount;
        _changeHPSubject.OnNext(_currenHP / _currentMaxHp);


        if (_currenHP <= 0)
        {
            AudioManager.PlaySE(SEType.PlayerDied);
            GameManager.Instance.OnGameEnd();
            GameManager.Instance.SceneLoader("Result");
            return true;
        }
        return false;
    }
    #endregion

    #region private method
    private void Setup()
    {
        _currentMaxHp = _startMaxHP;
        _currenHP = _currentMaxHp;
    }
    #endregion
}