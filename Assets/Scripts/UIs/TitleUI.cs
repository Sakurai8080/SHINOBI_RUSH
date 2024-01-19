using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

/// <summary>
/// タイトル画面のUIを操作するコンポーネント
/// </summary>
public class TitleUI : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("スタートボタン")]
    [SerializeField]
    private Button _inGameLoadButton;
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
        _inGameLoadButton.OnClickAsObservable()
                         .TakeUntilDestroy(this)
                         .Subscribe(_ =>
                         {
                             InGameSwitch();
                         });

        _currentTween = _inGameLoadButton.transform.DOScale(0.9f, 1f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void InGameSwitch()
    {
        _currentTween.Kill();
        GameManager.Instance.SceneLoader("InGame");
    }
    #endregion

    #region coroutine method
    #endregion
}