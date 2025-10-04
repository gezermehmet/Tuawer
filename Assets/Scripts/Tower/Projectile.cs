using UnityEngine;

namespace Tower
{
    public class Projectile : MonoBehaviour
    {
        private TowerController _towerController;
        public Transform enemy;
        public float bulletSpeed;
        public float bulletDamage;
        public float timer = 0f;
        public float bulletLifeTime = 5f;

        void Start()
        {
            _towerController = FindObjectOfType<TowerController>();
        }

        void Update()
        {
            if (_towerController != null)
                bulletSpeed = _towerController.bulletSpeed;

            Target();
            timer += Time.deltaTime;
        }

        void Target()
        {
            if (enemy == null) return;

            transform.position = Vector3.MoveTowards(
                transform.position,
                enemy.position,
                bulletSpeed * Time.deltaTime);

            if (timer >= bulletLifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}