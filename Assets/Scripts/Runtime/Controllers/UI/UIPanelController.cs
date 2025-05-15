using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIPanelController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Transform> layers = new List<Transform>();
    [SerializeField] private UIPanelTypes testPanelType;
    [SerializeField] private int testValue;
    #endregion

    #endregion

    private void OnEnable(){
        SubscribeEvents();
    }

    private void SubscribeEvents(){
        CoreUISignals.Instance.onOpenTestPanel += OnOpenTestPanel;
        CoreUISignals.Instance.onCloseTestPanel += onCloseTestPanel;
        CoreUISignals.Instance.onClosePanel += OnClosePanel;
        CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
        CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
    }
    [NaughtyAttributes.Button("Close All Panels")]
    private void OnCloseAllPanels(){
        foreach(var layer in layers){
                    if (layer.childCount <= 0 ) return;
#if UNITY_EDITOR 
        DestroyImmediate(layer.GetChild(0).gameObject);
#else
        Destroy(layer.GetChild(0).gameObject);

#endif
        }
    }
    //For Test
    [NaughtyAttributes.Button("OnOpenTestPanel")]
    private void OnOpenTestPanel(){
        OnClosePanel(testValue);
        var prefab = Resources.Load<GameObject>($"Screens/{testPanelType}Panel");
        var instance = Instantiate(prefab,layers[testValue]);
    }
    [NaughtyAttributes.Button("OnCloseTestPanel")]
    private void onCloseTestPanel(){
        if (layers[testValue].childCount <= 0) return;
#if UNITY_EDITOR
        DestroyImmediate(layers[testValue].GetChild(0).gameObject);
#else
        Destroy(layers[testValue].GetChild(0).gameObject);
#endif
    }
    private void OnOpenPanel(UIPanelTypes panelType, int value){
        OnClosePanel(value);
        Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"),layers[value]);
    }

    private void OnClosePanel(int value){
        if (layers[value].childCount <= 0 ) return;
#if UNITY_EDITOR
        DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
        Destroy(layers[value].GetChild(0).gameObject);
#endif
        
    }

        private void UnSubscribeEvents(){
        CoreUISignals.Instance.onOpenTestPanel -= OnOpenTestPanel;
        CoreUISignals.Instance.onCloseTestPanel -= onCloseTestPanel;
        CoreUISignals.Instance.onClosePanel -= OnClosePanel;
        CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
        CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
    }
    private void OnDisable(){
        UnSubscribeEvents();
    }
}