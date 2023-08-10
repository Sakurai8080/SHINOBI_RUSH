using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenSkill : SkillBase
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private Shuriken _shuriken = default;

    [SerializeField]
    private Transform _playerTransform = default;

    [SerializeField]
    private Transform _testTrans = default;
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
        _enemies.Add(_testTrans.GetComponent<Transform>());
    }

    private void Update()
    {

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
    #endregion

    #region coroutine method
    protected override IEnumerator SkillActionCroutine()
    {
        GameObject shuriken = Instantiate(_shuriken.gameObject, _playerTransform);

        while (true)
        {
            if (_enemies?.Count > 0)
            {
                Debug.Log("起動");

                shuriken.transform.position = Vector3.MoveTowards(shuriken.transform.position , _testTrans.position, _moveSpeed * Time.deltaTime);
            }
            yield return null;
        }

    }
    #endregion

}