using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingAttack : MonoBehaviour
{
    private bool isCharging = false;                 // 차징 중 여부
    private EnemyStats enemyStats;                  // EnemyStats 컴포넌트를 참조할 변수

    public GameObject laserPrefab;                   // 레이저 프리팹
    public Transform firePoint;                       // 레이저 발사 위치

    void Start()
    {
        // EnemyStats 컴포넌트를 가져옵니다.
        enemyStats = GetComponent<EnemyStats>();
        if (enemyStats == null)
        {
            Debug.LogWarning("EnemyStats 컴포넌트를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        // 적의 차징 공격 로직
        if (!isCharging)
        {
            StartCharging();
        }

        if (isCharging)
        {
            ChargeAttack();
        }

        if (enemyStats.chargeTime >= enemyStats.maxChargeTime)
        {
            PerformAttack();
        }
    }

    void StartCharging()
    {
        isCharging = true;
        enemyStats.chargeTime = 0f; // 차징 시간 초기화
        Debug.Log("차징 시작!");
    }

    void ChargeAttack()
    {
        // 차징 시간 증가
        enemyStats.chargeTime += Time.deltaTime;

        // 최대 차징 시간 초과 방지
        if (enemyStats.chargeTime > enemyStats.maxChargeTime)
        {
            enemyStats.chargeTime = enemyStats.maxChargeTime;
        }
    }

    void PerformAttack()
    {
        isCharging = false;

        // 최종 공격력 계산 (변경: 지역 변수로 설정)
        float finalDamage = enemyStats.attackPower;
        Debug.Log($"차징 공격! 데미지: {finalDamage}");

        // 레이저 발사
        FireLaser(finalDamage); // 데미지를 인수로 전달

        // 공격 후 차징 시간 초기화
        enemyStats.chargeTime = 0f;
    }

    void FireLaser(float damage)
    {
        // 레이저 생성
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Laser laserScript = laser.AddComponent<Laser>(); // Laser 스크립트를 동적으로 추가

        if (laserScript != null)
        {
            laserScript.SetDamage(damage); // 레이저에 공격력 설정
        }

        Debug.Log("레이저 발사!");
    }
}
