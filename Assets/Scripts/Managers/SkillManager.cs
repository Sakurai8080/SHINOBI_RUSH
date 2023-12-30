using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// スキルを管理するクラス
/// </summary>
public class SkillManager : SingletonMonoBehaviour<SkillManager>
{
    #region property
    public SkillBase[] Skills => _skills;
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("各スキル")]
    [SerializeField]
    private SkillBase[] _skills = default;
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    #endregion

    #region public method
    public void SetSkill(SkillType type)
    {
        Debug.Log($"発動スキル{type}");
        SkillBase skill = _skills.FirstOrDefault(x => x.SkillType == type);

        if (!skill.IsSkillActived)
            skill.OnSkillAction();
        else
            skill.SkillUp();
    }
    #endregion

    #region private method
    #endregion
}