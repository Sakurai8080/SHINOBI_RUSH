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
    #endregion

    #region private
    private Transform _spawnPosition;
    private float _attackCoefficient = 5.0f;
    private Wind _currentWind = default;
    private Vector3 _spawnPositionOffset = new Vector3(0f, 0f, 1f);
    private float _sizechangeCoefficient = 1.5f;
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
    #endregion

    #region public method
    public override void OnSkillAction()
    {
        Debug.Log($"{SkillType}スキル発動");
        _isSkillActive = true;
        transform.SetParent(_playerTransform);
        CreateWind(_wind);
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
        AttackUpAmount(_attackCoefficient);
        if (_currentSkillLevel == 4)
            CreateWind(_maxWind);
        else
            _currentWind.SizeUp(_sizechangeCoefficient);
    }

    public override void AttackUpAmount(float coefficient)
    {
        _currentAttackAmount *= coefficient;
    }
    #endregion

    #region private method
    private void CreateWind(Wind wind)
    {
        _currentWind?.gameObject?.SetActive(false);
        _currentWind = Instantiate(wind,transform);
    }
    #endregion

    #region coroutine method
    protected override IEnumerator SkillActionCroutine()
    {
        yield return null;
    }
    #endregion
}