using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    // ü�°���
    public float maxHealth = 100;                 // �⺻ �ִ� ü��

    // ���� ���� ����
    public float CurrentHealth;                   // ���� ü��

    // ���Ÿ� ���� ����
    public float rangedAttackPower = 8;           // ���Ÿ� ���ݷ�
    public float bulletspeed = 1000f;             // �߻�ü �ӵ�
    public float bulletfireRate = 0.3f;           // �߻� �ӵ�
    public float fireDelay;                       // �߻� ���� �ð�

    // �̵�����
    public float MoveSpeed = 2.0f;                // �⺻ �̵� �ӵ�
    public float SprintSpeed = 5.335f;            // �⺻ �޸��� �ӵ�
    public float JumpHeight = 1.2f;               // ��������
    public float jumppower = 10;                  // ��������

    [Header("�κ��丮")]
    public List<GameItem> inventoryItems = new List<GameItem>();
    
    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void AddToInventory(GameItem newItem)
    {
        // ���ο� �������� �κ��丮�� �߰��մϴ�.
        inventoryItems.Add(newItem);
        UnityEngine.Debug.Log($"������ {newItem.id} �߰� �Ϸ�");
    }
}