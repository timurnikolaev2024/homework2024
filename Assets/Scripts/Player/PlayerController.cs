using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("character")] [SerializeField] private Player _character;
        [FormerlySerializedAs("bulletManager")] [SerializeField] private BulletManager _bulletManager;
        [FormerlySerializedAs("bulletConfig")] [SerializeField] private BulletConfig _bulletConfig;

        private readonly PlayerInput _playerInput = new();
        private CharacterAttackHandler _characterAttackHandler;

        private bool _fireRequired;
        private float _moveDirection;

        private void Awake()
        {
            _characterAttackHandler = new(_bulletConfig, _bulletManager);
        }

        private void Update()
        {
            if (_playerInput.GetFireRequired())
            {
                _characterAttackHandler.Attack(_character.firePoint.position, 
                    _character.firePoint.rotation * Vector3.up);

                _fireRequired = false;
            }
            
            _moveDirection = _playerInput.GetDirection();
        }

        private void FixedUpdate()
        {
            _character.Move(this._moveDirection);
        }
    }
}