using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;  // 추적할 대상 (일반적으로 플레이어)
    public float speed = 3f;  // 적의 이동 속도
    public int attackPower = 10;  // 공격력
    public float attackSpeed = 1.5f;  // 공격 속도
    public float attackRange = 25f;  // 플레이어와의 공격 범위
    private float lastAttackTime;
    private bool isAttacking = false; // 공격 중인지 여부

    public Beam beam; // Beam 스크립트 참조 추가

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackRange)
            {
                if (!isAttacking)
                {
                    StartAttacking();
                }
                // 공격 중일 때는 움직이지 않도록 설정
                StopMovement();
            }
            else
            {
                if (isAttacking)
                {
                    StopAttacking();
                }
                // 공격 범위 밖에 있을 때는 추적
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        // 플레이어를 향해 이동, Y축 이동 제거
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        playerDirection.y = 0;  // Y축 성분 제거

        transform.Translate(playerDirection * speed * Time.deltaTime, Space.World);
    }

    private void StopMovement()
    {
        // 추가적으로 이동을 멈추도록 필요한 로직이 있을 경우 이곳에 작성
        // 예를 들어, Rigidbody가 사용 중이라면, 속도를 0으로 설정
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }
    }


    private void StartAttacking()
    {
        isAttacking = true;
        beam.ActivateBeam(); // 빔 활성화
    }

    private void StopAttacking()
    {
        isAttacking = false;
        beam.DeactivateBeam(); // 빔 비활성화
    }


    // 공격 범위를 시각적으로 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // 공격 범위를 빨간색으로 설정
        Gizmos.DrawWireSphere(transform.position, attackRange);  // 공격 범위 표시
    }
}
