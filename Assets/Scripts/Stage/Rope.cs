using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ロープに機能をもたせるクラス
/// </summary>
public class Rope : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    #endregion

    #region private
    private float _rotateSpeed = 5.0f;
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
                transform.Rotate(-_rotateSpeed, 0, 0);
            });
    }
    #endregion

    #region public method
    #endregion

    #region private method
    #endregion
}