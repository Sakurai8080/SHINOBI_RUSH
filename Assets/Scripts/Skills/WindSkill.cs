using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 風遁を操作するコンポーネント
/// </summary>
public class WindSkill : SkillBase
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("風エフェクト")]
    [SerializeField]
    private Wind _wind = default;

    [Tooltip("強化した風エフェクト")]
    [SerializeField]
    private Wind _maxWind = default;

    [Tooltip("プレイヤーのポジション")]
    [SerializeField]
    private Transform _playerTransform = default;

    [Tooltip("アクティブ時の親オブジェクト")]
    [SerializeField]
    private Transform _activedParent = default;
    #endregion

    #region private
    private List<Transform> _enemies = new List<Transform>();
    private Vector3 _spawnPosition;
    private float _waitTime = 5.0f;
    private float _attackCoefficient = 5.0f;
    private Wind currentWind = default;
    private WindGenerator _windGenerator;
    private Vector3 _spwnUpPositionOffset = new Vector3(-0.2f, 0.3f, 0.1f);
    private Vector3 _spwnDownPositionOffset = new Vector3(0.2f, -0.3f, 0.1f);
    private Vector3 _initialPlayerPos;
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
    }

    private void Start()
    {
        _initialPlayerPos = _playerTransform.position;
        _spawnPosition = _playerTransform.position + _spwnUpPositionOffset;
        _windGenerator = GetComponent<WindGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
            _enemies.Add(other.GetComponent<Transform>());
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(_enemies.Count());
        if (other.CompareTag(GameTag.Enemy))
            _enemies.Remove(other.GetComponent<Transform>());
    }
    #endregion

    #region public method
    public override void OnSkillAction()
    {
        Debug.Log($"{SkillType}スキル発動");
        _isSkillActive = true;
        transform.SetParent(_playerTransform);
        _currentCoroutine = StartCoroutine(SkillActionCroutine());
        currentWind = _wind;
    }

    public override void SkillUp()
    {
        if (_currentSkillLevel >= MAX_LEVEL)
        {
            Debug.Log($"{SkillType}はレベル上限");
            return;
        }
        Debug.Log($"{SkillType}は{_currentSkillLevel}");
        _currentSkillLevel++;
        currentWind = (_currentSkillLevel >= 5) ? _maxWind : _wind; 
        AttackUpAmount(_attackCoefficient);
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
            Wind windObj = _windGenerator.WindPool.Rent();

            _isPlayerDown = _initialPlayerPos != _playerTransform.position ? true : false;
            Vector3 currentSpawnPos = (_isPlayerDown) ? _playerTransform.position + _spwnDownPositionOffset : _spawnPosition;
            if (windObj != null)
            {
                windObj.transform.position = currentSpawnPos;
                yield return new WaitForSeconds(_waitTime);
                windObj.gameObject.SetActive(true);
                yield return new WaitUntil(() => _enemies?.Count() >= 1);
                Vector3 currentTransform = SetTarget(targetDir);
                windObj.SetAttackAmount(_currentAttackAmount);
                windObj.SetVelocity(currentTransform);
                windObj.transform.SetParent(_activedParent);
            }
        }
    }
    #endregion
}