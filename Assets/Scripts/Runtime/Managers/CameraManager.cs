using Cinemachine;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Self Variables

    #region Serializable Variables

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    #endregion

    #region Private Variables

    private float3 _firstPosition;

    #endregion

    #endregion

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _firstPosition = transform.position;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (CameraSignals.Instance == null)
        {
            Debug.LogError("CameraSignals is null");
            return;
        }
        CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void UnSubscribeEvents()
    {
        CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnReset()
    {
        transform.position = _firstPosition;
    }

    private void OnSetCameraTarget()
    {
        //var player = FindObjectOfType<PlayerManager>().transform;
        //virtualCamera.Follow = player;
        //virtualCamera.LookAt = player;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
