using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;

public class CameraManager : MonoBehaviour
{
    #region property
    #endregion

    #region serialize
    [SerializeField]
    private ActivationCamera[] _virtualCamera;

    [SerializeField]
    private CameraType _type;
    #endregion

    #region private
    private Dictionary<CameraType, CinemachineVirtualCamera> _cameraDic = new Dictionary<CameraType, CinemachineVirtualCamera>();
    private CinemachineTrackedDolly dolly;
    private float PathPositionMax;
    private float PathPositionMin;
    private CameraType initialCamera = CameraType.startCamera;
    private CameraType currentCamera;

    #endregion

    #region Constant
    private const int PriorityAmount = 11;
    #endregion

    #region Event
    #endregion

    #region unity methods
    private void Awake()
    {
        for (int i = 0; i < _virtualCamera.Length; i++)
        {
            _cameraDic.Add((CameraType)i, _virtualCamera[i].Camera);
        }
    }

    private void Start()
    {
        currentCamera = initialCamera;
        SignalManager.Instance.CameraMoveObserver
                              .TakeUntilDestroy(this)
                              .Subscribe(_ =>
                              {
                                  TimeLineCamera();
                              });
    }

    private void Update()
    {

    }
    #endregion

    #region public method
    public void CameraChange(CameraType cameraType)
    {
        int _initialPriority = 10;

        foreach (var camera in _cameraDic)
        {
            camera.Value.Priority = _initialPriority;
        }
        _cameraDic[cameraType].Priority = PriorityAmount;
        currentCamera = cameraType;
    }
    #endregion

    #region private method
    private void TimeLineCamera()
    {
        dolly = _cameraDic[CameraType.startCamera].GetComponent<CinemachineTrackedDolly>();
        DollyChangeCoroutin(PathPositionMax);
        CameraChange(CameraType.CvCamera1);
    }
    #endregion

    #region coroutine method
    IEnumerator DollyChangeCoroutin(float target)
    {
        yield return null;
    }
    #endregion

}

[System.Serializable]
public struct ActivationCamera
{
    public string CameraName;
    public CinemachineVirtualCamera Camera;
}

public enum CameraType
{
    CvCamera1,
    CvCamera2,
    startCamera,
}