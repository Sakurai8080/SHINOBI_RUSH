using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


/// <summary>
/// スキル発動テスト用
/// </summary>
public class SkillTest : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
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
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SkillManager.Instance.SetSkill(SkillType.Shuriken);
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    SkillManager.Instance.SetSkill(SkillType.Fire);
                }
                if (Input.GetKeyDown(KeyCode.H))
                {
                    SkillManager.Instance.SetSkill(SkillType.Wind);
                }
                if (Input.GetKeyDown(KeyCode.J))
                {
                    SkillManager.Instance.SetSkill(SkillType.Sord);
                }
                if (Input.GetKeyDown(KeyCode.K))
                {
                    SkillManager.Instance.SetSkill(SkillType.Water);
                }
            });
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}