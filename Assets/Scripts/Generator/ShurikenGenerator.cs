using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手裏剣を生成する機能
/// </summary>
public class ShurikenGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Shuriken> ShurikanPool => _shurikenPool;
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("生成する手裏剣プレハブ")]
    [SerializeField]
    private Shuriken _shurikenPrefab = default;

    [Tooltip("手裏剣を格納する親オブジェクト")]
    [SerializeField]
    private Transform _parent = default;
    #endregion

    #region private
    private Objectpool<Shuriken> _shurikenPool;
    #endregion

    #region unity methods
    private void Awake()
    {
        _shurikenPool = new Objectpool<Shuriken>(_shurikenPrefab, _parent);
    }
    #endregion
}