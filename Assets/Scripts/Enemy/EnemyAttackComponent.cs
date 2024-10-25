using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class EnemyAttackComponent : MonoBehaviour
    {
        [FormerlySerializedAs("countdown")] [SerializeField] private float _countdown;
        [NonSerialized] public Player target;
        [FormerlySerializedAs("firePoint")] [SerializeField] private Transform _firePoint;
        
        public event Enemy.FireHandler OnFire;
        private float currentTime;

        
        public void Reset()
        {
            currentTime = _countdown;
        }

        public void Attack()
        {
            if (target.health <= 0)
                return;
            
            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                Vector2 startPosition = _firePoint.position;
                Vector2 vector = (Vector2) target.transform.position - startPosition;
                Vector2 direction = vector.normalized;
                OnFire?.Invoke(startPosition, direction);
                    
                currentTime += _countdown;
            }
        }
    }
}