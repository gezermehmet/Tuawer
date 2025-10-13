using System.Collections;
using Managers;
using Tower;
using UnityEngine;

namespace Enemies
{
    public class EnemyBase : MonoBehaviour
    {
        public float hp = 10f;
        public float speed = 1f;
        public float damage = 1f;
        public float attackInterval = 3f;
        public float exp = 1f;

        public GameObject target;
        private float _attackTimer;
        public TowerController tower;
        private Rigidbody2D rb;
        private bool isTouchingTower = false;
        

        public void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Tower");
            tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<TowerController>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!isTouchingTower)
            {
                Move();
            }

            if (isTouchingTower)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= attackInterval)
                {
                    tower.TakeDamage(damage);
                    _attackTimer = 0f;
                }
            }
        }

        void Move()
        {
            if (target != null)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                transform.Translate(direction * (speed * Time.deltaTime));
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Tower"))
            {
                tower.TakeDamage(damage);
                isTouchingTower = true;
            }
        }
        
        public void DealDamage(float damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
            EnemySpawner.Instance.DecreaseMaxEnemyCount();
            tower.ExpEvent(exp);
        }

    }
}