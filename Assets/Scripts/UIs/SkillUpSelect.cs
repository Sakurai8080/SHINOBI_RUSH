﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

public class SkillUpSelect : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    List<Button> _skillSelectUIs = default;

    [SerializeField]
    private CanvasGroup _SkillUpSelectGroup = default;

    [SerializeField]
    private GridLayoutGroup _skillUpSelectGrid = default;
    #endregion

    #region private
    /// <summary> 表示させるUIの数</summary>
    private int _activeAmount = 3;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Start()
    {
        GameManager.Instance.GameStartObserver
                    .TakeUntilDestroy(this)
                    .Subscribe(_ => ActiveRondomSkillUIs());

        ItemManager.Instance.ItemGetObserver
                   .TakeUntilDestroy(this)
                   .Subscribe(_ => ActiveRondomSkillUIs());
        

        for (int i = 0; i < _skillSelectUIs.Count; i++){
            SkillType type = (SkillType)i;
            Debug.Log(type);

            _skillSelectUIs[i].OnClickAsObservable()
                              .Subscribe(_ => OnSkill(type))
                              .AddTo(this);
        }
        CanvasGroupChange(false);
    }
    #endregion

    #region public method
    public void ActiveRondomSkillUIs()
    {
        int[] maxSkillIndices = SkillManager.Instance.Skills.Select((item,index) => new {Item = item , Index = index })
                                                            .Where(x => x.Item.CurrentSkillLevel >=5)
                                                            .Select(c => c.Index)
                                                            .ToArray();

        if (maxSkillIndices.Length == _skillSelectUIs.Count)
            return;
        else
        {
            var randomIndices = Enumerable.Range(0, _skillSelectUIs.Count)
                                          .Except(maxSkillIndices)
                                          .OrderBy(x => UnityEngine.Random.value)
                                          .Take(_activeAmount);

            int gridLeftAmount = (randomIndices.Count() >= 3) ? -131 : (randomIndices.Count() == 2) ? -0 : -0;
            _skillUpSelectGrid.padding.left = gridLeftAmount;

            foreach (var index in randomIndices)
            {
                _skillSelectUIs[index].gameObject.SetActive(true);
                Debug.Log($"獲得できるスキルは{index}");
            }
            CanvasGroupChange(true);

            Time.timeScale = 0f;
        }
        
    }
    #endregion

    #region private method
    private void OnSkill(SkillType type)
    {
        SkillManager.Instance.SetSkill(type);

        foreach (var skillUI in _skillSelectUIs)
            skillUI.gameObject.SetActive(false);

        CanvasGroupChange(false);
        Time.timeScale = 1;
    }

    private void CanvasGroupChange(bool change)
    {
        _SkillUpSelectGroup.alpha = Convert.ToInt32(change);
        _SkillUpSelectGroup.interactable = change;
        _SkillUpSelectGroup.blocksRaycasts = change;
    }
    #endregion
}