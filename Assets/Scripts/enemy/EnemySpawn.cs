using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 스폰할 적의 프리팹 (이 경우는 기본 적 GameObject)
    public float spawnInterval = 3f; // 적 스폰 간격
    public int maxEnemies = 10; // 최대 적 수

    private int currentEnemyCount = 0; // 현재 적 수

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // 랜덤 x 및 z 위치 결정 (-500에서 500 사이)
        float randomX = Random.Range(-500f, 500f);
        float randomZ = Random.Range(-500f, 500f);

        // y 위치를 20으로 고정
        Vector3 spawnPosition = new Vector3(randomX, 20, randomZ); // y는 20, z는 랜덤

        // 씬에 있는 적 GameObject를 복제
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 적의 스크립트가 필요하다면 추가로 설정
        // enemy.GetComponent<EnemyStats>().Init(); // 예시로 초기화 함수 호출

        currentEnemyCount++;
    }
}
