using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Config/BulletConfig", order = 1)]
    public class BulletConfig : ScriptableObject
    {
        public Color color = Color.white;
        public int damage = 1;
        public PhysicsLayer physicsLayer;
        public float speed;
    }
}