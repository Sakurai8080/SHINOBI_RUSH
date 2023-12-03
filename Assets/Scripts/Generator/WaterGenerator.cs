using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    #region property
    public Objectpool<Water> WaterPool => _waterPool;
    #endregion

    #region serialize
    [SerializeField]
    private Water _waterPrefab = default;

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