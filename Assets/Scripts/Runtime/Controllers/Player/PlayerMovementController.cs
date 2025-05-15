using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables 

        #region Serialized Variables 
        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #region Private Variables

        [NaughtyAttributes.ShowNonSerializedField] private PlayerMovementData _data;
        [NaughtyAttributes.ShowNonSerializedField] private bool _isReadyToMove, _isReadyToPlay;
        [NaughtyAttributes.ShowNonSerializedField] private float _xValue;

        private float2 _clampValues;
        #endregion
        #endregion

        internal void SetData(PlayerMovementData data)
        {
            _data = data;
        }

        private void FixedUpdate()
        {
            if (!_isReadyToPlay)
            {
                StopPlayer();
                return;
            }

            if (!_isReadyToMove)
            {
                MovePlayer();
                return;
            }
            else
            {
                StopPlayerHorizontally();
            }
        }

        private void StopPlayer()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }


        private void StopPlayerHorizontally()
        {
           rigidbody.velocity = new Vector3(0,rigidbody.velocity.y, _data.ForwardSpeed);
        }
        
        private void MovePlayer()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_xValue * _data.SidewaySpeed, velocity.y, _data.ForwardSpeed);
            rigidbody.velocity = velocity;
            var position1 = rigidbody.position;
            Vector3 position;
            position = new Vector3(Mathf.Clamp(position1.x,_clampValues.x,_clampValues.y),
                (position = rigidbody.position).y, position.z);
            rigidbody.position = position;
        }

        internal void IsReadyToPlay(bool condition)
        {
            _isReadyToPlay = condition;
        }

        internal void IsReadyToMove(bool condition)
        {
            _isReadyToMove = condition;
        }

        internal void UpdateInput(HorizontalInputParams inputParams)
        {
            _xValue = inputParams.HorizontalValue;
            _clampValues = inputParams.ClampValues;
        }

        internal void OnReset()
        {
            StopPlayer();
            _isReadyToMove = false;
            _isReadyToPlay = false;
        }
    }
}