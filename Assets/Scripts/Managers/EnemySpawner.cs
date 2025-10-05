using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;
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


        public int currentPhaseIndex = 0;
        private PhaseData _currentPhase;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }


            _currentPhase = phases[currentPhaseIndex];
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }


        /* xxxİlk indexe gir.
               İlk phase'in:
                    Enemy prefablerini çek,
                    Enemy weihtlerini çek,
                    Weighte oranla instantiate et,
                    Cooldowna göre spawn çalışsın.
               Enemy sayısı 0 olur ise diğer phase'e geç
            */
        IEnumerator Spawn()
        {
            float waitTime = phases[currentPhaseIndex].cooldown;
            int maxEnemy = phases[currentPhaseIndex].maxEnemyCount;

            while (true)
            {
                spawnCount = Random.Range(_minEnemy, _maxEnemy);

                if (maxEnemy < spawnCount)
                {
                    spawnCount = maxEnemy;
                }

                for (int i = 0; i < spawnCount; i++)
                {
                    Instantiate(phases[currentPhaseIndex].enemyPool[EnemySelecter()].prefab,
                        spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                    maxEnemy--;
                }
                yield return new WaitForSeconds(waitTime);
            }
        }

        public void DecreaseMaxEnemyCount()
        {
            int x = --phases[currentPhaseIndex].maxEnemyCount;

            if (x <= 0)
            {
                currentPhaseIndex++;
                StartCoroutine(Spawn());
            }
        }

        private int EnemySelecter()
        {
            int w1 = phases[currentPhaseIndex].enemyPool[0].weight;
            int w2 = phases[currentPhaseIndex].enemyPool[1].weight;

            int w_total = w1 + w2;
            int r = Random.Range(0, w_total);

            if (r < 75)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}