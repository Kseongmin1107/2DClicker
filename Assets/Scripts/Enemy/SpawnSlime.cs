using System.Collections;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    [Header("Prefabs (Project 창의 프리팹 자산을 드래그!)")]
    [SerializeField] private GameObject samePrefab;   // 기본 리스폰 프리팹 (보통 자기 자신 프리팹)
    [SerializeField] private GameObject nextPrefab;   // N번 죽이면 교체될 프리팹
    [SerializeField, Min(1)] private int killsToChange = 3;

    [Header("Placement")]
    [SerializeField] private bool respawnAtOriginalPos = true;
    [SerializeField] private float respawnDelay = 0f;

    // 한 화면에 슬라임 1마리 → static 카운터로 간단히 유지
    private static int deathCount = 0;

    private Vector3 originalPos;
    private Quaternion originalRot;
    private Transform originalParent;

    private void Awake()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        originalParent = transform.parent;

        // 절대 런타임 인스턴스(gameObject)로 대체하지 말 것!
        // samePrefab/nextPrefab은 반드시 Project 창의 프리팹 자산이어야 함.
    }

    /// <summary>Death 애니 끝에서 DestroyOnDeath가 호출.</summary>
    public void HandleDeathAndRespawn(Transform deadRoot)
    {
        deathCount++;

        // 어떤 프리팹으로 리스폰할지 결정
        GameObject prefab = samePrefab;
        if (killsToChange > 0 && deathCount >= killsToChange && nextPrefab != null)
        {
            prefab = nextPrefab;
            deathCount = 0; // 사이클 리셋 (원하면 누적 유지로 변경 가능)
        }

        // 스폰 위치/부모
        Vector3 pos = respawnAtOriginalPos ? originalPos : deadRoot.position;
        Quaternion rot = respawnAtOriginalPos ? originalRot : deadRoot.rotation;
        Transform parent = originalParent != null ? originalParent : deadRoot.parent;

        if (respawnDelay > 0f)
        {
            StartCoroutine(RespawnAfterDelay(prefab, pos, rot, parent, deadRoot.gameObject, respawnDelay));
        }
        else
        {
            Object.Instantiate(prefab, pos, rot, parent);
            Object.Destroy(deadRoot.gameObject);
        }
    }

    private IEnumerator RespawnAfterDelay(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent, GameObject toDestroy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Object.Instantiate(prefab, pos, rot, parent);
        Object.Destroy(toDestroy);
    }
}
