using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ ���� ������ (�� ���� �⺻ �� GameObject)
    public float spawnInterval = 3f; // �� ���� ����
    public int maxEnemies = 10; // �ִ� �� ��

    private int currentEnemyCount = 0; // ���� �� ��

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
        // ���� x �� z ��ġ ���� (-500���� 500 ����)
        float randomX = Random.Range(-500f, 500f);
        float randomZ = Random.Range(-500f, 500f);

        // y ��ġ�� 20���� ����
        Vector3 spawnPosition = new Vector3(randomX, 20, randomZ); // y�� 20, z�� ����

        // ���� �ִ� �� GameObject�� ����
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // ���� ��ũ��Ʈ�� �ʿ��ϴٸ� �߰��� ����
        // enemy.GetComponent<EnemyStats>().Init(); // ���÷� �ʱ�ȭ �Լ� ȣ��

        currentEnemyCount++;
    }
}