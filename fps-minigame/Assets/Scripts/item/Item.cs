using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Passive };
    public Type type;
    public int id;
    public int value; // 코인과 스피릿의 값을 저장할 변수
    public GameItem gameItem;
    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        gameItem = CommonItem.instance.GetGameItemById(id); // id를 기반으로 gameItem 가져오기
    }


    private void Update()
    {
        if (type == Type.Passive)
        {
            // 패시브 아이템 회전
            transform.Rotate(Vector3.up * 20 * Time.deltaTime);
        }
      
    }


    public void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("충돌 감지: " + other.gameObject.name); // 충돌한 객체의 이름 로그 출력

        if (other.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("플레이어와 충돌"); // 플레이어와 충돌했음을 로그 출력

            CharacterStats characterStats = other.GetComponent<CharacterStats>();

            UnityEngine.Debug.Log("CharacterStats 컴포넌트: " + characterStats); // CharacterStats 컴포넌트 로그 출력

            if (type == Type.Passive)
            {
                if (gameItem != null)
                {
                    characterStats.AddToInventory(gameItem); // 아이템을 인벤토리에 추가합니다.
                    UnityEngine.Debug.Log(gameItem.itemName + "을(를) 획득하였습니다."); // 아이템 획득 로그
                    gameItem.ApplyEffect(characterStats);
                    Destroy(gameObject);
                }
                else
                {
                    UnityEngine.Debug.Log("gameItem이 null입니다.");
                }
            }
        }
    }
}
