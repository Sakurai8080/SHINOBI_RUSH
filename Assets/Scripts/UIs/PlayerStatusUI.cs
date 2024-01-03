using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UniRx;

/// <summary>
/// プレイヤーの情報を表示するためのUI
/// </summary>
public class PlayerStatusUI : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("プレイヤーステータスのCanvasGroup")]
    [SerializeField]
    private CanvasGroup _playerStatusGroup = default;

    [Tooltip("現在のHPの明示")]
    [SerializeField]
    private Image _currentHPFillArea = default;
    #endregion

    #region private
    private Tween _currentTween;
    #endregion
    
    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Start()
    {
        //プレイヤーのHPの値が変化した時の処理を登録
        PlayerController.Instance.Health.ChangeHPObserver
                                        .TakeUntilDestroy(this)
                                        .Subscribe(amount => CurrentHPView(amount));
    }
    #endregion

    #region public method
    public void ChangeActivePanelView(bool value)
    {
        _playerStatusGroup.alpha = Convert.ToInt32(value);
    }

    public void PlayerHpAnimation()
    {
        AnimateHp(_playerStatusGroup);
    }
    #endregion

    #region private method

    private void CurrentHPView(float amount)
    {
        _currentHPFillArea.fillAmount = amount;
    }

    private void AnimateHp(CanvasGroup targetCanvasGroup)
    {
        _currentTween =  targetCanvasGroup.DOFade(1f, 3.5f).From(0f);
    }
    #endregion
}