using System.Collections;
using UnityEngine;

[System.Serializable]
public class GameItem
{
    public int id; // 아이템 ID
    public string itemName; // 아이템 이름
    public string description; // 아이템 설명
    public Sprite icon; // 아이템 아이콘 (UI에 사용)
    public ItemType itemType; // 아이템 종류 (열거형)

    public enum ItemType
    {
        Buff, // 버프 아이템
        Debuff, // 디버프 아이템
        Consumable // 소모성 아이템
    }

    // 아이템 효과 적용 함수
    public void ApplyEffect(CharacterStats characterStats)
    {
        switch (id)
        {
            case 1:
                characterStats.StartCoroutine(ApplyItem1Effect(characterStats));
                break;
            case 2:
                characterStats.StartCoroutine(ApplyItem2Effect(characterStats));
                break;
            case 3:
                characterStats.StartCoroutine(ApplyItem3Effect(characterStats));
                break;
            case 4:
                ApplyItem4Effect(characterStats);
                break;
            case 5:
                characterStats.StartCoroutine(ApplyItem5Effect(characterStats));
                break;
            default:
                Debug.Log("효과가 정의되지 않은 아이템입니다.");
                break;
        }
    }

    // 아이템 효과 구현 (코루틴 호출)
    private IEnumerator ApplyItem1Effect(CharacterStats characterStats)
    {
        characterStats.rangedAttackPower *= 2;
        Debug.Log("20초 동안 공격력 2배 증가: " + characterStats.rangedAttackPower);
        yield return new WaitForSeconds(20f);
        characterStats.rangedAttackPower /= 2;
        Debug.Log("공격력 감소: " + characterStats.rangedAttackPower);
    }

    private IEnumerator ApplyItem2Effect(CharacterStats characterStats)
    {
        float moveSpeedIncrease = characterStats.MoveSpeed * 0.5f;
        float sprintSpeedIncrease = characterStats.SprintSpeed * 0.5f;
        characterStats.MoveSpeed += moveSpeedIncrease;
        characterStats.SprintSpeed += sprintSpeedIncrease;
        Debug.Log("20초 동안 이동 속도 1.5배 증가: " + characterStats.MoveSpeed + ", " + characterStats.SprintSpeed);
        yield return new WaitForSeconds(20f);
        characterStats.MoveSpeed -= moveSpeedIncrease;
        characterStats.SprintSpeed -= sprintSpeedIncrease;
        Debug.Log("이동 속도 감소: " + characterStats.MoveSpeed + ", " + characterStats.SprintSpeed);
    }

    private IEnumerator ApplyItem3Effect(CharacterStats characterStats)
    {
        characterStats.CurrentHealth = Mathf.Infinity; // 무적 상태
        Debug.Log("5초간 무적 상태");
        yield return new WaitForSeconds(5f);
        characterStats.CurrentHealth = characterStats.maxHealth; // 무적 해제
        Debug.Log("무적 상태 해제");
    }

    private void ApplyItem4Effect(CharacterStats characterStats)
    {
        float healAmount = characterStats.maxHealth / 2; // 최대 HP의 절반만큼 회복
        characterStats.CurrentHealth = Mathf.Min(characterStats.CurrentHealth + healAmount, characterStats.maxHealth); // 최대 HP를 넘지 않도록 제한
        Debug.Log("HP 절반 회복: " + characterStats.CurrentHealth);
    }

    private IEnumerator ApplyItem5Effect(CharacterStats characterStats)
    {
        characterStats.rangedAttackPower += 500;
        Debug.Log("5초 동안 공격력 +500: " + characterStats.rangedAttackPower);
        yield return new WaitForSeconds(5f);
        characterStats.rangedAttackPower -= 500;
        Debug.Log("공격력 복구: " + characterStats.rangedAttackPower);
    }
    // 총알 크기를 20초 동안 5배로 증가시키는 효과
    private IEnumerator ApplyItem6Effect(player player)
    {
        // BulletPrefab의 크기를 5배로 증가
        player.BulletPrefab.transform.localScale *= 5f;
        Debug.Log("총알 크기 5배 증가: " + player.BulletPrefab.transform.localScale);

        // 20초 동안 유지
        yield return new WaitForSeconds(20f);

        // 크기를 원래대로 복구 (/5)
        player.BulletPrefab.transform.localScale /= 5f;
        Debug.Log("총알 크기 복구: " + player.BulletPrefab.transform.localScale);
    }


    private void ApplyItem7Effect(CharacterStats characterStats, player player)
    {
        // 총알 프리팹을 무작위로 선택
        int randomBulletIndex = Random.Range(0, player.BulletPrefabs.Length);
        GameObject selectedBulletPrefab = player.BulletPrefabs[randomBulletIndex];

        // 선택된 총알 프리팹으로 설정
        player.BulletPrefab = selectedBulletPrefab;
        Debug.Log("랜덤 총알 프리팹 선택: " + selectedBulletPrefab.name);

    }

}
