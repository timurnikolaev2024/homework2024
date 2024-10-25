using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [FormerlySerializedAs("levelBounds")] 
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private BulletSpawner _bulletSpawner;
        
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        private void Awake()
        {
            _bulletSpawner.FillPool(10);
        }

        private void FixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                Bullet bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void SpawnBullet(BulletSpawnSettings settings)
        {
            var bullet = _bulletSpawner.SpawnBullet(settings);
            
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletSpawner.RemoveBullet(bullet);
            }
        }

        private void DealDamage(Bullet bullet, GameObject other)
        {
            int damage = bullet.damage;
            if (damage <= 0)
                return;

            if (other.TryGetComponent(out IDamageable character))
            {
                character.TakeDamage(damage);
            }
        }
    }
}