using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ステージを動かすコンポーネント
/// </summary>
public class StageScroll : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("ステージ")]
    [SerializeField]
    private GameObject _openingStage = default;
    #endregion

    #region private
    /// <summary>ステージの初期位置</summary>
    private Vector3 _initialPosition;

    /// <summary>スクロールするz軸のsタイミング</summary>
    private float _scrollPosition = 340.0f;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
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
    #endregion

    #region public method
    #endregion

    #region private method
    private void StageMove()
    {
        transform.position -= transform.forward * 50 * Time.deltaTime;
    }

    private void Scroling()
    {
        if (_scrollPosition < _initialPosition.z - transform.position.z)
            transform.position = _initialPosition;
    }
    #endregion
}