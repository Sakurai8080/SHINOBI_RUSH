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
                         GameReplay();
                     });

        _titleTransitionButton.OnClickAsObservable()
                              .TakeUntilDestroy(this)
                              .Subscribe(_ =>
                              {
                                  TitleTransition();
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
            Debug.Log("Try");
            _finalDefeatAmount = PersistentDataManager.Instance.FinalDefeatAmount;
            _remainingTime = PersistentDataManager.Instance.CurrentLimitTime;
            _remainingTimeTMP.text = $"{_remainingTime / 60:00}:{(_remainingTime % 60):00}";
            await UniTask.Delay(TimeSpan.FromSeconds(1));

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
                                             .SetEase(Ease.InOutQuad)
                                             .SetUpdate(true)
                                             .AsyncWaitForCompletion();

            _replayButton.gameObject.SetActive(true);
            _titleTransitionButton.gameObject.SetActive(true);
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

    private void GameReplay()
    {
        SceneManager.LoadScene("InGame");
    }

    private void TitleTransition()
    {
        SceneManager.LoadScene("Title");
    }
    #endregion
}