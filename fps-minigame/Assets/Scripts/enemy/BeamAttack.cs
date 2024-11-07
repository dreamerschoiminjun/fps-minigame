using System.Collections.Generic;
using UnityEngine;

public class BeamAttack : MonoBehaviour
{
    [Header("References")]
    public GameObject beamStart; // 빔 시작 지점
    public GameObject beamEnd; // 빔 끝 지점
    public GameObject beam; // 빔 본체 (기존에 LineRenderer가 추가되어 있음)

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; // 레이저 끝 위치 조정
    private List<Vector3> playerPositions; // 플레이어 위치 추적용 리스트
    private GameObject player; // 플레이어 오브젝트
    private float positionDelay = 0.2f; // 0.2초 전 위치를 추적
    private int frameBuffer; // 프레임마다 기록할 버퍼
    private bool isFiring = true; // 자동 공격 상태
    private float damageInterval = 0.1f; // 데미지 주는 간격
    private float attackDuration = 2f; // 공격 지속 시간
    private float attackCooldown = 0.5f; // 공격 쿨타임
    private float damageTimer = 0f; // 데미지 타이머
    private bool isDamageActive = false; // 데미지 활성화 상태

    // 초기화
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerPositions = new List<Vector3>();

        // 0.2초 지연을 위해 버퍼 계산 (대략 5프레임 전 위치를 추적)
        frameBuffer = Mathf.CeilToInt(positionDelay / Time.fixedDeltaTime);
    }

    // 매 프레임마다 플레이어 위치를 추적
    void FixedUpdate()
    {
        if (player != null)
        {
            // 현재 플레이어 위치를 리스트에 추가
            playerPositions.Add(player.transform.position);

            // 버퍼 크기를 넘어가면 가장 오래된 위치 제거
            if (playerPositions.Count > frameBuffer)
            {
                playerPositions.RemoveAt(0);
            }
        }
    }

    // 매 프레임마다 실행
    void Update()
    {
        if (isFiring && playerPositions.Count >= frameBuffer)
        {
            // 0.2초 전 플레이어 위치로 빔 발사
            Vector3 delayedPosition = playerPositions[0]; // 0.2초 전 위치 (가장 오래된 값)
            Vector3 direction = delayedPosition - transform.position;
            ShootBeamInDir(transform.position, direction);

            // 적에게 지속적으로 데미지 입히기
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Player")) // 플레이어가 태그가 "Player"인 경우
                {
                    if (isDamageActive)
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

        // 빔 종료 로직 추가
        if (beam != null)
        {
            Destroy(beamStart, attackDuration);
            Destroy(beamEnd, attackDuration);
            Destroy(beam, attackDuration);
            isFiring = false; // 공격 상태를 비활성화
            Invoke("StartAttackCooldown", attackCooldown); // 쿨타임 후 재시작
        }
    }

    // 데미지를 플레이어에게 입히는 함수
    private void TakeDamage(GameObject player)
    {
        CharacterStats characterStats = player.GetComponent<CharacterStats>(); // CharacterStats로 수정
        EnemyStats enemyStats = GetComponent<EnemyStats>(); // EnemyStats 참조
        if (characterStats != null)
        {
            characterStats.CurrentHealth -= enemyStats.attackPower; // CharacterStats의 CurrentHealth를 감소
        }
    }

    // 빔을 특정 방향으로 발사하는 함수
    void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        LineRenderer line = beam.GetComponent<LineRenderer>(); // 기존의 LineRenderer 가져오기
        line.positionCount = 2;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
            end = hit.point - (dir.normalized * beamEndOffset);
        else
            end = start + (dir * 100);

        beamEnd.transform.position = end;
        line.SetPosition(1, end);
        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);
    }

    // 공격 쿨타임 후 공격 재시작
    private void StartAttackCooldown()
    {
        isFiring = true; // 공격 상태를 활성화
        isDamageActive = true; // 데미지 활성화
        damageTimer = 0f; // 타이머 초기화
    }
}
