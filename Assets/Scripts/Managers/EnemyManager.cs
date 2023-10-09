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
    #endregion

    #region serialize
    #endregion

    #region private
    private EnemyGenerator _enemyGenerator;
    #endregion

    #region Constant
    #endregion

    #region Event
    private ReactiveProperty<uint> _defeatAmountProperty = new ReactiveProperty<uint>();
    #endregion

    #region unity methods
    private void Awake()
    {
        _enemyGenerator = GetComponent<EnemyGenerator>();
    }

    private void Start()
    {
        GameManager.Instance.GameStartObserver
                   .TakeUntilDestroy(this)
                   .Subscribe(_enemyGenerator=>
                   {
                      OnGenerateEnemies(EnemyWaveType.Wave_1);
                   });

    }

    private void Update()
    {

    }
    #endregion

    #region public method
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