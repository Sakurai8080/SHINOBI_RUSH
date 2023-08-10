using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルのデータをまとめたスクリタブルオブエジェクト
/// </summary>
[CreateAssetMenu(menuName ="MyScritable/SkillData")]
public class SkillData : ScriptableObject
{
    #region property
    public SkillType SkillType => _skillType;
    public float AttackAmount => _attackAmount;
    public float CorrectionValue => _correctionValue;
    #endregion

    #region serialize
    [Header("変数")]
    [Tooltip("スキルの種類")]
    [SerializeField]
    private SkillType _skillType = default;

    [SerializeField]
    private float _attackAmount = 1.0f;

    [SerializeField]
    private float _correctionValue = 1.0f;
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
    #endregion

    #region private method
    #endregion
}
/// <summary>
/// 各スキルタイプ
/// </summary>
public enum SkillType
{
    Shuriken,
    ShadowBall,
    Kagebunsin,
    Katon
}