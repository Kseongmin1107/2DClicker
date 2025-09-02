using System.Collections;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    [Header("Prefabs (Project â�� ������ �ڻ��� �巡��!)")]
    [SerializeField] private GameObject samePrefab;   // �⺻ ������ ������ (���� �ڱ� �ڽ� ������)
    [SerializeField] private GameObject nextPrefab;   // N�� ���̸� ��ü�� ������
    [SerializeField, Min(1)] private int killsToChange = 3;

    [Header("Placement")]
    [SerializeField] private bool respawnAtOriginalPos = true;
    [SerializeField] private float respawnDelay = 0f;

    // �� ȭ�鿡 ������ 1���� �� static ī���ͷ� ������ ����
    private static int deathCount = 0;

    private Vector3 originalPos;
    private Quaternion originalRot;
    private Transform originalParent;

    private void Awake()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        originalParent = transform.parent;

        // ���� ��Ÿ�� �ν��Ͻ�(gameObject)�� ��ü���� �� ��!
        // samePrefab/nextPrefab�� �ݵ�� Project â�� ������ �ڻ��̾�� ��.
    }

    /// <summary>Death �ִ� ������ DestroyOnDeath�� ȣ��.</summary>
    public void HandleDeathAndRespawn(Transform deadRoot)
    {
        deathCount++;

        // � ���������� ���������� ����
        GameObject prefab = samePrefab;
        if (killsToChange > 0 && deathCount >= killsToChange && nextPrefab != null)
        {
            prefab = nextPrefab;
            deathCount = 0; // ����Ŭ ���� (���ϸ� ���� ������ ���� ����)
        }

        // ���� ��ġ/�θ�
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
