using UnityEngine;

namespace ShootEmUp
{
    public class CharacterAttackHandler
    {
        private BulletManager bulletManager;
        private BulletConfig config;
        public CharacterAttackHandler(BulletConfig config, BulletManager bulletManager)
        {
            this.config = config;
            this.bulletManager = bulletManager;
        }
        
        public void Attack(Vector2 firePointPosition, Vector2 fireDirection)
        {
            var bulletSettings = new BulletSpawnSettings(firePointPosition, fireDirection, config);
            bulletManager.SpawnBullet(bulletSettings);
        }
    }
}