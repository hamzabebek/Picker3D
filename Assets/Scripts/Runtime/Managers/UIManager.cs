using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable() {
        SubscribeEvents();
    }
    private void SubscribeEvents() {
        if (CoreGameSignals.Instance != null)
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onReset += OnReset;
        }
    }
    private void UnSubscribeEvents()
    {
        if (CoreGameSignals.Instance != null)
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    private void OnLevelFailed()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
    }

    private void OnLevelSuccessful()
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
    }

    private void OnLevelInitialize(byte arg0)
    {
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 1);
        UISignals.Instance.onSetLevelValue?.Invoke((byte)CoreGameSignals.Instance.onGetLevelValue?.Invoke());
    }

    private void OnReset(){
            CoreUISignals.Instance.onCloseAllPanels?.Invoke();
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
    }


    public void Play(){
        Debug.LogWarning("Executed ---> Play");
        UISignals.Instance.onPlay?.Invoke();
        CoreUISignals.Instance.onClosePanel?.Invoke(1);
        InputSignals.Instance.onEnableInput?.Invoke();
        //CameraSignals.Instance.onSetCameraTarget?.Invoke();
    }
    public void NextLevel(){
        CoreGameSignals.Instance.onNextLevel?.Invoke();
    }
    public void RestartLevel(){
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
    }
}