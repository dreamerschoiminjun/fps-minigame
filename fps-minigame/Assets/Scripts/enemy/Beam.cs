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
    private GameObject player; // �÷��̾� ������Ʈ

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f;
    public float textureScrollSpeed = 8f;
    public float textureLengthScale = 3;

    [Header("Damage Settings")]
    public float damageInterval = 0.1f; // �������� �ִ� ����
    private float damageTimer = 0f; // ������ Ÿ�̸�
    private bool isDamageActive = false; // ������ Ȱ��ȭ ����

    void Start()
    {
        InitializeBeam();
        player = GameObject.FindWithTag("Player"); // �±׷� �÷��̾� ã��
        DeactivateBeam(); // �ʱ⿡�� ���� ��Ȱ��ȭ ���·� ����
    }

    void Update()
    {
        // ���� �׻� ������Ʈ ��ġ�� �÷��̾� ��ġ ���̿� �����ǵ��� ����
        if (beam && line && player != null && isDamageActive)
        {
            Vector3 direction = player.transform.position - transform.position;
            ShootBeamInDir(transform.position, direction);

            // �÷��̾�� ���������� ������ ������
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Player")) // �浹�� ��ü�� �÷��̾����� Ȯ��
                {
                    damageTimer += Time.deltaTime;
                    if (damageTimer >= damageInterval)
                    {
                        TakeDamage(hit.collider.gameObject); // �÷��̾�� ������ ������
                        damageTimer = 0f; // Ÿ�̸� �ʱ�ȭ
                    }
                }
            }
        }
    }

    private void InitializeBeam()
    {
        // �ʱ� �� ����, �� ��ũ��Ʈ�� ���� ������Ʈ�� ��ġ�� ���� �������� ���
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

    // ���� Ȱ��ȭ�ϴ� �Լ�
    public void ActivateBeam()
    {
        beamStart.SetActive(true);
        beamEnd.SetActive(true);
        beam.SetActive(true);
        isDamageActive = true; // ������ Ȱ��ȭ
    }

    // ���� ��Ȱ��ȭ�ϴ� �Լ�
    public void DeactivateBeam()
    {
        beamStart.SetActive(false);
        beamEnd.SetActive(false);
        beam.SetActive(false);
        isDamageActive = false; // ������ ��Ȱ��ȭ
    }

    // �������� �÷��̾�� ������ �Լ�
    private void TakeDamage(GameObject player)
    {
        CharacterStats characterStats = player.GetComponent<CharacterStats>(); // �÷��̾��� CharacterStats ������Ʈ ��������
        EnemyStats enemyStats = GetComponent<EnemyStats>(); // ���� EnemyStats ������Ʈ ����
        if (characterStats != null && enemyStats != null)
        {
            characterStats.CurrentHealth -= enemyStats.attackPower; // �÷��̾��� ü���� ���� ���ݷ¸�ŭ ����
        }
    }
}