using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

/// <summary>
/// リザルト画面のUIを管理するコンポーネント
/// </summary>
public class ResultUI : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("Tweenのアニメーション時間")]
    [SerializeField]
    private float _viewAnimTime = 1.5f;

    [Tooltip("討伐数を表示するTMP")]
    [SerializeField]
    private TextMeshProUGUI _defeatAmountTMP = default;

    [Tooltip("残り時間を表示するTMP")]
    [SerializeField]
    private TextMeshProUGUI _remainingTimeTMP = default;

    [Tooltip("リプレイボタン")]
    [SerializeField]
    private Button _replayButton = default;

    [Tooltip("タイトル画面へ移動")]
    [SerializeField]
    private Button _titleTransitionButton = default;

    [Tooltip("リザルトUIをまとめたUIグループ")]
    [SerializeField]
    private CanvasGroup _resultUIGroup = default;
    #endregion

    #region private
    private uint _finalDefeatAmount;
    private uint _currentDefeatAmount;
    private uint _remainingTime;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        ResultUISetup();
    }

    private void Start()
    {
        _replayButton.OnClickAsObservable()
                     .TakeUntilDestroy(this)
                     .Subscribe(_ =>
                     {
                         SceneSwicher("InGame");
                     });

        _titleTransitionButton.OnClickAsObservable()
                              .TakeUntilDestroy(this)
                              .Subscribe(_ =>
                              {
                                  SceneSwicher("Title");
                              });
    }
    #endregion

    #region public method
    #endregion

    #region private method
    private async UniTask OnResultAsync(CancellationToken token)
    {
        try
        {
            _finalDefeatAmount = PersistentDataManager.Instance.FinalDefeatAmount;
            _remainingTime = PersistentDataManager.Instance.CurrentLimitTime;
            _remainingTimeTMP.text = $"{_remainingTime / 60:00}:{(_remainingTime % 60):00}";
            _remainingTimeTMP.transform.localScale = Vector3.zero;

            await DOTween.To(() =>
                          _currentDefeatAmount,
                          x => _currentDefeatAmount = x,
                          _finalDefeatAmount,
                          _viewAnimTime)
                          .OnUpdate(() =>
                          {
                              _defeatAmountTMP.text = _currentDefeatAmount.ToString();
                          })
                          .AsyncWaitForCompletion();

            await _remainingTimeTMP.transform.DOScale(Vector3.one, 1f)
                                             .SetEase(Ease.OutBounce)
                                             .SetUpdate(true)
                                             .AsyncWaitForCompletion();

            ButtonAnimation(_replayButton);
            ButtonAnimation(_titleTransitionButton);
        }
        catch (Exception e)
        {
            Debug.LogError($"OnResultAsyncにエラーが発生: {e}");
        }
    }

    private void ResultUISetup()
    {
        
        _resultUIGroup.alpha = 0;
        _replayButton.gameObject.SetActive(false);
        _titleTransitionButton.gameObject.SetActive(false);
        _resultUIGroup.DOFade(1f, 2f)
                      .OnComplete(()=>
                       OnResultAsync(this.GetCancellationTokenOnDestroy()).Preserve().Forget());
    }

    private void ButtonAnimation(Button button)
    {
        CanvasGroup buttonGroup = button.gameObject.GetComponent<CanvasGroup>();
        if (buttonGroup == null)
            buttonGroup = button.gameObject.AddComponent<CanvasGroup>();

        buttonGroup.alpha = 0;
        button.gameObject.SetActive(true);
        Tween currenTween = buttonGroup.DOFade(1, 0.7f)
                                       .OnComplete(async () =>
                                       {
                                            await UniTask.Delay(TimeSpan.FromMilliseconds(300));
                                            button.transform.DOScale(0.95f, 1f)
                                            .SetEase(Ease.InQuint)
                                            .SetLoops(-1,LoopType.Yoyo);
                                       });
    }

    private void SceneSwicher(string sceneName)
    {
        GameManager.Instance.SceneLoader(sceneName);
    }
    #endregion
}