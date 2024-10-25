using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [FormerlySerializedAs("m_params")] [SerializeField] private Params _params;
        
        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;

        private Transform myTransform;

        private void Awake()
        {
            startPositionY = _params.m_startPositionY;
            endPositionY = _params.m_endPositionY;
            movingSpeedY = _params.m_movingSpeedY;
            myTransform = transform;
            var position = myTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (myTransform.position.y <= endPositionY)
            {
                myTransform.position = new Vector3(
                    positionX,
                    startPositionY,
                    positionZ
                );
            }

            myTransform.position -= new Vector3(
                positionX,
                movingSpeedY * Time.fixedDeltaTime,
                positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float m_startPositionY;

            [SerializeField] public float m_endPositionY;

            [SerializeField] public float m_movingSpeedY;
        }
    }
}