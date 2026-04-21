using UnityEngine;

public class HookSpawner : MonoBehaviour
{
    public Transform player;
    public Transform mouseFollower;
    public GameObject hookPrefab;
    public float distanceFromPlayer = 0.4f;

    private GameObject currentHook;
    private ElementalChecker elementalChecker;

    void Start()
    {
        elementalChecker = player.GetComponent<ElementalChecker>();
    }

    void Update()
    {
        UpdatePosition();

        if (Input.GetMouseButtonDown(1))
        {
            if (elementalChecker != null && elementalChecker.CanGrapple())
                TrySpawnHook();
        }

        if (Input.GetMouseButtonUp(1)) RemoveHook();
    }

    void UpdatePosition()
    {
        Vector2 dir = (mouseFollower.position - player.position).normalized;
        transform.position = (Vector2)player.position + dir * distanceFromPlayer;
        transform.right = dir;
    }

    void TrySpawnHook()
    {
        if (currentHook != null)
        {
            Debug.Log("이미 훅이 있음");
            return;
        }

        Debug.Log("Nature 상태인가에 대해서, " + (elementalChecker != null ? elementalChecker.CanGrapple().ToString() : "ElementalChecker null"));

        currentHook = Instantiate(hookPrefab, transform.position, transform.rotation);
        Debug.Log("훅 생성됨 =  " + currentHook.name + " || 위치 = " + transform.position);

        Hook hook = currentHook.GetComponent<Hook>();
        Debug.Log("Hook 컴포넌트+= " + (hook == null ? "null" : "있음"));

        PlayerController pc = player.GetComponent<PlayerController>();
        Debug.Log("PlayerController: " + (pc == null ? "null" : "있음"));

        hook.Init(pc);
    }

    void RemoveHook()
    {
        if (currentHook != null)
        {
            Destroy(currentHook);
            currentHook = null;
            Debug.Log("훅 제거됨");
        }
    }
}
