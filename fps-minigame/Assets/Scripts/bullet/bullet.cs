using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonArsenal
{
    public class Bullet : MonoBehaviour
    {
        public int damage = 10;  // 총알의 데미지
        public float destroyTime = 5f;  // 총알이 자동으로 제거되는 시간

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Destroy Timer에 따른 총알 제거
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0f)
            {
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            // 충돌한 물체가 적일 경우 데미지 적용
            if (collision.gameObject.CompareTag("enemy"))
            {
                EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(damage);
                    Debug.Log($"적에게 {damage} 데미지 적용.");
                }
            }

            // 총알 파괴
            Destroy(gameObject);
        }
    }
}
