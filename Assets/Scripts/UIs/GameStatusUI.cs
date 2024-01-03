using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using TMPro;
using DG.Tweening;

/// <summary>
/// ゲーム全体の状況を明示するUI
/// </summary>
public class GameStatusUI : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("討伐数を表示するTMP")]
    [SerializeField]
    private TextMeshProUGUI _defeatTMP = default;

    [SerializeField]
    private CanvasGroup _gameStatusGroup = default;
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Start()
    {
        EnemyManager.Instance.DefeatedEnemyAmountViewObserver
                             .TakeUntilDestroy(this)
                             .Subscribe(value => ViewDefeatedAmount(value));
    }
    #endregion

    #region public method
    public void ChangeActivePanelView(bool value)
    {
       _gameStatusGroup.alpha = Convert.ToInt32(value);
    }

    public void GameStatusUIAnimation()
    {
        AnimateHp(_gameStatusGroup);
    }
    #endregion

    #region private method
    private void ViewDefeatedAmount(uint amount)
    {
        _defeatTMP.text = $"{amount}";
    }

    private void AnimateHp(CanvasGroup targetCanvasGroup)
    {
        targetCanvasGroup.DOFade(1f, 3.5f).From(0f);
    }
    #endregion
}