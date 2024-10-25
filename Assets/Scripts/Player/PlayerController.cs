using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player character;

        [SerializeField] private BulletManager bulletManager;
        
        [SerializeField] private BulletConfig bulletConfig;

        private readonly PlayerInput playerInput = new();
        private CharacterAttackHandler characterAttackHandler;

        private bool fireRequired;
        private float moveDirection;

        private void Awake()
        {
            characterAttackHandler = new(bulletConfig, bulletManager);
        }

        private void Update()
        {
            if (playerInput.GetFireRequired())
            {
                characterAttackHandler.Attack(character.firePoint.position, 
                    character.firePoint.rotation * Vector3.up);

                fireRequired = false;
            }
            
            moveDirection = playerInput.GetDirection();
        }

        private void FixedUpdate()
        {
            character.Move(this.moveDirection);
        }
    }
}