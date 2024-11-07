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
        gameItems.Add(new GameItem
        {
            id = 1,
            itemName = "활력의 반지",
            description = "최대 체력이 10% 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 2
        gameItems.Add(new GameItem
        {
            id = 2,
            itemName = "공격의 부적",
            description = "공격력이 10% 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 3
        gameItems.Add(new GameItem
        {
            id = 3,
            itemName = "신속의 부츠",
            description = "공격 속도가 10% 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 4
        gameItems.Add(new GameItem
        {
            id = 4,
            itemName = "영혼의 부적",
            description = "영혼 획득량이 10% 증가합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 5
        gameItems.Add(new GameItem
        {
            id = 5,
            itemName = "시간의 부적",
            description = "스킬의 쿨타임이 10% 감소합니다.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });
    }

    public List<GameItem> GetGameItems()
    {
        return gameItems;
    }

    // id 값을 기반으로 GameItem을 가져오는 메서드
    public GameItem GetGameItemById(int id) // 메서드 이름 수정
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
