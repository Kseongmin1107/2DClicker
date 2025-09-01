using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPopup : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject gameObject;
    public float showDuration = 1.2f;
    public float fadeDuration = 0.25f;

    Coroutine current;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(GameObject obj)
    {
        if (current != null)
        {
            StopCoroutine(current);
        }
        current = StartCoroutine(Coshow(obj));
    }

    IEnumerator Coshow(GameObject obj)
    {
        gameObject = obj;
        canvasGroup.alpha = 1f;
        gameObject.SetActive(true);

        yield return new WaitForSeconds(showDuration);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f,0f, t/ fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
        current = null;

    }
}
