using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UniRx;

public class PlayerStatusUI : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Tooltip("プレイヤーステータスのCanvasGroup")]
    [SerializeField]
    private CanvasGroup _playerStatusGroup = default;

    [SerializeField]
    private Image _currentHPFillArea = default;

    [SerializeField]
    private Image _playerHpImageBackGround;
    #endregion

    #region private
    private Tween _currentTween;
    #endregion
    
    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {

    }

    private void Start()
    {
        //プレイヤーのHPの値が変化した時の処理を登録
        PlayerController.Instance.Health.ChangeHPObserver
                                        .TakeUntilDestroy(this)
                                        .Subscribe(amount => CurrentHPView(amount));
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    #endregion

    #region public method
    public void ChangeActivePanelView(bool value)
    {
        _playerStatusGroup.alpha = Convert.ToInt32(value);
    }

    public void PlayerHpAnimation()
    {
        AnimateHp(_currentHPFillArea.transform);
        AnimateHp(_playerHpImageBackGround.transform);;
    }
    #endregion

    #region private method

    private void CurrentHPView(float amount)
    {
        _currentHPFillArea.fillAmount = amount;
    }

    private void AnimateHp(Transform target)
    {
        target.transform.DOScale(Vector3.one, 0.15f)
                                               .SetEase(Ease.InOutBounce)
                                               .SetUpdate(true)
                                               .OnComplete(() =>
                                               {
                                                    //ChangeActivePanelView(false);
                                               });
    }
    #endregion
}