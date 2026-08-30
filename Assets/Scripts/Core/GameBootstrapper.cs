using System;
using Player;
using UnityEngine;
using World;

namespace Game
{
    public enum GameBootstrapPhase
    {
        NotStarted,
        MapReady,
        PlayerReady,
        GameReady,
        Failed
    }

    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private MapGenerator mapGenerator;
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private EnemySpawner enemySpawner;

        public GameBootstrapPhase CurrentPhase { get; private set; } = GameBootstrapPhase.NotStarted;
        public GameObject CurrentMap => mapGenerator != null ? mapGenerator.CurrentMap : null;
        public GameObject CurrentPlayer => playerSpawner != null ? playerSpawner.PlayerInstance : null;

        public event Action<GameBootstrapPhase> PhaseChanged;
        public event Action<GameObject> MapReady;
        public event Action<GameObject> PlayerReady;
        public event Action GameReady;

        private void Start()
        {
            InitializeGame();
        }

        public void InitializeGame()
        {
            if (CurrentPhase != GameBootstrapPhase.NotStarted)
            {
                Debug.LogWarning(
                    "GameBootstrapper: Game initialization has already started.",
                    this
                );
                return;
            }

            if (mapGenerator == null || playerSpawner == null || enemySpawner == null)
            {
                Fail("GameBootstrapper: One or more system references are missing.");
                return;
            }

            GameObject map = mapGenerator.GenerateMap();

            if (map == null)
            {
                Fail("GameBootstrapper: Unable to create the map.");
                return;
            }

            SetPhase(GameBootstrapPhase.MapReady);
            MapReady?.Invoke(map);

            GameObject player = playerSpawner.SpawnPlayer();

            if (player == null)
            {
                Fail("GameBootstrapper: Unable to create the player.");
                return;
            }

            SetPhase(GameBootstrapPhase.PlayerReady);
            PlayerReady?.Invoke(player);

            enemySpawner.StartSpawning(player);

            if (!enemySpawner.IsSpawning)
            {
                Fail("GameBootstrapper: Unable to start enemy spawning.");
                return;
            }

            SetPhase(GameBootstrapPhase.GameReady);
            GameReady?.Invoke();
        }

        public bool HasReached(GameBootstrapPhase phase)
        {
            return CurrentPhase != GameBootstrapPhase.Failed && CurrentPhase >= phase;
        }

        private void SetPhase(GameBootstrapPhase phase)
        {
            CurrentPhase = phase;
            PhaseChanged?.Invoke(CurrentPhase);
        }

        private void Fail(string message)
        {
            Debug.LogError(message, this);
            SetPhase(GameBootstrapPhase.Failed);
        }
    }
}
