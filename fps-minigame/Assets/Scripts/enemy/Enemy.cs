using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;  // ������ ��� (�Ϲ������� �÷��̾�)
    public float speed = 3f;  // ���� �̵� �ӵ�
    public int attackPower = 10;  // ���ݷ�
    public float attackSpeed = 1.5f;  // ���� �ӵ�
    public float attackRange = 25f;  // �÷��̾���� ���� ����
    private float lastAttackTime;
    private bool isAttacking = false; // ���� ������ ����

    public Beam beam; // Beam ��ũ��Ʈ ���� �߰�

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
                // ���� ���� ���� �������� �ʵ��� ����
                StopMovement();
            }
            else
            {
                if (isAttacking)
                {
                    StopAttacking();
                }
                // ���� ���� �ۿ� ���� ���� ����
                ChasePlayer();
            }
        }
    }

    private void ChasePlayer()
    {
        // �÷��̾ ���� �̵�, Y�� �̵� ����
        Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
        playerDirection.y = 0;  // Y�� ���� ����

        transform.Translate(playerDirection * speed * Time.deltaTime, Space.World);
    }

    private void StopMovement()
    {
        // �߰������� �̵��� ���ߵ��� �ʿ��� ������ ���� ��� �̰��� �ۼ�
        // ���� ���, Rigidbody�� ��� ���̶��, �ӵ��� 0���� ����
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
        }
    }


    private void StartAttacking()
    {
        isAttacking = true;
        beam.ActivateBeam(); // �� Ȱ��ȭ
    }

    private void StopAttacking()
    {
        isAttacking = false;
        beam.DeactivateBeam(); // �� ��Ȱ��ȭ
    }


    // ���� ������ �ð������� ǥ��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  // ���� ������ ���������� ����
        Gizmos.DrawWireSphere(transform.position, attackRange);  // ���� ���� ǥ��
    }
}