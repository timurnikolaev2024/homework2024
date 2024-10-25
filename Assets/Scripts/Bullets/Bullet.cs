using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized]
        public bool isPlayer;
        
        [NonSerialized]
        public int damage;

        [SerializeField]
        public new Rigidbody2D rigidbody2D;

        [SerializeField]
        public SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);
        }
    }
}