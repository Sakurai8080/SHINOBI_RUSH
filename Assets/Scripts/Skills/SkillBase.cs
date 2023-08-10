using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    #region property
    public SkillType SkillType => _skillData.SkillType;
    public bool IsSkillActived => _isSkillActive;
    public int CurrentSkillLevel => _currentSkillLevel;
    #endregion

    #region serialize
    [Header("変数")]
    [Tooltip("スキルの種類")]
    [SerializeField]
    private SkillData _skillData = default;
    #endregion

    #region protected
    protected float _currentAttackAmount = 0;
    protected int _currentSkillLevel = 1;
    protected bool _isSkillActive = false;
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

    private void Start()
    {

    }

    private void Update()
    {

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
    /// <param name="coefficient"></param>
    public abstract void AttackUpAmount(float coefficient);
    #endregion

    #region coroutine method
    protected abstract IEnumerator SkillActionCroutine();
    #endregion
}