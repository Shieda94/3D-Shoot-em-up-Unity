using System;
using UnityEngine;

namespace Player
{
    public class PlayerProgression : MonoBehaviour
    {
        [Header("Progression")]
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int currentExperience = 0;
        [SerializeField] private int experienceToNextLevel = 10;

        public int CurrentLevel => currentLevel;
        public int CurrentExperience => currentExperience;
        public int ExperienceToNextLevel => experienceToNextLevel;

        public event Action<int> ExperienceChanged;
        public event Action<int> LevelUp;

        public void AddExperience(int amount)
        {
            if (amount <= 0)
                return;

            currentExperience += amount;

            ExperienceChanged?.Invoke(currentExperience);

            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            while (currentExperience >= experienceToNextLevel)
            {
                currentExperience -= experienceToNextLevel;

                currentLevel++;

                LevelUp?.Invoke(currentLevel);
            }
        }
    }
}