using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Wind> WindPool => _windPool;
    #endregion

    #region serialize
    [SerializeField]
    private Wind _windPrefab = default;


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