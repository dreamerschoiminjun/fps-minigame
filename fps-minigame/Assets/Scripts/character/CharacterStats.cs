using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    // 체력관련
    public float maxHealth = 100;                 // 기본 최대 체력

    // 현재 보유 스탯
    public float CurrentHealth;                   // 현재 체력

    // 원거리 공격 관련
    public float rangedAttackPower = 8;           // 원거리 공격력
    public float bulletspeed = 1000f;             // 발사체 속도
    public float bulletfireRate = 0.3f;           // 발사 속도
    public float fireDelay;                       // 발사 지연 시간

    // 이동관련
    public float MoveSpeed = 2.0f;                // 기본 이동 속도
    public float SprintSpeed = 5.335f;            // 기본 달리기 속도
    public float JumpHeight = 1.2f;               // 점프높이
    public float jumppower = 10;                  // 점프세기

    [Header("인벤토리")]
    public List<GameItem> inventoryItems = new List<GameItem>();
    
    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void AddToInventory(GameItem newItem)
    {
        // 새로운 아이템을 인벤토리에 추가합니다.
        inventoryItems.Add(newItem);
        UnityEngine.Debug.Log($"아이템 {newItem.id} 추가 완료");
    }
}
