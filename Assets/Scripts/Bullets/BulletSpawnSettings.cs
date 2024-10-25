using UnityEngine;

namespace ShootEmUp
{
    public struct BulletSpawnSettings
    {
        public BulletConfig config { get; }
        public Vector2 firePointPosition { get; }
        public Vector2 fireDirection { get; }
        
        public BulletSpawnSettings(Vector2 firePointPosition, Vector2 fireDirection, 
            BulletConfig config)
        {
            this.firePointPosition = firePointPosition;
            this.fireDirection = fireDirection;
            this.config = config;
        }
    }
}