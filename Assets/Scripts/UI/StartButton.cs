using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button startBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        UIManager.Instance.LoadScene("GameManagerScene");
    }
}
