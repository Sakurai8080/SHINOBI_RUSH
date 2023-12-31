using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ロープに機能に関わるコンポーネント
/// </summary>
public class Rope : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [Header("Variable")]
    [Tooltip("ロープの回転スピード")]
    [SerializeField]
    private float _ropeRotationSpeed = 5.0f;
    #endregion

    #region private
    private float _rotationMultiplier = 50f;
    #endregion

    #region Constant
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Start()
    {
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                RopeRotate();
            });
    }
    #endregion

    #region public method
    #endregion

    #region private method
    private void RopeRotate()
    {
        transform.Rotate(-_ropeRotationSpeed * _rotationMultiplier * Time.deltaTime, 0, 0);
    }
    #endregion
}