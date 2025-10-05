using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryActionHandler : MonoBehaviour
{
    PlayerController playerController;

    //현재 선택된 방패 및 패리 모드
    ShieldDataSO currentShield;
    ParryModeDataSO currentParryMode;

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
    }
    //현재 선택된 패리모드가 업데이트 되었을 때
    void CurrentParryModeUpdated(ParryModeDataSO curParryMode, int curIndex)
    {
        currentParryMode = curParryMode;
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
        //발사될 투사체
        GameObject projectile = null;

        //투사체 설정
        switch (currentShield.shieldType)
        {
            case ShieldType.Flame:
                projectile = Instantiate(currentShield.parryProjectilePrefab, prefab.gameObject.transform.position, Quaternion.identity);

                break;

            case ShieldType.Reflect:
                projectile = Instantiate(prefab.gameObject, prefab.gameObject.transform.position, Quaternion.identity);
                projectile.layer = LayerMask.NameToLayer("PlayerAttack");

                break;
        }
        //패리 모드 설정
        switch (currentParryMode.parryModeType)
        {
            case ParryModeType.Counter:
                projectile.GetComponent<ProjectileBase>().SetTarget(sender, gameObject);

                break;

            case ParryModeType.Proximity:
                GameObject target = FindProximityObject(ProximityMaxRadius);
                projectile.GetComponent<ProjectileBase>().SetTarget(target, gameObject);
                break;

            case ParryModeType.Directional:
                StartCoroutine(FindDirectionAndShoot(projectile));
                break;

            case ParryModeType.Absorb:
                Debug.Log("Attack Absorbed!");
                Destroy(projectile);

                break;
        }

    }
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
    IEnumerator FindDirectionAndShoot(GameObject projectile)
    {
        float slowModeScale = 0.0f;
        float confirmDelay = 0.2f;
        float maxWaitTime = 1.0f;

        Time.timeScale = slowModeScale;

        Vector2 inputDir = Vector2.zero;
        Vector2 finalDir = Vector2.right;

        bool firstInputDetected = false;
        float waitTimer = 0f;
        float confirmTimer = 0f;

        yield return new WaitForSecondsRealtime(0.2f);

        while (waitTimer < maxWaitTime)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // 첫 입력 감지
            if (!firstInputDetected && input.sqrMagnitude > 0.01f)
            {
                firstInputDetected = true;
                inputDir = input.normalized;
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
                        inputDir = input.normalized;
                }
                else
                {
                    // 0.2초 지남 → 방향 확정 및 발사
                    finalDir = inputDir;
                    break;
                }
            }

            waitTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        // 1초 동안 입력이 없었던 경우
        if (!firstInputDetected)
        {
            finalDir = Vector2.right; // 기본 방향
        }

        Time.timeScale = 1f;
        projectile.GetComponent<ProjectileBase>().SetDirection(finalDir, gameObject);

    }

}
