using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーのステータス全般の機能を持つコンポーネント
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    #region property
    public IObservable<uint> CurrentPlayerLevel => _currentPlayerLevel;
    public IObservable<uint> CurrentExp => _currentExp;
    public IObservable<uint> CurrentRequireExp => _currentRequireExp;
    public IObservable<float> GetExpObserver => _getEXPSubject;
    #endregion

    #region serialize
    [Tooltip("スタート時に必要な経験値")]
    [SerializeField]
    private uint _startRequireExp = 100;
    #endregion

    #region private
    private readonly ReactiveProperty<uint> _currentPlayerLevel = new ReactiveProperty<uint>();
    private readonly ReactiveProperty<uint> _currentExp = new ReactiveProperty<uint>();
    private readonly ReactiveProperty<uint> _currentRequireExp = new ReactiveProperty<uint>();
    #endregion

    #region Constant
    #endregion

    #region Event
    private Subject<float> _getEXPSubject = new Subject<float>();
    #endregion

    #region unity methods
    private void Awake()
    {
        Setup();
    }
    #endregion

    #region public method
    public void AddExp(uint value)
    {
        _currentExp.Value += value;

        //経験値が現在の必要値を超えた場合
        if (_currentExp.Value >= _currentExp.Value)
        {
            uint overFlowExp = _currentExp.Value - _currentRequireExp.Value;

            _currentPlayerLevel.Value++;
            _currentExp.Value = overFlowExp;

            _currentRequireExp.Value = (uint)((_currentRequireExp.Value + (_currentRequireExp.Value / 4)) * 1.2);
        }
        //経験値獲得時のSubject
        _getEXPSubject.OnNext((float)_currentExp.Value / _currentRequireExp.Value);
    }
    #endregion

    #region private method
    /// <summary>
    /// 初期化
    /// </summary>
    private void Setup()
    {
        _currentRequireExp.Value = _startRequireExp;
    }
    #endregion
}