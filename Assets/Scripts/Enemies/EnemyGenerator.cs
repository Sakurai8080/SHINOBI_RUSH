using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("変数")]
    [Tooltip("生成する間隔")]
    [SerializeField]
    private float _generateInterval = 1.0f;

    [Tooltip("一度に生成する数")]
    [SerializeField]
    private uint _onceGenerateAmount = 10;

    [Tooltip("生成する敵の限度")]
    [SerializeField]
    private uint _startGenerateLimit = 10;

    [Tooltip("各敵")]
    [SerializeField]
    private Enemy[] _enemies = default;
    #endregion

    #region private
    private Dictionary<EnemyType, Objectpool<EnemyBase>> _enemyPoolDic = new Dictionary<EnemyType, Objectpool<EnemyBase>>();
    private Dictionary<EnemyType, Coroutine> _generateCoroutineDic = new Dictionary<EnemyType, Coroutine>();
    private bool _isInGame = false;
    private uint _currentGenerateAmount;
    private uint _currentGenerateLimit;
    private Transform _playerTrans;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        SetUp();

        _isInGame = true;
    }

    private void Start()
    {
        
    }
    #endregion

    #region public method
     public void OnEnemyGenerate(EnemyType type)
    {
        Coroutine c = StartCoroutine(GenerateCoroutine(type));

        if (!_generateCoroutineDic.ContainsKey(type))
            _generateCoroutineDic.Add(type, c);
        else
            _generateCoroutineDic[type] = c;

    }
    #endregion

    #region private method
    private void SetUp()
    {
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemyPoolDic.Add(_enemies[i].EnemyPrefab.EnemyType, new Objectpool<EnemyBase>(_enemies[i].EnemyPrefab, _enemies[i].Parent));
        }

        _playerTrans = GameObject.FindGameObjectWithTag(GameTag.Player).transform;
        _currentGenerateAmount = _onceGenerateAmount;
        _currentGenerateLimit = _startGenerateLimit;
    }
    #endregion

    #region coroutine method
    private IEnumerator GenerateCoroutine(EnemyType type)
    {
        Debug.Log($"{type}ジェネレート開始");
        WaitForSeconds interval = new WaitForSeconds(_generateInterval);
        int count = 0;

        while(_isInGame)
        {
            count++;
            for (int i = 0; i < _currentGenerateAmount; i++)
            {
                EnemyBase enemy = _enemyPoolDic[type].Rent(_currentGenerateLimit);
                Debug.Log($"現在の生成カウント{count}");

                if (enemy != null)
                {
                    enemy.gameObject.SetActive(true);

                    float randomX = UnityEngine.Random.Range(-3, 3);
                    float randomY = UnityEngine.Random.Range(2, 5);

                    Vector3 generatePos = new Vector3(randomX,randomY, _playerTrans.transform.position.z+20);
                    enemy.transform.position = generatePos;

                    EnemyManager.Instance.NotifyEnemyCreated(enemy);
                }
            }
            yield return interval;
        }
    }
    #endregion
}

[Serializable]
public class Enemy
{
    public string EnemyName;
    public EnemyBase EnemyPrefab;
    public uint ReserveAmount;
    public uint ActivationLimit;
    public Transform Parent;
}