using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Button pauseButton;
    public Button pauseCloseBtn;
    public Button homeButton;
    public Button ResetBtn;
    public Button ResetYesBtn;
    public Button ResetNoBtn;
    public GameObject option;
    public GameObject resetPopup;

    private void Awake()
    {
        pauseButton.onClick.AddListener(Pause);
        pauseCloseBtn.onClick.AddListener(CloseOption);
        homeButton.onClick.AddListener(GoTitle);

        ResetBtn.onClick.AddListener(OpenResetPopup);
        ResetNoBtn.onClick.AddListener(CancelReset);

    }

    void Pause()
    {
        Time.timeScale = 0f;
        option.SetActive(true);
    }

    void CloseOption()
    {
        Time.timeScale = 1f;
        option.SetActive(false);
    }

    void GoTitle()
    {
        Time.timeScale = 1f;
        UIManager.Instance.LoadScene("TitleScene");
    }


    void OpenResetPopup()
    {
        resetPopup.SetActive(true);
    }

    private void Reset()
    {

    }

    void CancelReset()
    {
        resetPopup.SetActive(false);
    }
}
