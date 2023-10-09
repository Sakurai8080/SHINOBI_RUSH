using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>, IDamagable
{
    #region property
    public PlayerHealth Health => _health;
    #endregion

    #region serialize
    #endregion

    #region private
    private PlayerHealth _health;
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

    private void Start()
    {

    }

    private void Update()
    {

    }
    #endregion

    #region public method
    public void Damage(float amount)
    {
        Debug.Log($"プレイヤーが{amount}ダメージを受けた");
        //ダメージを受けたあと、プレイヤーのHPがなくなったら
        if (_health.Damage(amount))
        {
        }
    }
    #endregion

    #region private method
    #endregion
}