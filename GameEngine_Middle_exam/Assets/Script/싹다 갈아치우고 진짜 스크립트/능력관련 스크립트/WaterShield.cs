using UnityEngine;
using System.Collections;

public class WaterShield : MonoBehaviour
{
    public bool isWater = true;

    public float shieldDuration = 3f;
    public float invincibleDuration = 0.5f;
    public float cooldown = 15f;

    private bool isOnCooldown = false;
    private bool isInvincible = false;
    private bool isShieldActive = false;

    void Update()
    {
        if (isWater && Input.GetKeyDown(KeyCode.W) && !isOnCooldown)
        {
            StartCoroutine(ActivateShield());
        }
    }

    IEnumerator ActivateShield()
    {
        isOnCooldown = true;
        isShieldActive = true;

        isInvincible = true;
        Debug.Log("무적 시작");

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        Debug.Log("무적 종료");

        yield return new WaitForSeconds(shieldDuration - invincibleDuration);

        isShieldActive = false;
        Debug.Log("쉴드 종료");

        yield return new WaitForSeconds(cooldown);

        isOnCooldown = false;
        Debug.Log("쿨타임 종료");
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }
}