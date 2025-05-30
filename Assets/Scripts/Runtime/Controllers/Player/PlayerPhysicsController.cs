﻿using Runtime.Signals;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
        #endregion

        #region Private Variables
        private readonly string _stageArea = "StageArea";
        private readonly string _finish = "FinishArea";
        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_stageArea))
            {
                manager.ForceCommend.Execute();
                CoreGameSignals.Instance.onStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();

                //Stage Area Kontrol Süreci
            }

            if (other.CompareTag(_finish))
            {
                CoreGameSignals.Instance.onFinishAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                return;
            }
        }
        public void OnReset()
        {
        }
    }
}