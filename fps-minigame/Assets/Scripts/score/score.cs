using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    public float point; // 외부에서 접근할 수 있도록 public으로 설정

    private float timeAlive; // 점수 증가를 위한 시간 변수
                         
    [SerializeField]
    public float healthIncreasePerSecond = 0.1f;   // 초당 체력 증가량
    public float speedIncreasePerSecond = 0.01f;    // 초당 속도 증가량
    public float attackPowerIncreasePerSecond = 0.02f; // 초당 공격력 증가량
    public float scorePerKillIncreasePerSecond = 1f; // 초당 점수 증가량

    void Update()
    {
        // 시간 증가
        timeAlive += Time.deltaTime;

    }

    public void AddKillScore(float score)
    {
        // 적을 처치할 때 추가 점수
        point += score;
        Debug.Log($"점수 증가: {score}, 총 점수: {point}"); // 점수 변화 디버그 메시지
    }

    public void ApplyStatIncreases(EnemyStats enemyStats)
    {
        // 상태 변화 증가
        enemyStats.maxHealth += healthIncreasePerSecond * Time.deltaTime;
        enemyStats.attackPower += attackPowerIncreasePerSecond * Time.deltaTime;
        enemyStats.moveSpeed += speedIncreasePerSecond * Time.deltaTime;
        enemyStats.scorePerKill += scorePerKillIncreasePerSecond * Time.deltaTime;
    }
}

