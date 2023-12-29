using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火遁に用いる炎を生成する機能
/// </summary>
public class KatonGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Katon> KatonPool => _katonPool;
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("生成する炎エフェクト")]
    [SerializeField]
    private Katon _katonPrefab = default;

    [Tooltip("炎を格納する親のからオブジェクト")]
    [SerializeField]
    private Transform _parant = default;
    #endregion

    #region private
    private Objectpool<Katon> _katonPool;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _katonPool = new Objectpool<Katon>(_katonPrefab, _parant);
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}