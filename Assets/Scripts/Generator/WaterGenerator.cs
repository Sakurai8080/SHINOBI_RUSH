using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 水遁に用いる水の生成機能
/// </summary>
public class WaterGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Water> WaterPool => _waterPool;
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("生成する水エフェクト")]
    [SerializeField]
    private Water _waterPrefab = default;

    [Tooltip("水を格納する親のからオブジェクト")]
    [SerializeField]
    private Transform _parent = default;
    #endregion

    #region private
    private Objectpool<Water> _waterPool;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _waterPool = new Objectpool<Water>(_waterPrefab, _parent); 
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}