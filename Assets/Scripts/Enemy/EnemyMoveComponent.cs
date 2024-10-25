using UnityEngine;

namespace ShootEmUp
{
    public class EnemyMoveComponent : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D _rigidbody;
        [SerializeField] public float speed = 5.0f;

        private Vector2 destination;
        private bool isPointReached;

        public void SetDestination(Vector2 destination)
        {
            this.destination = destination;
            isPointReached = false;
        }

        public void Move()
        {
            Vector2 vector = this.destination - (Vector2)this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this.isPointReached = true;
                return;
            }

            Vector2 moveStep = vector.normalized * Time.fixedDeltaTime * speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }

        public bool GetDestinationReached()
        {
            return this.isPointReached;
        }
}
}