using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// スキル表示したときのアニメーション管理
/// </summary>
public class SkillUIAnimation : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("アニメーションするグループ")]
    [SerializeField]
    private List<Image> _animationImageList;
    #endregion

    #region private
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void OnEnable()
    {
        foreach (var image in _animationImageList)
        {
            image.transform.localScale = Vector3.zero;
            SkillActiveAnimation(image);
        }
    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void SkillActiveAnimation(Image image)
    {
        image.transform.DOScale(1, 0.5f)
                       .SetEase(Ease.OutExpo)
                       .SetUpdate(true);
    }
    #endregion
}