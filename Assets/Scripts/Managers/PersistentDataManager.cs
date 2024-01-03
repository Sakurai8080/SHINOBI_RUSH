using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    #region property
    public static PersistentDataManager Instance { get; private set; }

    public uint FinalDefeatAmount { get; set; }
    public uint CurrentLimitTime { get; set; }
    #endregion

    #region serialize
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}