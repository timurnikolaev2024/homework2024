using System;
using ShootEmUp;
using UnityEngine;

namespace Game
{
    public class GameEndController : MonoBehaviour
    {
        [SerializeField] private Player character;

        private void Awake()
        {
            character.OnHealthEmpty += OnGameEnd;
        }

        private void OnGameEnd(Player obj)
        {
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            character.OnHealthEmpty -= OnGameEnd;
        }
    }
}