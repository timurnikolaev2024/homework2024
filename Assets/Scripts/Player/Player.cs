using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class Player : MonoBehaviour, IDamageable
    {
        public Action<Player, int> OnHealthChanged;
        public Action<Player> OnHealthEmpty;
        
        [SerializeField] public Transform firePoint;
        [SerializeField] public int health;
        [SerializeField] private Rigidbody2D _rigidbody;
        [FormerlySerializedAs("speed")] [SerializeField] private float _speed = 5.0f;

        public void Move(float floatDirection)
        {
            Vector2 moveDirection = new Vector2(floatDirection, 0);
            Vector2 moveStep = moveDirection * Time.fixedDeltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }

        public void TakeDamage(int damage)
        {
            if (health <= 0)
                return;

            health = Mathf.Max(0, health - damage);
            OnHealthChanged?.Invoke(this, health);

            if (health <= 0)
                OnHealthEmpty?.Invoke(this);
        }
    }
}