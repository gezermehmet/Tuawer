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

        void Spawnn()
        {
            spawnCount = Random.Range(_minEnemy, _maxEnemy);

            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(phases[i].enemyPool[i].prefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
            }

            spawnCount = 0;
        }


        void UpdatePhases()
        {
            //if (_currentPhase != null && _currentPhase.maxEnemyCount) ;

            /* xxxİlk indexe gir.
               İlk phase'in:
                    Enemy prefablerini çek,
                    Enemy weihtlerini çek,
                    Weighte oranla instantiate et,
                    Cooldowna göre spawn çalışsın.
               Enemy sayısı 0 olur ise diğer phase'e geç
            */
        }

        IEnumerator Spawn()
        {
            
            while (true)
            {
                Debug.Log("Düşman spawn oldu!");
                //yield return new WaitForSeconds(); 
            }
        }

        public void DecreaseMaxEnemyCount()
        {
            int x = --phases[currentPhaseIndex].maxEnemyCount;
            Debug.Log(x);
            if (x <= 0)
            {
                currentPhaseIndex++;
            }
        }
        
    }

    
    
}