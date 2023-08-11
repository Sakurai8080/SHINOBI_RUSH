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

    private float _moveSpeed = 1.0f;
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
        StartCoroutine(SkillActionCroutine());
        transform.SetParent(_playerTransform);
    }

    private void Update()
    {
        Debug.Log(_enemies.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.Enemy))
        {
            Debug.Log("エネミー入った");
            _enemies.Add(other.GetComponent<Transform>());
        }
    }
    #endregion

    #region public method
    public override void OnSkillAction()
    {
        _isSkillActive = true;
        transform.SetParent(_playerTransform);
    }

    public override void SkillUp()
    {
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
        return targetDir = (nearestEnemy.position - transform.position).normalized;
    }
    #endregion

    #region coroutine method
    protected override IEnumerator SkillActionCroutine()
    {
        if (_enemies?.Count > 0)
        {
            GameObject shuriken = Instantiate(_shuriken.gameObject, _playerTransform);

            while (_isSkillActive)
            {
                Vector3 currentTransform = SetTarget(Vector3.zero);

                if (_enemies?.Count > 0)
                {
                    Debug.Log("起動");

                    shuriken.transform.position = Vector3.MoveTowards(shuriken.transform.position, currentTransform, _moveSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }

    }
    #endregion

}