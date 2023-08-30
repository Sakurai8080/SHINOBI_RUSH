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
    private Coroutine _currentCoroutine;

    #endregion

    #region Constant
    private const int PriorityAmount = 11;
    #endregion

    #region Event
    private Action gameStartAction;
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
        gameStartAction += GameStartCameraChenge;
        SignalManager.Instance.CameraMoveObserver
                              .TakeUntilDestroy(this)
                              .Subscribe(_ =>
                              {
                                  TimeLineCamera();
                                  Debug.Log("カメラマネージャーサブスクライブ");
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
    }
    #endregion

    #region private method
    private void TimeLineCamera()
    {
        Debug.Log("TimeLine開始");
        dolly = _cameraDic[CameraType.startCamera].GetCinemachineComponent<CinemachineTrackedDolly>();
        PathPositionMax = dolly.m_Path.MaxPos;
        PathPositionMin = dolly.m_Path.MinPos;
        _currentCoroutine = StartCoroutine(DollyChangeCoroutin(dolly));
    }

    private void GameStartCameraChenge()
    {
        CameraChange(CameraType.CvCamera1);
    }
    #endregion

    #region coroutine method
    IEnumerator DollyChangeCoroutin(CinemachineTrackedDolly dolly)
    {
        Debug.Log("ドリーチェンジ開始");
        float elapsedTime = 0f;
        float duration = 1.0f;

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            dolly.m_PathPosition = Mathf.Lerp(PathPositionMin, PathPositionMax, time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("ドリーチェンジ完了");
        gameStartAction.Invoke();
        //CameraChange(CameraType.CvCamera1);
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