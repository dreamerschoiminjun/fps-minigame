using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonArsenal
{
    public class Bullet : MonoBehaviour
    {
        public int damage = 10;  // �Ѿ��� ������
        public float destroyTime = 5f;  // �Ѿ��� �ڵ����� ���ŵǴ� �ð�

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Destroy Timer�� ���� �Ѿ� ����
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0f)
            {
                Destroy(gameObject);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            // �浹�� ��ü�� ���� ��� ������ ����
            if (collision.gameObject.CompareTag("enemy"))
            {
                EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(damage);
                    Debug.Log($"������ {damage} ������ ����.");
                }
            }

            // �Ѿ� �ı�
            Destroy(gameObject);
        }
    }
}