using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance
    {
        get; private set;
    }
    public WarningPopup warningPopup;
    public CanvasGroup fadePanel;
    public float fadeDuration = 0.8f;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnSpendFailed += HandleSpendFailed;
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnSpendFailed -= HandleSpendFailed;
        }
    }

    private void HandleSpendFailed(GameObject popup)
    {
        popup.SetActive(true);
    }

    public void FadeIn()
    {
        StartCoroutine(CoFade(1, 0));
    }

    public void FadeOut()
    {
        StartCoroutine(CoFade(0, 1));
    }

    public void LoadScene(string SceneName)
    {
        StartCoroutine(FadeAndLoadScene(SceneName));
    }

    public void ResetAndLoad(string SceneName)
    {
        StartCoroutine(CoResetAndLoad(SceneName));
    }

    private IEnumerator CoFade(float from, float to)
    {
        float t = 0;
        fadePanel.blocksRaycasts = true;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = to;
        fadePanel.blocksRaycasts = (to != 0);
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        yield return CoFade(0, 1);

        SceneManager.LoadScene(sceneName);

        yield return CoFade(1, 0);
    }

    private IEnumerator CoResetAndLoad(string sceneName)
    {
        yield return CoFade(0, 1);

        GameManager.Instance.ResetToDefaults();
               
        SceneManager.LoadScene(sceneName);

        yield return CoFade(1, 0);
    }
}
