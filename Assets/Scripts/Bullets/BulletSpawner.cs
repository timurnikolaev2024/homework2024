using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] public Bullet prefab;
        [SerializeField] public Transform worldTransform;
        [SerializeField] private Transform container;
        
        private readonly Queue<Bullet> m_bulletPool = new();
        
        public void FillPool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Bullet bullet = Instantiate(this.prefab, this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBullet(BulletSpawnSettings settings)
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }

            bullet.transform.position = settings.firePointPosition;
            bullet.spriteRenderer.color = settings.config.color;
            bullet.gameObject.layer = (int)settings.config.physicsLayer;
            bullet.damage = settings.config.damage;
            bullet.rigidbody2D.velocity = settings.fireDirection * settings.config.speed;

            return bullet;
        }

        public void RemoveBullet(Bullet bullet)
        {
            bullet.transform.SetParent(this.container);
            this.m_bulletPool.Enqueue(bullet);
        }
    }
}