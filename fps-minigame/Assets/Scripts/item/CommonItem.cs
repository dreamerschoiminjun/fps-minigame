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
            itemName = "Ȱ���� ����",
            description = "�ִ� ü���� 10% �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 2
        gameItems.Add(new GameItem
        {
            id = 2,
            itemName = "������ ����",
            description = "���ݷ��� 10% �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 3
        gameItems.Add(new GameItem
        {
            id = 3,
            itemName = "�ż��� ����",
            description = "���� �ӵ��� 10% �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 4
        gameItems.Add(new GameItem
        {
            id = 4,
            itemName = "��ȥ�� ����",
            description = "��ȥ ȹ�淮�� 10% �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });

        // Item 5
        gameItems.Add(new GameItem
        {
            id = 5,
            itemName = "�ð��� ����",
            description = "��ų�� ��Ÿ���� 10% �����մϴ�.",
            icon = null,
            itemType = GameItem.ItemType.Buff
        });
    }

    public List<GameItem> GetGameItems()
    {
        return gameItems;
    }

    // id ���� ������� GameItem�� �������� �޼���
    public GameItem GetGameItemById(int id) // �޼��� �̸� ����
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