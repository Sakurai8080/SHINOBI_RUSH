using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ステージスクロール
/// </summary>
public class StageScroll : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private GameObject _openingStage = default;
    #endregion

    #region private
    /// <summary>ステージの初期位置</summary>
    private Vector3 _initialPosition;

    /// <summary>スクロールするz軸の数値</summary>
    private float _scrollPosition = 340.0f;
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
        _initialPosition = transform.position;
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                Scroling();
                StageMove();
            });
        GameManager.Instance.GameStartObserver
                            .TakeUntilDestroy(this)
                            .Subscribe(_ => Destroy(_openingStage));
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void StageMove()
    {
        transform.position = transform.position - transform.forward;
    }

    private void Scroling()
    {
        if (_scrollPosition < _initialPosition.z - transform.position.z)
        {
            transform.position = _initialPosition;
        }
    }
    #endregion
}