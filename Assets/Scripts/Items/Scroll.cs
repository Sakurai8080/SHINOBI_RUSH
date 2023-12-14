using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 巻物で経験値アップ
/// </summary>
public class Scroll : ItemBase
{
    #region property
    #endregion

    #region serialize
    [Tooltip("経験値")]
    [SerializeField]
    private uint _expValue = 1;
    #endregion

    #region private
    private Coroutine _currentCoroutine;
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
        _currentCoroutine = StartCoroutine(ScrollMoveCoroutine());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (_currentCoroutine != null)
            StartCoroutine(ScrollMoveCoroutine());

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (_currentCoroutine != null)
            StopCoroutine(ScrollMoveCoroutine());
    }
    #endregion

    #region public method
    public override void Use(PlayerController player)
    {
        Debug.Log($"{ItemType}を使用した");
        player.GetExp(_expValue);
        gameObject.SetActive(false);
    }

    public override void Return()
    {
        Debug.Log($"{ItemType}を非アクティブにした");

        //座標をリセットして非表示にする。
        gameObject.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }
    #endregion

    #region private method
    #endregion

    #region private method
    IEnumerator ScrollMoveCoroutine()
    {
        while (true)
        {
            transform.Translate(0, 0, -0.05f);
            yield return null;
        }
    }
    #endregion
}