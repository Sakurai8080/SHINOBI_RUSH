using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Shuriken> ShurikanPool => _shurikenPool;
    #endregion

    #region serialize
    [SerializeField]
    private Shuriken _shurikenPrefab = default;
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