using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIPanelController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Transform> layers = new List<Transform>();
    #endregion

    #endregion

    private void OnEnable(){
        SubscribeEvents();
        var cd = Resources.Load<CD_Level>("Data/CD_Level");
        Debug.Log($"Level count: {cd.Levels.Count}");
        Debug.Log($"First level pool count: {cd.Levels[0].Pools.Count}");
    }

    private void SubscribeEvents(){
        CoreUISignals.Instance.onClosePanel += OnClosePanel;
        CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
        CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
    }

    private void OnCloseAllPanels()
    {
        foreach(var layer in layers)
        {
            if (layer.childCount <= 0 ) return;
#if UNITY_EDITOR 
        DestroyImmediate(layer.GetChild(0).gameObject);
#else
        Destroy(layer.GetChild(0).gameObject);

#endif
        }
    }

    private void OnOpenPanel(UIPanelTypes panelType, int value){
        OnClosePanel(value);
        Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"),layers[value]);
    }

    private void OnClosePanel(int value)
    {
        if (layers[value].childCount <= 0 ) return;
#if UNITY_EDITOR
        DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
        Destroy(layers[value].GetChild(0).gameObject);
#endif
        
    }
    private void UnSubscribeEvents(){
        CoreUISignals.Instance.onClosePanel -= OnClosePanel;
        CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
        CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
    }
    private void OnDisable(){
        UnSubscribeEvents();
    }
}