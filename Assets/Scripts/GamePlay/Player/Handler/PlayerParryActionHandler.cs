using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryActionHandler : MonoBehaviour
{
    PlayerController playerController;

    //패리 성공 당시 정보들
    ProjectileBase parriedProjectile; //패리 성공한 투사체
    Vector2 parriedPosition;          //패리 성공한 위치
    GameObject parriedProjectileSender; //패리한 투사체를 발사했던 곳
    GameObject parriedProjectileCopy; //패리 성공한 투사체 복사본

    //현재 선택된 방패 및 패리 모드
    ShieldDataSO currentShield;
    ParryModeDataSO currentParryMode;

    //Proximity 패리모드 관련
    float ProximityMaxRadius = 10f;
    LayerMask EnemyAndTrapLayerMask;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        EnemyAndTrapLayerMask = LayerMask.GetMask("Enemy", "Trap");
    }
    private void OnEnable()
    {
        //현재 선택된 방패가 업데이트 되었을 때
        PlayerEvents.OnCurrentShieldUpdated += CurrentShieldUpdated;

        //현재 선택된 패리모드가 업데이트 되었을 때
        PlayerEvents.OnCurrentParryModeUpdated += CurrentParryModeUpdated;

        //PlayerParry가 성공했을 때
        PlayerEvents.OnPlayerParrySuccess += ParrySuccess;

        //PlayerParry가 투사체 패링에 성공했을 때
        PlayerEvents.OnProjectileParried += ProjectileParried;

    }
    private void OnDisable()
    {
        //현재 선택된 방패가 업데이트 되었을 때
        PlayerEvents.OnCurrentShieldUpdated -= CurrentShieldUpdated;

        //현재 선택된 패리모드가 업데이트 되었을 때
        PlayerEvents.OnCurrentParryModeUpdated -= CurrentParryModeUpdated;

        //PlayerParry가 성공했을 때
        PlayerEvents.OnPlayerParrySuccess -= ParrySuccess;

        //PlayerParry가 투사체 패링에 성공했을 때
        PlayerEvents.OnProjectileParried -= ProjectileParried;
    }
    //현재 선택된 방패가 업데이트 되었을 때
    void CurrentShieldUpdated(ShieldDataSO curShield, int curIndex)
    {
        currentShield = curShield;
        playerController.currentShield = curShield;
    }
    //현재 선택된 패리모드가 업데이트 되었을 때
    void CurrentParryModeUpdated(ParryModeDataSO curParryMode, int curIndex)
    {
        currentParryMode = curParryMode;
        playerController.currentParryMode = curParryMode;
    }

    //PlayerParry가 성공했을 때
    void ParrySuccess()
    {
        //패리 성공 보상
        //일시적 무적
        playerController.InvincibleTimer = playerController.parrySuccessInvincibleTime;
        playerController.isInvincible = true;
        //공중에서 기술 사용횟수 초기화
        playerController.isParriedInAir = false;
        playerController.isDashedInAir = false;
        playerController.jumpCount = 1;
    }

    //PlayerParry가 투사체 패링에 성공했을 때
    void ProjectileParried(ProjectileBase prefab, GameObject sender)
    {
        parriedProjectile = prefab;
        parriedPosition = prefab.gameObject.transform.position;
        parriedProjectileSender = sender;

        if(currentShield.shieldType == ShieldType.Reflect && currentParryMode.parryModeType != ParryModeType.Absorb)
            parriedProjectileCopy = Instantiate(prefab.gameObject, parriedPosition, Quaternion.identity);

        HandleParryMode();
    }
    //ParryMode에 따라서 Direction 혹은 Target 계산
    void HandleParryMode()
    {
        GameObject target = null;
        Vector2 direction = Vector2.zero;

        //계산 진행
        switch (currentParryMode.parryModeType)
        {
            case ParryModeType.Counter:

                if (parriedProjectileSender != null)                
                    target = parriedProjectileSender;                
                else
                    direction = new Vector2(playerController.isHeadToRight, 0);

                HandleShieldType(target, direction);

                break;

            case ParryModeType.Proximity:
                target = FindProximityObject(ProximityMaxRadius);

                if (target == null)
                    direction = new Vector2(playerController.isHeadToRight, 0);

                HandleShieldType(target, direction);

                break;

            case ParryModeType.Directional:

                StartCoroutine(FindDirection());

                break;

            case ParryModeType.Absorb:
                Debug.Log("Attack Absorbed!");

                break;
        }
    }
    //패리모드가 Proximity일 때 범위 내 가장 가까운 적 찾는 메서드
    GameObject FindProximityObject(float radius)
    {
        Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, radius, EnemyAndTrapLayerMask);

        GameObject ProximityObject = null;
        float minDistance = -1;
        if (Objects.Length != 0)
        {
            ProximityObject = Objects[0].gameObject;
            minDistance = (Objects[0].transform.position - transform.position).sqrMagnitude;
        }
        foreach (Collider2D candidate in Objects)
        {
            float distance = (candidate.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                ProximityObject = candidate.gameObject;
                minDistance = distance;
            }
        }

        return ProximityObject;
    }
    //패리모드가 Direction일 때 방향 계산하는 코루틴 메서드
    IEnumerator FindDirection()
    {
        float slowModeScale = 0.0f;
        float confirmDelay = 0.2f;
        float maxWaitTime = 1.6f;

        Time.timeScale = slowModeScale;

        Vector2 inputDir = Vector2.zero;
        Vector2 finalDir = Vector2.zero;

        bool firstInputDetected = false;
        float waitTimer = 0f;
        float confirmTimer = 0f;

        //일단 입력 가능 상태까지 대기
        yield return new WaitForSecondsRealtime(0.5f);

        while (waitTimer < maxWaitTime)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // 첫 입력 감지
            if (!firstInputDetected && input.sqrMagnitude > 0.01f)
            {
                inputDir = input.normalized;
                finalDir = inputDir;

                firstInputDetected = true;
                confirmTimer = 0f; // 0.2초 카운트다운 시작
            }

            // 첫 입력이 감지된 이후
            if (firstInputDetected)
            {
                // 0.2초 동안 방향 갱신
                if (confirmTimer < confirmDelay)
                {
                    confirmTimer += Time.unscaledDeltaTime;
                    if (input.sqrMagnitude > 0.01f)
                    {
                        inputDir = input.normalized;
                        finalDir = inputDir;
                    }
                }
                else // 0.2초 지남 → 방향 확정 및 발사
                {
                    break;
                }
            }

            waitTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        // 1초 동안 입력이 없었던 경우
        if (!firstInputDetected)
        {
            finalDir = new Vector2(playerController.isHeadToRight, 0); // 기본 방향
        }

        Time.timeScale = 1f;
        HandleShieldType(null, finalDir);

    }
    //계산된 Direction 혹은 Target으로 투사체 만들기
    void HandleShieldType(GameObject target, Vector2 direction)
    {
        //만들 투사체
        GameObject projectile = null;

        //투사체 생성
        switch (currentShield.shieldType)
        {
            case ShieldType.Flame:
                projectile = Instantiate(currentShield.parryProjectilePrefab, parriedPosition, Quaternion.identity);

                ShootParryAction(projectile, target, direction);

                break;

            case ShieldType.Reflect:
                projectile = parriedProjectileCopy;
                projectile.layer = LayerMask.NameToLayer("PlayerAttack");
                projectile.GetComponent<Collider2D>().enabled = true;

                ShootParryAction(projectile, target, direction);

                break;

            case ShieldType.Impact:
                projectile = Instantiate(currentShield.parryProjectilePrefab, transform.position, Quaternion.identity);

                ShootParryAction(projectile, target, direction);

                break;
        }
    }
    // 만들어진 투사체를 발사하기
    void ShootParryAction(GameObject projectile, GameObject target, Vector2 direction)
    {
        if (target != null)
        {
            projectile.GetComponent<ProjectileBase>().SetTarget(target, gameObject);
            PlayerEvents.InvokeParryActionTargetSet(target);
        }
        else if (direction != Vector2.zero)
        {
            projectile.GetComponent<ProjectileBase>().SetDirection(direction, gameObject);
            PlayerEvents.InvokeParryActionDirectionSet(direction);
        }
    }



}
