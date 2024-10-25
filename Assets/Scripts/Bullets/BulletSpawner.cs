using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class BulletSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("prefab")] 
        [SerializeField] private Bullet _prefab;
        [FormerlySerializedAs("worldTransform")] 
        [SerializeField] private Transform _worldTransform;
        [FormerlySerializedAs("container")] 
        [SerializeField] private Transform _container;
        
        private readonly Queue<Bullet> _bulletPool = new();
        
        public void FillPool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Bullet bullet = Instantiate(_prefab, _container);
                _bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBullet(BulletSpawnSettings settings)
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = Instantiate(_prefab, _worldTransform);
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
            bullet.transform.SetParent(this._container);
            _bulletPool.Enqueue(bullet);
        }
    }
}