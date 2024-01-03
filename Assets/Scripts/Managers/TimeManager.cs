using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using Cysharp.Threading.Tasks;

/// <summary>
/// 時間に伴って処理するマネージャー
/// </summary>
public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    #region property
    public IObservable<uint> EnemyEventObserver => _enemyEventSubject;
    public ReactiveProperty<uint> _currentLimitTime = new ReactiveProperty<uint>();
    #endregion

    #region serialize
    [Header("Variables")]
    [Tooltip("制限時間(分)")]
    [SerializeField]
    private uint _limit = 6;

    [Tooltip("制限時間を表示するTMP")]
    [SerializeField]
    private TextMeshProUGUI _limitTimeTMP = default;

    [Tooltip("エネミーウェーブと時間を紐付けるイベント")]
    [SerializeField]
    private EnemyChangeEvent[] _enemyEvents;
    #endregion

    #region private
    #endregion

    #region Constant
    private const uint TIME_SECOND = 1;
    #endregion

    #region Event
    private IObservable<long> _currentTimeEvent;
    private Subject<uint> _enemyEventSubject = new Subject<uint>(); 
    #endregion
    
    #region unity methods
    private void Awake()
    {
        _currentLimitTime.Value = _limit * 60;
    }

    private void Start()
    {
        GameManager.Instance.GameStartObserver
                            .TakeUntilDestroy(this)
                            .Subscribe(_ => OnLimitAndEventTimer());

        GameManager.Instance.IsGameEndObsever
                            .TakeUntilDestroy(this)
                            .Subscribe(gameEnded =>
                            {
                                if (gameEnded)
                                    RemainTimeSave();
                            });

        _currentLimitTime.TakeUntilDestroy(this)
                         .Where(value => value >= 0)
                         .Subscribe(value =>
                         {
                             _limitTimeTMP.text = $"{value / 60:00}:{(value % 60):00}";
                             if (value <= 0)
                             {
                                 _limitTimeTMP.enabled = false;
                             }
                         });
    }

    #endregion

    #region public method
    #endregion

    #region private method
    private void OnLimitAndEventTimer()
    {
        _currentTimeEvent = _enemyEvents.Select(e => Observable.Timer(TimeSpan.FromSeconds(e.InvokeTime)))
                                        .Merge();

        uint currentEnemyEventIndex = 1;

        _currentTimeEvent.TakeUntilDestroy(this)
                         .Subscribe(_ =>
                         {
                             _enemyEventSubject.OnNext(currentEnemyEventIndex);
                             currentEnemyEventIndex++;
                         });

        OnLimitTimerAsync().Forget();
    }

    private async UniTaskVoid OnLimitTimerAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(TIME_SECOND));

        while (_currentLimitTime.Value > 0)
        {
            _currentLimitTime.Value -= TIME_SECOND;
            await UniTask.Delay(TimeSpan.FromSeconds(TIME_SECOND));
        }
    }

    private void RemainTimeSave()
    {
        PersistentDataManager.Instance.CurrentLimitTime = _currentLimitTime.Value;
    }
    #endregion
}

[Serializable]
public struct EnemyChangeEvent
{
    public string WaveName;
    public uint InvokeTime;
}