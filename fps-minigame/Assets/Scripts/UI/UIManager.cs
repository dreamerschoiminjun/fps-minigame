using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CharacterStats characterStats; // 캐릭터 스탯 스크립트 참조
    public Score scoreManager;            // Score 스크립트 참조

    [Header("UI Elements")]
    public Slider healthBar;              // 체력바
    public Text scoreText;                // 점수 표시 텍스트
    public Text itemDescriptionText;      // 아이템 설명 텍스트
    public Image crosshairImage;          // 에임 이미지

    private void Start()
    {
        UpdateHealthBar();
        UpdateScoreUI();
        UpdateItemDescriptionUI("");
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateScoreUI(); // 매 프레임마다 점수 UI 업데이트
    }

    // 체력바 업데이트
    private void UpdateHealthBar()
    {
        healthBar.value = characterStats.CurrentHealth / characterStats.maxHealth;
    }

    // 점수 UI 업데이트
    private void UpdateScoreUI()
    {
        // Score 스크립트의 point 값을 가져와서 UI에 반영
        scoreText.text = "Score: " + scoreManager.point.ToString("F0");
    }

    // 아이템 설명 UI 업데이트
    public void UpdateItemDescriptionUI(string description)
    {
        itemDescriptionText.text = "Item: " + description;
    }

    
}
