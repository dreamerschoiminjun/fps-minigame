using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    public float point; // �ܺο��� ������ �� �ֵ��� public���� ����

    private float timeAlive; // ���� ������ ���� �ð� ����
                         
    [SerializeField]
    public float healthIncreasePerSecond = 0.1f;   // �ʴ� ü�� ������
    public float speedIncreasePerSecond = 0.01f;    // �ʴ� �ӵ� ������
    public float attackPowerIncreasePerSecond = 0.02f; // �ʴ� ���ݷ� ������
    public float scorePerKillIncreasePerSecond = 1f; // �ʴ� ���� ������

    void Update()
    {
        // �ð� ����
        timeAlive += Time.deltaTime;

    }

    public void AddKillScore(float score)
    {
        // ���� óġ�� �� �߰� ����
        point += score;
        Debug.Log($"���� ����: {score}, �� ����: {point}"); // ���� ��ȭ ����� �޽���
    }

    public void ApplyStatIncreases(EnemyStats enemyStats)
    {
        // ���� ��ȭ ����
        enemyStats.maxHealth += healthIncreasePerSecond * Time.deltaTime;
        enemyStats.attackPower += attackPowerIncreasePerSecond * Time.deltaTime;
        enemyStats.moveSpeed += speedIncreasePerSecond * Time.deltaTime;
        enemyStats.scorePerKill += scorePerKillIncreasePerSecond * Time.deltaTime;
    }
}
