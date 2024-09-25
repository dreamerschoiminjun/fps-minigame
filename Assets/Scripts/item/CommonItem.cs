using System.Collections.Generic;
using UnityEngine;

public class CommonItem : MonoBehaviour
{
    public static CommonItem instance;

    public List<GameItem> gameItems;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        gameItems = new List<GameItem>();

        // Item 1
        GameItem item1 = ScriptableObject.CreateInstance<GameItem>();
        item1.id = 1;
        item1.itemName = "활력의 반지";
        item1.itemGrade = "Common";
        item1.itemIcon = null;
        item1.itemsimpleDescription = "생명력 증가";
        item1.itemDescription = "최대 체력이 10% 증가합니다.";
        item1.itemSynergy = null;
        item1.itemUnlockMethod = null;
        gameItems.Add(item1);

        // Item 2
        GameItem item2 = ScriptableObject.CreateInstance<GameItem>();
        item2.id = 2;
        item2.itemName = "공격의 부적";
        item2.itemGrade = "Common";
        item2.itemIcon = null;
        item2.itemsimpleDescription = "공격력 증가";
        item2.itemDescription = "공격력이 10% 증가합니다.";
        item2.itemSynergy = null;
        item2.itemUnlockMethod = null;
        gameItems.Add(item2);

        // Item 3
        GameItem item3 = ScriptableObject.CreateInstance<GameItem>();
        item3.id = 3;
        item3.itemName = "신속의 부츠";
        item3.itemGrade = "Common";
        item3.itemIcon = null;
        item3.itemsimpleDescription = "공격 속도 증가";
        item3.itemDescription = "공격 속도가 10% 증가합니다.";
        item3.itemSynergy = null;
        item3.itemUnlockMethod = null;
        gameItems.Add(item3);

        // Item 4
        GameItem item4 = ScriptableObject.CreateInstance<GameItem>();
        item4.id = 4;
        item4.itemName = "영혼의 부적";
        item4.itemGrade = "Common";
        item4.itemIcon = null;
        item4.itemsimpleDescription = "영혼 획득량 증가";
        item4.itemDescription = "영혼 획득량이 10% 증가합니다.";
        item4.itemSynergy = null;
        item4.itemUnlockMethod = null;
        gameItems.Add(item4);

        // Item 5
        GameItem item5 = ScriptableObject.CreateInstance<GameItem>();
        item5.id = 5;
        item5.itemName = "시간의 부적";
        item5.itemGrade = "Common";
        item5.itemIcon = null;
        item5.itemsimpleDescription = "쿨타임 감소";
        item5.itemDescription = "스킬의 쿨타임이 10% 감소합니다.";
        item5.itemSynergy = null;
        item5.itemUnlockMethod = null;
        gameItems.Add(item5);

    }
    public List<GameItem> GetGameItems()
    {
        return gameItems;
    }

    // id 값을 기반으로 GameItem을 가져오는 메서드 추가
    public GameItem GetGameItemByid(int id)
    {
        foreach (GameItem item in gameItems)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}
