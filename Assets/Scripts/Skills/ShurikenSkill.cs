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
        transform.SetParent(_playerTransform);
        _spawnPosition = _playerTransform.position + new Vector3(0f, 0.1f, 0.1f);
        OnSkillAction();
    }

    private void Update()
    {
       Debug.Log(_enemies.Count());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            _enemies.Add(other.GetComponent<Transform>());
            _isSkillActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            Debug.Log("出た");
            _enemies.Remove(other.GetComponent<Transform>());
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
            Debug.Log("コルーチンスタート");
            if (_enemies?.Count > 0)
            {
                Debug.Log("起動");

                Shuriken shuriken = Instantiate(_shuriken, _spawnPosition,Quaternion.identity);
                Vector3 currentTransform = SetTarget(targetDir);
                Debug.Log(currentTransform);
                shuriken.SetVelocity(currentTransform);
                shuriken.SetAttackAmount(_currentAttackAmount);
            }
            yield return new WaitForSeconds(_waitTime);
            Debug.Log("コルーチンエンド");
        }
    }
    #endregion
}