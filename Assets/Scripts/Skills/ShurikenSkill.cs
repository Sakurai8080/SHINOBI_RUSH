using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShurikenSkill : SkillBase
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private Shuriken _shuriken = default;

    [SerializeField]
    private Transform _playerTransform = default;

    #endregion

    #region private
    private List<Transform> _enemies = new List<Transform>();

    private Vector3 _spawnPosition;

    private float _waitTime = 3.0f;

    private float attackCoefficient = 2.0f;

    private Vector3 _playerV3 = default;

    private ShurikenGenerator _shurikenGenerator;
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
        _spawnPosition = _playerTransform.position + new Vector3(0f, 0.1f, 0.1f);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            _enemies.Add(other.GetComponent<Transform>());
            Debug.Log($"手裏剣スキルの射程圏内{_enemies.Count()}匹");
            if (!_isSkillActive)
            {
                OnSkillAction();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            _enemies.Remove(other.GetComponent<Transform>());
            Debug.Log($"射程内残{_enemies.Count()}匹(手裏剣)");
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
        Vector3 targetDir = Vector3.zero;
        while (_isSkillActive)
        {
            Shuriken srknObj = _shurikenGenerator.ShurikanPool.Rent();
            if (_enemies?.Count > 0 && srknObj != null)
            {
                Vector3 currentTransform = SetTarget(targetDir);
                srknObj.transform.position = transform.position;
                srknObj.gameObject.SetActive(true);
                srknObj.SetVelocity(currentTransform);
                srknObj.SetAttackAmount(_currentAttackAmount);
            }
            yield return new WaitForSeconds(_waitTime);
        }
    }
    #endregion
}