using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : SingletonMonoBehaviour<SkillManager>
{
    #region property
    public SkillBase[] Skills => _skills;
    #endregion

    #region serialize
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
    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }
    #endregion

    #region public method
    public void SetSkill(SkillType type)
    {
        Debug.Log($"発動スキル{type}");
        SkillBase skill = _skills.FirstOrDefault(x => x.SkillType == type);

        if (!skill.IsSkillActived)
        {
            skill.OnSkillAction();
        }
        else
        {
            skill.SkillUp();
        }
    }
    #endregion

    #region private method
    #endregion
}