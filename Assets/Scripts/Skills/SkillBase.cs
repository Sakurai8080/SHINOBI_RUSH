using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全スキルに共通する基底クラス
/// </summary>
public abstract class SkillBase : MonoBehaviour
{
    #region property
    public SkillType SkillType => _skillData.SkillType;
    public bool IsSkillActived => _isSkillActive;
    public int CurrentSkillLevel => _currentSkillLevel;
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("スキルの種類")]
    [SerializeField]
    private SkillData _skillData = default;
    #endregion

    #region protected
    protected float _currentAttackAmount = 0;
    protected int _currentSkillLevel = 1;
    protected bool _isSkillActive = false;
    protected Coroutine _currentCoroutine;
    #endregion

    #region private
    #endregion

    #region Constant
    protected const int MAX_LEVEL = 5;
    #endregion

    #region Event
    #endregion


    #region unity methods
    protected virtual void Awake()
    {
        AttackSet();
    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void AttackSet()
    {
        _currentAttackAmount = _skillData.AttackAmount;
    }
    #endregion

    #region abstract method
    /// <summary>
    /// スキル発動時の効果
    /// </summary>
    public abstract void OnSkillAction();

    /// <summary>
    /// スキルレベルアップ時の効果
    /// </summary>
    public abstract void SkillUp();

    /// <summary>
    /// スキルアップ時の攻撃力アップ
    /// </summary>
    /// <param name="coefficient">攻撃力に対する係数</param>
    public abstract void AttackUpAmount(float coefficient);
    #endregion

    #region coroutine method
    protected abstract IEnumerator SkillActionCroutine();
    #endregion
}