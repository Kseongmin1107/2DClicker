using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public Button optionBtn;
    public Button closeOptionBtn;
    public GameObject option;

    private void Awake()
    {
        optionBtn.onClick.AddListener(OpenOption);
        closeOptionBtn.onClick.AddListener(CloseOption);
    }

    private void Update()
    {
        if (option.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseOption();
        }
    }

    void OpenOption()
    {
        option.SetActive(true);
    }
    
    void CloseOption()
    {
        option.SetActive(false);
    }
}
