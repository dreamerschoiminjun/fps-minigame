using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [Header("Beam Prefabs")]
    public GameObject beamLineRendererPrefab;
    public GameObject beamStartPrefab;
    public GameObject beamEndPrefab;

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
    private LineRenderer line;
    private GameObject player; // 플레이어 오브젝트

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f;
    public float textureScrollSpeed = 8f;
    public float textureLengthScale = 3;

    [Header("Damage Settings")]
    public float damageInterval = 0.1f; // 데미지를 주는 간격
    private float damageTimer = 0f; // 데미지 타이머
    private bool isDamageActive = false; // 데미지 활성화 상태

    void Start()
    {
        InitializeBeam();
        player = GameObject.FindWithTag("Player"); // 태그로 플레이어 찾기
        DeactivateBeam(); // 초기에는 빔을 비활성화 상태로 설정
    }

    void Update()
    {
        // 빔이 항상 오브젝트 위치와 플레이어 위치 사이에 유지되도록 설정
        if (beam && line && player != null && isDamageActive)
        {
            Vector3 direction = player.transform.position - transform.position;
            ShootBeamInDir(transform.position, direction);

            // 플레이어에게 지속적으로 데미지 입히기
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Player")) // 충돌한 객체가 플레이어인지 확인
                {
                    damageTimer += Time.deltaTime;
                    if (damageTimer >= damageInterval)
                    {
                        TakeDamage(hit.collider.gameObject); // 플레이어에게 데미지 입히기
                        damageTimer = 0f; // 타이머 초기화
                    }
                }
            }
        }
    }

    private void InitializeBeam()
    {
        // 초기 빔 생성, 이 스크립트가 붙은 오브젝트의 위치를 시작 지점으로 사용
        beamStart = Instantiate(beamStartPrefab, transform.position, Quaternion.identity);
        beamEnd = Instantiate(beamEndPrefab, transform.position, Quaternion.identity);
        beam = Instantiate(beamLineRendererPrefab, transform.position, Quaternion.identity);
        line = beam.GetComponent<LineRenderer>();
    }

    void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        line.positionCount = 2;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = player.transform.position - (dir.normalized * beamEndOffset);
        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }

    // 빔을 활성화하는 함수
    public void ActivateBeam()
    {
        beamStart.SetActive(true);
        beamEnd.SetActive(true);
        beam.SetActive(true);
        isDamageActive = true; // 데미지 활성화
    }

    // 빔을 비활성화하는 함수
    public void DeactivateBeam()
    {
        beamStart.SetActive(false);
        beamEnd.SetActive(false);
        beam.SetActive(false);
        isDamageActive = false; // 데미지 비활성화
    }

    // 데미지를 플레이어에게 입히는 함수
    private void TakeDamage(GameObject player)
    {
        CharacterStats characterStats = player.GetComponent<CharacterStats>(); // 플레이어의 CharacterStats 컴포넌트 가져오기
        EnemyStats enemyStats = GetComponent<EnemyStats>(); // 적의 EnemyStats 컴포넌트 참조
        if (characterStats != null && enemyStats != null)
        {
            characterStats.CurrentHealth -= enemyStats.attackPower; // 플레이어의 체력을 적의 공격력만큼 감소
        }
    }
}
