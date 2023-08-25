using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatonGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Katon> KatonPool => _katonPool;
    #endregion

    #region serialize
    [SerializeField]
    private Katon _katonPrefab = default;

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