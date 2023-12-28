using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵を管理するManagerクラス
/// </summary>
//[RequireComponent(typeof))]
public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    #region property
    public static EnemyManager Instance { get; private set; }

    public ReactiveProperty<uint> DefeatAmount => _defeatAmountProperty;
    public IObservable<uint> DefeatedEnemyAmountViewObserver => _defeatedEnemyAmountViewSubject;

    public IObservable<EnemyBase> OnEnemyCreated => _onEnemyCreatedSubject;
    public IObservable<EnemyBase> OnEnemyDeactivated => _onEnemyDeactiveSubject;
    #endregion

    #region serialize
    #endregion

    #region private
    private EnemyWaveType _currentEnemyWave = EnemyWaveType.Wave_1;
    private EnemyGenerator _enemyGenerator;

    private Subject<EnemyBase> _onEnemyCreatedSubject = new Subject<EnemyBase>();
    private Subject<EnemyBase> _onEnemyDeactiveSubject = new Subject<EnemyBase>();
    #endregion

    #region Constant
    #endregion

    #region Event
    private ReactiveProperty<uint> _defeatAmountProperty = new ReactiveProperty<uint>();
    private Subject<uint> _defeatedEnemyAmountViewSubject = new Subject<uint>();
    #endregion

    #region unity methods
    private void Awake()
    {
        Instance = this;
        _enemyGenerator = GetComponent<EnemyGenerator>();
    }

    private void Start()
    {
        _defeatAmountProperty.TakeUntilDestroy(this)
                             .Subscribe(value =>
                             {
                                 _defeatedEnemyAmountViewSubject.OnNext(value);
                             });

        GameManager.Instance.GameStartObserver
                   .TakeUntilDestroy(this)
                   .Subscribe(_enemyGenerator=>
                   {
                      OnGenerateEnemies(EnemyWaveType.Wave_1);
                   });

        TimeManager.Instance.EnemyEventObserver
                            .TakeUntilDestroy(this)
                            .Subscribe(value => EnemySwitching(value));

    }
    #endregion

    #region public method
    public void EnemySwitching(uint enemyTypeAmount)
    {
        uint currentWave = enemyTypeAmount;
        _currentEnemyWave = (EnemyWaveType)currentWave;
        OnGenerateEnemies(_currentEnemyWave);
    }

    public void NotifyEnemyCreated(EnemyBase enemy)
    {
        _onEnemyCreatedSubject.OnNext(enemy);

        enemy.InactiveObserver
             .Subscribe(_ =>
             {
                 _onEnemyDeactiveSubject.OnNext(enemy);
             })
             .AddTo(this);
    }
    #endregion

   

    #region private method
    private void OnGenerateEnemies(EnemyWaveType type)
    {
        switch(type)
        {
            case EnemyWaveType.Wave_1:
                _enemyGenerator.OnEnemyGenerate(EnemyType.Wave1_Enemy1);
                _enemyGenerator.OnEnemyGenerate(EnemyType.Wave1_Enemy2);
                Debug.Log("Wave1開始");
                break;
            case EnemyWaveType.Wave_2:
                _enemyGenerator.OnEnemyGenerate(EnemyType.Wave2_Enemy1);
                _enemyGenerator.OnEnemyGenerate(EnemyType.Wave2_Enemy2);
                Debug.Log("Wave2開始");
                break;
            case EnemyWaveType.Wave_3:
                _enemyGenerator.OnEnemyGenerate(EnemyType.Wave3_Enemy1);
                _enemyGenerator.OnEnemyGenerate(EnemyType.Wave3_Enemy2);
                Debug.Log("Wave3開始");
                break;
            default:
                break;
        }
    }
    #endregion
}

public enum EnemyWaveType
{
    Wave_1,
    Wave_2,
    Wave_3
}