using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // 기본 체력, 속도, 공격력
    [SerializeField]
    public float maxHealth = 100f;                 // 최대 체력
    public float currentHealth;                     // 현재 체력
    public float attackPower = 10f;                 // 공격력
    public float moveSpeed = 2.0f;                  // 이동 속도
    public float scorePerKill = 100;                  // 적이 사망할 때 추가 점수

    public GameObject scoreObject;                  // Score 컴포넌트를 가진 GameObject 참조
    private Score scoreManager;                     // Score 컴포넌트를 참조하기 위한 변수

    void Start()
    {
        currentHealth = maxHealth;

        // Score 컴포넌트를 가져옵니다
        if (scoreObject != null)
        {
            scoreManager = scoreObject.GetComponent<Score>();
            if (scoreManager == null)
            {
                Debug.LogWarning("Score 컴포넌트를 찾을 수 없습니다!");
            }
        }
    }

    void Update()
    {
        // 상태 변화 증가
        if (scoreManager != null)
        {
            scoreManager.ApplyStatIncreases(this);
        }
    }

    // 데미지를 받는 함수
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 적이 사망했을 때 처리
    void Die()
    {
        Debug.Log("적 사망");

        if (scoreManager != null)
        {
            // 적이 사망할 때 추가로 점수를 증가시킴
            scoreManager.AddKillScore(scorePerKill);
        }
        // 새로운 랜덤 위치 설정
        float randomX = Random.Range(-250f, 250f);
        float randomZ = Random.Range(-250f, 250f);
        Vector3 newPosition = new Vector3(randomX, 20, randomZ);

        // 위치로 이동
        transform.position = newPosition;
    }
}
