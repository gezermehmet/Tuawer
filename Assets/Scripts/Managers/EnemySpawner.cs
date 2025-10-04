using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] spawnPoints;
        private float _timer = 0f;
        private int _minEnemy = 0;
        private int _maxEnemy = 4;

        [System.Serializable]
        public class EnemySpawnData
        {
            public GameObject prefab;
            public int weight;
        }

        [System.Serializable]
        public class PhaseData
        {
            public int maxEnemyCount;
            public float cooldown;
            public List<EnemySpawnData> enemyPool; // prefab + weight birlikte
        }
        public List<PhaseData> phases;
        private int spawnCount;
        private PhaseData _currentPhase;

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        void Spawn()
        {
            spawnCount = Random.Range(_minEnemy, _maxEnemy);

            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(spawnPoints[Random.Range(0, spawnPoints.Length)], transform.position, Quaternion.identity);
            }

            spawnCount = 0;
        }


        void UpdatePhases()
        {
            foreach (var phase in phases)
            {
                if (phase.maxEnemyCount == 0)
                {
                   
                }
            }

            for (int i = 0; i < phases.Count; i++)
            {
                _currentPhase = phases[i];

                if (_currentPhase.maxEnemyCount == 0)
                {
                    
                }
                else
                {
                    
                }
            }

        }
    }
    
    
}