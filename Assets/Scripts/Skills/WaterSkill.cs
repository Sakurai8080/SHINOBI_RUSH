using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkill : SkillBase
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private Water _water = default;

    [SerializeField]
    private Transform _playerTransform = default;
    #endregion

    #region private
    private Vector3 _randomPosition;

    private float _waitTime = 3.0f;

    private float _attackCoefficient = 2.0f;

    private Water _waterball = default;
    private WaterGenerator _waterGenerator;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    protected override void Awake()
    {
        base.Awake();

        _waterGenerator = GetComponent<WaterGenerator>();
    }

    private void Start()
    {
        transform.SetParent(_playerTransform);
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
        _waitTime *= 0.5f;
        AttackUpAmount(_attackCoefficient);
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
        while (_isSkillActive)
        {
            Water waterObj = _waterGenerator.WaterPool.Rent();

            if (waterObj != null)
            {
                float randomPosX = Random.Range(-3f, 3f);
                float randomPosY = Random.Range(0f, 2f);
                _randomPosition = new Vector3(randomPosX, randomPosY, 0);
                waterObj.transform.position = _randomPosition;
                waterObj.gameObject.SetActive(true);
                waterObj.SetAttackAmount(_currentAttackAmount);
                yield return new WaitForSeconds(_waitTime);
            }
        }
    }
    #endregion
}