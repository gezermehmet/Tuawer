using System;
using UnityEngine;

namespace Tower
{
    public class TowerController : MonoBehaviour
    {
        public LayerMask enemyLayer;
        private Transform _currentTarget;

        [Header("Tower")] public float towerRotationSpeed = 5f;
        public float range = 2f;
        public float criticalChance = 0.5f;
        public float criticalDamage = 10f;
        public float hp = 100f;
        public float maxHp = 100f;
        public float xpBar = 0f;
        public float currentXp = 0f;
        public int level = 1;

        [Header("Bullet")] public GameObject bulletPrefab;
        public float bulletDamage = 1f;
        public float fireRate = 5f;
        public float bulletSpeed = 2f;
        public float bulletLifeTime = 5f;
        public Transform bulletSpawnPoint;
        private float timer = 0f;
        private float baseTime = 10f;

        void Update()
        {
            FindTarget();
            RotateToTarget();
            timer += Time.deltaTime;
        }

        private void Start()
        {
        }

        void FindTarget()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
            Debug.Log("Found enemies: " + enemies.Length);

            if (enemies.Length > 0)
            {
                _currentTarget = enemies[0].transform;
                Shoot();
            }
            else
            {
                _currentTarget = null;
            }
        }

        void RotateToTarget()
        {
            if (_currentTarget == null)
            {
                return;
            }

            Vector3 direction = _currentTarget.position - transform.position;
            direction.z = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, targetRotation, towerRotationSpeed * Time.deltaTime);
            }
        }

        void Shoot()
        {
            if (_currentTarget == null) return;

            if (_currentTarget != null && timer >= (baseTime / fireRate))
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Projectile>().enemy = _currentTarget;
                timer = 0f;
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;

            UIManager.instance.UpdateHpBar(hp/maxHp);
            
            if (hp <= 0)
            {
                Destroy(gameObject);
                Time.timeScale = 0f;
                Debug.Log("Tower destroyed");
            }
        }

        public void ExpEvent(float exp)
        {
            currentXp += exp;

            if (currentXp >= xpBar)
            {
                level++;
                currentXp = 0;
                xpBar += (level - 1) * 5;
            }
        }
    }
}