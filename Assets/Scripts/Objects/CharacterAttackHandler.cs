using UnityEngine;

namespace ShootEmUp
{
    public class CharacterAttackHandler
    {
        private BulletManager _bulletManager;
        private BulletConfig _config;
        public CharacterAttackHandler(BulletConfig config, BulletManager bulletManager)
        {
            _config = config;
            _bulletManager = bulletManager;
        }
        
        public void Attack(Vector2 firePointPosition, Vector2 fireDirection)
        {
            var bulletSettings = new BulletSpawnSettings(firePointPosition, fireDirection, _config);
            _bulletManager.SpawnBullet(bulletSettings);
        }
    }
}