using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 風遁に用いる風エフェクトの生成機能
/// </summary>
public class WindGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Wind> WindPool => _windPool;
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("生成する風エフェクト")]
    [SerializeField]
    private Wind _windPrefab = default;

    [Tooltip("風を格納する親の空オブジェクト")]
    [SerializeField]
    private Transform _parent = default;
    #endregion

    #region private
    private Objectpool<Wind> _windPool;
    #endregion

    #region unity methods
    private void Awake()
    {
        _windPool = new Objectpool<Wind>(_windPrefab, _parent);
    }
    #endregion
}