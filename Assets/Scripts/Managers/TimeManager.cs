using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using Cysharp.Threading.Tasks;

public class TimeManager : SingletonMonoBehaviour<TimeManager>
{
    #region property
    public static TimeManager Instance { get; set; }
    #endregion

    #region serialize
    [Header("Variacles")]
    [Tooltip("制限時間(分)")]
    [SerializeField]
    private uint _limit = 6;

    [Tooltip("制限時間を表示するTMP")]
    [SerializeField]
    private TextMeshProUGUI _limitTimeTMP = default;

    #endregion

    #region private
    private ReactiveProperty<uint> _currentLimitTime = new ReactiveProperty<uint>();
    #endregion

    #region Constant
    private const uint TIME_SECOND = 1;
    #endregion

    #region Event
    private IObservable<long> _currentTimeEvent;
    private Coroutine _currentCoroutine;
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
                             .Subscribe(_ => OnLimitTimerAsync());

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

    private void Update()
    {

    }
    #endregion

    #region public method
    #endregion

    #region private method
    private async UniTaskVoid OnLimitTimerAsync()
    {
        Debug.Log("タイムカウント開始");
        await UniTask.Delay(TimeSpan.FromSeconds(TIME_SECOND));

        while (_currentLimitTime.Value > 0)
        {
            _currentLimitTime.Value -= TIME_SECOND;
            await UniTask.Delay(TimeSpan.FromSeconds(TIME_SECOND));
        }
    }
    #endregion
}