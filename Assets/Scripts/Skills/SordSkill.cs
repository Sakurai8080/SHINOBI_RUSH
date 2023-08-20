﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SordSkill : SkillBase
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private Sord _sord = default;

    [SerializeField]
    private Transform _sordParent = default;
    #endregion

    #region private
    private float _attackCoefficient = 2.0f;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    public override void OnSkillAction()
    {
        Debug.Log($"{SkillType}を発動");
        _isSkillActive = true;

        Sord sord = Instantiate(_sord, _sordParent);
    }

    public override void SkillUp()
    {
        if (_currentSkillLevel >= MAX_LEVEL)
        {
            Debug.Log($"{SkillType}はレベル上限です");
            return;
        }

        _currentSkillLevel++;
        Debug.Log($"{SkillType}は{_currentSkillLevel}");

        AttackUpAmount(_attackCoefficient);
    }

    public override void AttackUpAmount(float coefficient)
    {
        _currentAttackAmount *= coefficient;
    }
    #endregion

    #region private method
    #endregion

    #region protected method
    protected override IEnumerator SkillActionCroutine()
    {
        yield return null;
    }
    #endregion
}