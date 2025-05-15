using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventSubscriber : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables
    [SerializeField] private UIEventSubscriptionTypes types;
    [SerializeField] private Button button;
    #endregion

    #region Private Variables
    private UIManager _manager;

    #endregion

    #endregion

    private  void Awake()
    {
        GetReferences();
    }
    private void GetReferences(){
        _manager = FindObjectOfType<UIManager>();
    }

    private void OnEnable(){
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        switch (types)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.AddListener(_manager.Play);
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.AddListener(_manager.NextLevel);
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.AddListener(_manager.RestartLevel);
                break;
        }
    }    
    private void UnSubscribeEvents()
    {
        switch (types)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.RemoveListener(_manager.Play);
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.RemoveListener(_manager.NextLevel);
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.RemoveListener(_manager.RestartLevel);
                break;
        }
    }
    private void OnDisable(){
        UnSubscribeEvents();
    }   
}