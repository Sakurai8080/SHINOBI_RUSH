using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 巻物で経験値アップ
/// </summary>
public class Scroll : ItemBase
{
    #region property

    #endregion

    #region serialize
    [Tooltip("スピードを操作する係数")]
    [SerializeField]
    private float _transitionMulti = 1;
    #endregion

    #region private
    private float _zPositionSpeed = 2f;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                ScrollMoveCoroutine();
            });
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
    #endregion

    #region public method
    public override void Return()
    {
        //座標をリセットして非表示にする。
        gameObject.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }
    #endregion

    #region private method
    #endregion

    #region private method
    private void ScrollMoveCoroutine()
    {
        transform.Translate(0,0,-_zPositionSpeed * _transitionMulti * Time.deltaTime);
    }
    #endregion
}