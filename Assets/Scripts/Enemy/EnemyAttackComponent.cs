using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyAttackComponent : MonoBehaviour
    {
        [SerializeField] private float countdown;

        [NonSerialized] public Player target;
        
        [SerializeField] public Transform firePoint;
        
        public event Enemy.FireHandler OnFire;

        public void ResetCountdown()
        {
            this.currentTime = this.countdown;
        }

        public void Attack()
        {
            if (this.target.health <= 0)
                return;
            
            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                Vector2 startPosition = this.firePoint.position;
                Vector2 vector = (Vector2) this.target.transform.position - startPosition;
                Vector2 direction = vector.normalized;
                this.OnFire?.Invoke(startPosition, direction);
                    
                this.currentTime += this.countdown;
            }
        }

        private float currentTime;
    }
}