using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour, IDamageable
    {
        public delegate void FireHandler(Vector2 position, Vector2 direction);

        [SerializeField] private EnemyAttackComponent _enemyAttackComponent;
        [SerializeField] private EnemyMoveComponent _enemyMoveComponent;
        [SerializeField] public int health;
        
        public EnemyAttackComponent enemyAttackComponent => _enemyAttackComponent;
        
        public void SetDestination(Vector2 endPoint)
        {
            _enemyMoveComponent.SetDestination(endPoint);
        }

        private void FixedUpdate()
        {
            if (_enemyMoveComponent.GetDestinationReached())
            {
                _enemyAttackComponent.Attack();
            }
            else
            {
                _enemyMoveComponent.Move();
            }
        }

        public void TakeDamage(int damage)
        {
            if (health > 0)
            {
                health = Mathf.Max(0, health - damage);
            }
        }
    }
}