
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

/// <summary>
/// UI周りをまとめるコンポーネント
/// </summary>
[RequireComponent(typeof(PlayerStatusUI))]
[RequireComponent(typeof(GameStatusUI))]
public class HUDManager : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private PlayerStatusUI _playerStatus;
    private GameStatusUI _gameStatus;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        _playerStatus = GetComponent<PlayerStatusUI>();
        _gameStatus = GetComponent<GameStatusUI>();
    }

    private void Start()
    {
        GameManager.Instance.GameStartObserver
                            .TakeUntilDestroy(this)
                            .Subscribe(_ =>
                            {
                                _playerStatus.PlayerHpAnimation();
                                _gameStatus.GameStatusUIAnimation();
                            });
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}