using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPopup : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float showDuration = 1.2f;
    public float fadeDuration = 0.25f;

    Coroutine current;

    public void Show()
    {
        if (current != null)
        {
            StopCoroutine(current);
        }
        current = StartCoroutine(Coshow());
    }

    IEnumerator Coshow()
    {
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(showDuration);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f,0f, t/ fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        current = null;

    }
}
