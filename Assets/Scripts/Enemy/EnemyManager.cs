using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [FormerlySerializedAs("spawnPositions")] [SerializeField] private Transform[] _spawnPositions;
        [FormerlySerializedAs("attackPositions")] [SerializeField] private Transform[] _attackPositions;
        [FormerlySerializedAs("character")] [SerializeField] private Player _character;
        [FormerlySerializedAs("worldTransform")] [SerializeField] private Transform _worldTransform;
        [FormerlySerializedAs("container")] [SerializeField] private Transform _container;
        [FormerlySerializedAs("prefab")] [SerializeField] private Enemy _prefab;
        [SerializeField] private BulletManager _bulletSystem;
        [SerializeField] private BulletConfig _config;
        
        private readonly HashSet<Enemy> _activeEnemies = new();
        private CharacterAttackHandler _characterAttackHandler;
        private readonly Queue<Enemy> _enemyPool = new();
        
        private void Awake()
        {
            _characterAttackHandler = new CharacterAttackHandler(_config, _bulletSystem);
            for (var i = 0; i < 7; i++)
            {
                Enemy enemy = Instantiate(_prefab, _container);
                _enemyPool.Enqueue(enemy);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, 2));
                
                if (!_enemyPool.TryDequeue(out Enemy enemy))
                {
                    enemy = Instantiate(_prefab, _container);
                }

                enemy.transform.SetParent(_worldTransform);

                Transform spawnPosition = RandomPoint(_spawnPositions);
                enemy.transform.position = spawnPosition.position;

                Transform attackPosition = RandomPoint(_attackPositions);
                enemy.SetDestination(attackPosition.position);
                enemy.enemyAttackComponent.target = _character;

                if (_activeEnemies.Count < 5 && _activeEnemies.Add(enemy))
                {
                    enemy.enemyAttackComponent.OnFire += OnFire;
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (Enemy enemy in _activeEnemies.ToArray())
            {
                if (enemy.health <= 0)
                {
                    enemy.enemyAttackComponent.OnFire -= OnFire;
                    enemy.transform.SetParent(_container);

                    _activeEnemies.Remove(enemy);
                    _enemyPool.Enqueue(enemy);
                }
            }
        }

        private void OnFire(Vector2 position, Vector2 direction)
        {
            _characterAttackHandler.Attack(position, direction);
        }

        private Transform RandomPoint(Transform[] points)
        {
            int index = Random.Range(0, points.Length);
            return points[index];
        }
    }
}