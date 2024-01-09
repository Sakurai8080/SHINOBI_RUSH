using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

/// <summary>
/// 手裏剣スキルを操作するコンポーネント
/// </summary>
public class ShurikenSkill : SkillBase
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("手裏剣オブジェクト")]
    [SerializeField]
    private Shuriken _shuriken = default;

    [Tooltip("プレイヤーのポジション")]
    [SerializeField]
    private Transform _playerTransform = default;

    [Tooltip("手裏剣が飛んだあとの親オブジェクト")]
    [SerializeField]
    private Transform _shurikenParent = default;
    #endregion

    #region private
    private List<Transform> _enemies = new List<Transform>();
    private Vector3 _spawnUpPosition;
    private Vector3 _spawnDownPosition;
    private Vector3 _initialPlayerPos;
    private float _waitTime = 3.0f;
    private float attackCoefficient = 2.0f;
    private Coroutine _currentCoroutine;
    private ShurikenGenerator _shurikenGenerator;
    private bool _isPlayerDown = false;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    protected override void Awake()
    {
        base.Awake();
        _shurikenGenerator = GetComponent<ShurikenGenerator>();
    }

    private void Start()
    {
        transform.position = _playerTransform.position;
        _initialPlayerPos = _playerTransform.position;
        _spawnUpPosition = _playerTransform.localPosition + new Vector3(0f, 0.2f, 0.1f);
        _spawnDownPosition = _playerTransform.localPosition + new Vector3(0f, -0.2f, 0.1f);


        EnemyManager.Instance.OnEnemyCreated
                    .Subscribe(enemy =>
                    {
                        _enemies.Add(enemy.transform);
                    })
                    .AddTo(this);

        EnemyManager.Instance.OnEnemyDeactivated
                    .Subscribe(enemy =>
                    {
                        _enemies.Remove(enemy.transform);
                    })
                    .AddTo(this);
    }

    private void OnEnable()
    {
        _currentCoroutine = StartCoroutine(SkillActionCroutine());
    }

    private void OnDisable()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(SkillActionCroutine());
            _currentCoroutine = null;
        }
    }
    #endregion

    #region public method
    public override void OnSkillAction()
    {
        Debug.Log($"{SkillType}スキル発動");
        _isSkillActive = true;
        transform.SetParent(_playerTransform);
        _currentCoroutine = StartCoroutine(SkillActionCroutine());

    }

    public override void SkillUp()
    {
        if (_currentSkillLevel >= MAX_LEVEL)
        {
            Debug.Log($"{SkillType}はレベル上限");
            return;
        }

        Debug.Log($"{SkillType}レベルアップ");
        _currentSkillLevel++;
        AttackUpAmount(attackCoefficient);
        _waitTime -= 0.6f;
    }

    public override void AttackUpAmount(float coefficient)
    {
        _currentAttackAmount *= coefficient;
    }
    #endregion

    #region private method
    private Vector3 SetTarget(Vector3 targetDir)
    {
        Transform nearestEnemy = _enemies.First();
        float distance = float.MaxValue;

        foreach (Transform enemyTransform in _enemies)
        {
            float currentDistance = Vector3.Distance(transform.position, enemyTransform.position);
            if (currentDistance < distance)
            {
                nearestEnemy = enemyTransform;
                distance = currentDistance;
            }
        }
        return targetDir = (nearestEnemy.position - transform.position);
    }
    #endregion

    #region coroutine method
    protected override IEnumerator SkillActionCroutine()
    {
        while (_isSkillActive)
        {
            Vector3 targetDir = Vector3.zero;
            _isPlayerDown = _initialPlayerPos != _playerTransform.position ? true : false;
            targetDir = SetTarget(targetDir);
            if (_enemies?.Count > 0 && 5 >= targetDir.z - _playerTransform.position.z)
            {
                Shuriken srknObj = _shurikenGenerator.ShurikanPool.Rent();
                if (srknObj != null)
                {
                    srknObj.transform.position = (_isPlayerDown) ? _spawnDownPosition : _spawnUpPosition;
                    srknObj.gameObject.SetActive(true);
                    srknObj.SetVelocity(targetDir);
                    srknObj.SetAttackAmount(_currentAttackAmount);
                    srknObj.transform.SetParent(_shurikenParent);
                }
            }
            yield return new WaitForSeconds(_waitTime);
        }
    }
    #endregion
}