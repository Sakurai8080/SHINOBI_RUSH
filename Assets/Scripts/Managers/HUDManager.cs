using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


[RequireComponent(typeof(PlayerStatusUI))]
public class HUDManager : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private PlayerStatusUI _playerStatus;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _playerStatus = GetComponent<PlayerStatusUI>();
    }

    private void Start()
    {
        GameManager.Instance.GameStartObserver
                            .TakeUntilDestroy(this)
                            .Subscribe(_ =>
                            {
                                ChangeHUDPanelActive(true);
                                _playerStatus.PlayerHpAnimation();
                            });
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void ChangeHUDPanelActive(bool value)
    {
        //_playerStatus.ChangeActivePanelView(value);
    }
    #endregion
}