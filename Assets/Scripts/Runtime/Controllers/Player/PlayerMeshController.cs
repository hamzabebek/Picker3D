﻿

using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private new Renderer renderer;
    [SerializeField] private TextMeshPro scaleText;
    [SerializeField] private ParticleSystem confetti;

    #endregion

    #region Private Variables
    [NaughtyAttributes.ShowNonSerializedField] private PlayerMeshData _data;
    #endregion

    #endregion

    internal void SetData(PlayerMeshData data)
    {
        _data = data;
    }

    internal void ScaleUpPlayer()
    {
        renderer.gameObject.transform.DOScaleX(_data.ScaleCounter,1).SetEase(Ease.Flash);
    }
    internal void ShowUpText()
    {
        scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
        { 
        scaleText.DOFade(0, .30f).SetDelay(.35f);
        scaleText.rectTransform.DOAnchorPosY(1f, .65f).SetEase(Ease.Linear);
    });
    }

    internal void PlayConfetti()
    {
        confetti.Play();
    }
    internal void OnReset()
    {
        renderer.gameObject.transform.DOScaleX(1,1).SetEase(Ease.Linear);
    }
}