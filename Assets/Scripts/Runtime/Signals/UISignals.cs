using UnityEngine;
using UnityEngine.Events;

public class UISignals : MonoBehaviour
{
    #region Singleton

    public static UISignals Instance;

    private void Awake(){

        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public UnityAction<byte> onSetStageColor = delegate { };
    public UnityAction onTestStageColor = delegate { };
    public UnityAction<byte> onSetLevelValue = delegate { };
    public UnityAction onPlay = delegate { };


}