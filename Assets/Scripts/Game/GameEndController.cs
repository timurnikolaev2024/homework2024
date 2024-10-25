using System;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class GameEndController : MonoBehaviour
    {
        [FormerlySerializedAs("character")] [SerializeField] private Player _character;

        private void Awake()
        {
            _character.OnHealthEmpty += OnGameEnd;
        }

        private void OnGameEnd(Player obj)
        {
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            _character.OnHealthEmpty -= OnGameEnd;
        }
    }
}