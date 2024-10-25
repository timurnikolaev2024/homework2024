using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class EnemyMoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [FormerlySerializedAs("speed")] [SerializeField] private float _speed = 5.0f;

        private Vector2 destination;
        private bool isPointReached;

        public void SetDestination(Vector2 destination)
        {
            this.destination = destination;
            isPointReached = false;
        }

        public void Move()
        {
            Vector2 vector = destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                isPointReached = true;
                return;
            }

            Vector2 moveStep = vector.normalized * Time.fixedDeltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }

        public bool GetDestinationReached()
        {
            return isPointReached;
        }
}
}