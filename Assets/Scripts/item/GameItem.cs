using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Item", menuName = "Game Item")]
public class GameItem : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemGrade;
    public Sprite itemIcon;
    public string itemsimpleDescription;
    public string itemDescription;
    public string itemSynergy;
    public string itemUnlockMethod;


    public void ApplyEffect(CharacterStats characterStats)
    {
        var effectActions = new Action<CharacterStats>[]
        {
            null, // Index 0은 사용하지 않으므로 null로 설정
        ApplyItem1Effect,
        ApplyItem2Effect,
        ApplyItem3Effect,
        ApplyItem4Effect,
        ApplyItem5Effect,
        
        };

        if (id >= 1 && id <= 200)
        {
            effectActions[id]?.Invoke(characterStats);
        }
    }

    // 각 아이템의 효과를 적용하는 메서드를 정의합니다.
    private void ApplyItem1Effect(CharacterStats characterStats)
    {
        characterStats.maxHealth = Mathf.RoundToInt(characterStats.maxHealth * 1.1f); // 최대 체력이 10% 증가
        UnityEngine.Debug.Log("힐팩 효과 적용 중: 최대 체력 10% 증가 -> " + characterStats.maxHealth);
    }

    private void ApplyItem2Effect(CharacterStats characterStats)
    {
        characterStats.rangedAttackPower += characterStats.rangedAttackPower * 0.1f; // 원거리 공격력 10% 증가
        UnityEngine.Debug.Log("총알 효과 적용 중: 원거리 공격력 10% 증가 -> " + characterStats.rangedAttackPower);
    }

    private void ApplyItem3Effect(CharacterStats characterStats)
    {
        characterStats.MoveSpeed += characterStats.MoveSpeed * 0.1f;  // 기본 이동 속도가 10% 증가
        characterStats.SprintSpeed += characterStats.SprintSpeed * 0.1f;  // 기본 달리기 속도가 10% 증가
        UnityEngine.Debug.Log("부츠 효과 적용 중: 이동 속도 10% 증가 -> " + characterStats.MoveSpeed + ", 달리기 속도 10% 증가 -> " + characterStats.SprintSpeed);
    }

    private void ApplyItem4Effect(CharacterStats characterStats)
    {
        characterStats.JumpHeight += characterStats.JumpHeight * 0.1f;  // 점프 높이가 10% 증가
        UnityEngine.Debug.Log("점프대 효과 적용 중: 점프 높이 10% 증가 -> " + characterStats.JumpHeight);
    }

    private void ApplyItem5Effect(CharacterStats characterStats)
    {
        characterStats.bulletfireRate -= characterStats.bulletfireRate * 0.1f;  // 발사 속도가 10% 증가 (발사 간격이 10% 감소)
        UnityEngine.Debug.Log("속사 효과 적용 중: 발사 속도 10% 증가 -> " + characterStats.bulletfireRate);
    }

}