using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 火遁を操作するコンポーネント
/// </summary>
public class KatonSkill : SkillBase
{
    #region property
    #endregion

    #region serialize

    [Header("Variable")]
    [Tooltip("プレイヤーのポジション")]
    [SerializeField]
    private Transform _playerTransform = default;
    #endregion

    #region private
    private Vector3 _spawnPosition;
    private float _waitTime = 3.0f;
    private float _attackCoefficient = 2.0f;
    private float _scaleCoefficient = 1.0f;
    private KatonGenerator _katonGenerator;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    protected override void Awake()
    {
        base.Awake();
        _katonGenerator = GetComponent<KatonGenerator>();
    }

    private void Start()
    {
        transform.SetParent(_playerTransform);
        _spawnPosition = _playerTransform.position + new Vector3(0f, 0.1f, 0.1f);
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
        _scaleCoefficient += 2.5f;
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
            Katon ktnObj = _katonGenerator.KatonPool.Rent();
            if (ktnObj != null)
            {
                ktnObj.transform.position = _spawnPosition;
                ktnObj.gameObject.SetActive(true);
                ktnObj.SetAttackAmount(_currentAttackAmount);
                ktnObj.SizeChange(_scaleCoefficient);
                yield return new WaitForSeconds(_waitTime);
            }
        }
    }
    #endregion
}