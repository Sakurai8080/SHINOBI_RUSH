﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>, IDamagable
{
    #region property
    public PlayerHealth Health => _health;
    public PlayerStatus Status => _status;
    #endregion

    #region serialize
    #endregion

    #region private
    private PlayerHealth _health;
    private PlayerStatus _status;
    private bool _isDead = false;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
    }
    #endregion

    #region public method
    public void Damage(float amount)
    {
        if (_isDead)
        {
            return;
        }
        Debug.Log($"プレイヤーが{amount}ダメージを受けた");
        //ダメージを受けたあと、プレイヤーのHPがなくなったら
        if (_health.Damage(amount))
        {
            if (!_isDead)
            {
                _isDead = true;
            }
        }
    }
    #endregion

    #region private method
    #endregion
}