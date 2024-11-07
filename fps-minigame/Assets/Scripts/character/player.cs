using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]                                                       // 유니티 내부에서 확인
    public Transform characterBody;                                        // 해당되는 개체를 드래그 후 지정하면 맞게 사용
    public Transform cameraArm;                                            // 해당되는 개체를 드래그 후 지정하면 맞게 사용
    public float applySpeed;                                               // 적용되는 속도를 변수로 만듦
    public CharacterStats characterStats;                                  // 캐릭터 스탯에서 필요한 값 받아오기 위한 변수

    public float WaterHeight = 15.5f;                                      // 물의 높이 설정
    private bool isInWater = false;                                        // 캐릭터가 물에 있는지 여부 확인 변수
    private float originalSpeed;                                           // 원래의 이동 속도 저장
    private float originalDrag;                                            // 원래의 Drag 값 저장
    private float waterDrag = 5f;                                          // 물 속에서의 Drag 값

    bool jump;                                                             // 점프 여부 확인 변수
    bool isJump;                                                           // 땅에서만 점프 가능하게 하는 변수
    

    float hAxis;                                                           // 키값 받기위한 변수
    float vAxis;                                                           // 키값 받기위한 변수

    bool isBorder;                                                         // 벽관통 막는 변수    

    Vector3 moveVec;                                                       // 조건 설정을 위한 벡터
    Rigidbody rigid;                                                       // 물리효과 구현
    Animator anim;                                                         // 애니메이션 넣기 위한 변수

    // 총알 프리팹 배열 추가
    public GameObject[] BulletPrefabs;                                     // 여러 종류의 총알 프리팹

    // 원거리 공격 구현 함수에 필요한 변수
    public GameObject BulletPrefab;                                        // 발사체 프리팹
    public Transform gunTransform;                                         // 발사 위치로 사용할 Transform
    private bool isFireReady;                                              // 발사 준비 여부
    bool attack;                                                           // 공격 여부 확인 변수

    void Start()
    {
        rigid = GetComponent<Rigidbody>();                                 // Rigidbody 컴포넌트 가져오기
        originalDrag = rigid.drag;                                         // 원래의 Drag 값 저장

        anim = characterBody.GetComponent<Animator>();                     // Animator 컴포넌트 가져오기

        // 애니메이팅에 사용하기 위한 변수 설정
        anim.SetBool("isWalk", false);                                     // 걷기 상태 초기화
        anim.SetBool("isStrafeRight", false);                              // 오른쪽으로 스트레이프 상태 초기화
        anim.SetBool("isStrafeLeft", false);                               // 왼쪽으로 스트레이프 상태 초기화
        anim.SetBool("isWalkBack", false);                                 // 뒤로 걷기 상태 초기화
        applySpeed = characterStats.MoveSpeed;                             // 이동 속도 초기화
        originalSpeed = applySpeed;                                        // 원래의 이동 속도 저장
    }

    void Update()
    {
        CheckForWaterHeight();                                             // 물 높이 체크
        GetInput();                                                        // 입력 값 받기
        Aim();                                                             // 조준 기능 호출
        Move();                                                            // 이동 기능 호출
        Jump();                                                            // 점프 기능 호출
        BulletAttack();                                                    // 원거리 공격 기능 호출
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");                            // 수평 입력 값 받기
        vAxis = Input.GetAxisRaw("Vertical");                              // 수직 입력 값 받기
    }

    void Aim()
    {
        // 마우스 움직임 값을 받아옵니다.
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // 카메라의 현재 각도를 받아옵니다.
        Vector3 camAngle = cameraArm.localEulerAngles;

        // 마우스의 Y축 이동에 따라 카메라의 X축 회전을 계산합니다.
        float x = camAngle.x - mouseDelta.y;

        // X축 회전을 -45도와 45도 사이로 제한합니다.
        if (x > 180f) x -= 360f; // 360도 값 때문에 발생할 수 있는 문제를 방지
        x = Mathf.Clamp(x, -45f, 45f);

        // 카메라의 회전을 적용합니다.
        cameraArm.localEulerAngles = new Vector3(x, camAngle.y, 0);

        // 마우스의 X축 이동에 따라 캐릭터의 Y축 회전을 계산하여 적용합니다.
        float yRotation = transform.eulerAngles.y + mouseDelta.x;
        transform.eulerAngles = new Vector3(0, yRotation, 0);
    }


    void Move() // tps 캐릭터 움직임 구현
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;                                          // 이동 벡터 계산

        if (Input.GetKey(KeyCode.LeftShift) && !isInWater && !attack)
        {

            applySpeed = characterStats.SprintSpeed;                                                // 스프린트 속도 적용
            anim.speed = 2.0f;                                                                      // 달릴 때 애니메이션 속도를 2배로 설정
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || isInWater)
        {

            applySpeed = originalSpeed;                                                             // 원래 속도 적용
            anim.speed = 1.0f;                                                                      // 애니메이션 속도를 정상으로 설정
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));    // 이동 입력 값 받기
        bool isMove = moveInput.magnitude != 0;                                                     // 이동 여부 확인
        anim.SetBool("isWalk", vAxis > 0);                                                          // vAxis가 양수일 때만 isWalk를 true로 설정

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;  // 전방 벡터 계산
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0, cameraArm.right.z).normalized;        // 오른쪽 벡터 계산
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;                      // 이동 방향 계산

            characterBody.forward = lookForward;                                                    // 캐릭터의 전방을 설정
            if (!isBorder)
                transform.position += moveDir * Time.deltaTime * applySpeed;                        // 이동 적용
            UnityEngine.Debug.DrawRay(transform.position, lookForward, Color.green);                 // 이동 방향 디버그
            isBorder = Physics.Raycast(transform.position, lookForward, 1, LayerMask.GetMask("Wall")); // 벽과의 충돌 감지

            // 스트레이프 동작 추가
            if (hAxis > 0)
            {
                anim.SetBool("isStrafeRight", true);                                                // 오른쪽으로 스트레이프 애니메이션 설정
                anim.SetBool("isStrafeLeft", false);
                anim.SetBool("isWalkBack", false);
            }
            else if (hAxis < 0)
            {
                anim.SetBool("isStrafeRight", false);
                anim.SetBool("isStrafeLeft", true);                                                 // 왼쪽으로 스트레이프 애니메이션 설정
                anim.SetBool("isWalkBack", false);
            }
            else if (vAxis < 0)
            {
                anim.SetBool("isStrafeRight", false);
                anim.SetBool("isStrafeLeft", false);
                anim.SetBool("isWalkBack", true);                                                   // 뒤로 걷기 애니메이션 설정
            }
            else
            {
                anim.SetBool("isStrafeRight", false);
                anim.SetBool("isStrafeLeft", false);
                anim.SetBool("isWalkBack", false);
            }
        }
        else
        {
            anim.SetBool("isStrafeRight", false);
            anim.SetBool("isStrafeLeft", false);
            anim.SetBool("isWalkBack", false);
        }
    }

    void Jump()
    {
        jump = Input.GetButtonDown("Jump");                                                         // 점프 키 입력 확인
        if ( !attack && jump && !isJump)
        {
            if (vAxis < 0)
            {
                anim.SetTrigger("isJumpBackward");                                                  // 뒤로 점프 애니메이션 트리거
            }
            else
            {
                anim.SetTrigger("isJumpForward");                                                   // 앞으로 점프 애니메이션 트리거
            }
            rigid.AddForce(Vector3.up * characterStats.jumppower, ForceMode.Impulse);               // 점프 힘 적용
            anim.SetBool("isJump", true);                                                           // 점프 상태 설정
            anim.SetTrigger("doJump");                                                              // 점프 애니메이션 트리거
            isJump = true;                                                                          // 점프 중 상태 설정
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")                                                  // 바닥과의 충돌 확인
        {
            isJump = false;                                                                        // 점프 상태 해제
            anim.SetBool("isJump", false);                                                         // 점프 애니메이션 해제
        }
    }

    void BulletAttack()
    {
        bool isMouseDown = Input.GetMouseButton(0);  // 마우스 왼쪽 버튼 입력 확인 (0은 왼쪽 버튼)
        bool isMouseUp = Input.GetMouseButtonUp(0);  // 마우스 왼쪽 버튼이 떼어진 순간 확인
        characterStats.fireDelay += Time.deltaTime;  // 발사 지연 시간 증가 (프레임당 경과 시간 추가)
        isFireReady = characterStats.fireDelay >= characterStats.bulletfireRate;  // 발사 준비 여부 판단 (지연 시간이 발사 속도보다 큰지 확인)

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isInWater && !attack; // 달리는 상태 확인

        if (isMouseDown && isFireReady && !isRunning)  // 발사 준비가 되었고, 달리는 중이 아닐 때
        {
            attack = true;  // 공격 상태를 true로 설정
            anim.SetBool("isFiring", true);  // 발사 애니메이션 시작
            StartCoroutine(Bullet());  // 발사 코루틴 시작
            characterStats.fireDelay = 0;  // 발사 지연 시간 초기화
        }
        else if (isMouseUp || isRunning)  // 마우스 버튼이 떼어진 경우 또는 달리는 중인 경우
        {
            attack = false;  // 공격 상태를 false로 설정
            anim.SetBool("isFiring", false);  // 발사 애니메이션 종료
        }
    }



    IEnumerator Bullet()
    {
        ShootProjectile();                                                                         // 발사체 발사
        yield return new WaitForSeconds(characterStats.bulletfireRate);                            // 발사 속도만큼 대기
        isFireReady = true;                                                                        // 발사 준비 완료
    }

    void ShootProjectile()
    {
        Ray ray = new Ray(cameraArm.position, cameraArm.forward);                                  // 카메라 위치에서 전방으로 광선(Ray) 생성
        RaycastHit hit;                                                                            // RaycastHit 구조체 선언 (충돌 정보 저장)
        Vector3 targetPoint;                                                                       // 목표 지점 벡터 선언

        if (Physics.Raycast(ray, out hit))                                                         // 광선이 무언가에 충돌했는지 확인
        {
            targetPoint = hit.point;                                                               // 광선이 충돌한 지점 설정
        }
        else
        {
            targetPoint = cameraArm.position + cameraArm.forward * 100f;                           // 충돌하지 않으면 임의의 먼 거리 설정
        }

        Vector3 direction = (targetPoint - gunTransform.position).normalized;                      // 발사 방향 계산 (목표 지점에서 총구 위치까지의 방향)
        GameObject projectile = Instantiate(BulletPrefab, gunTransform.position, Quaternion.LookRotation(direction)); // 발사체 생성 (총구 위치에서)
        projectile.transform.forward = direction;                                                  // 발사체의 전방 방향 설정
        projectile.GetComponent<Rigidbody>().AddForce(direction * characterStats.bulletspeed);     // 발사체에 힘을 가하여 발사
    }


    void CheckForWaterHeight()
    {
        if (transform.position.y < WaterHeight)
        {
            isInWater = true;                                                                      // 캐릭터가 물에 있음을 표시
            rigid.useGravity = false;                                                              // 물에 있을 때 중력 비활성화
            rigid.drag = waterDrag;                                                                // 물 속에서의 저항 값 적용
            applySpeed = characterStats.MoveSpeed / 2;                                             // 물에 있을 때 이동 속도 감소
        }
        else
        {
            isInWater = false;                                                                     // 캐릭터가 물 밖에 있음을 표시
            rigid.useGravity = true;                                                               // 물 밖에 있을 때 중력 활성화
            rigid.drag = originalDrag;                                                             // 물 밖에서의 원래 저항 값 복원
            applySpeed = originalSpeed;                                                            // 물 밖에 있을 때 원래 속도 적용
        }
    }
}
