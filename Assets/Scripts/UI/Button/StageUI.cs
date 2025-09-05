using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private TMP_Text stageNameText;
    [SerializeField] private TMP_Text costText;

    [SerializeField] private Button prevStageBtn;
    [SerializeField] private Button nextStageBtn;
    [SerializeField] private Button closePopupBtn;
    [SerializeField] private Button costBtn;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject nextStagePopup;


    private void Awake()
    {
        //버튼 연결
        nextStageBtn.onClick.AddListener(OnClickNextStage);
        prevStageBtn.onClick.AddListener(OnClickPrevStage);
        costBtn.onClick.AddListener(ClickAndGoNext);
        closePopupBtn.onClick.AddListener(PopupClose);

        UpdatePrevButton();
        UpdateStageName();
    }

    private void OnEnable()
    {
        var gold = GameManager.Instance.playerGold;
        gold.OnGoldChanged += HandleGloldChanged;
    }
    private void OnDisable()
    {
        var gold = GameManager.Instance.playerGold;
        gold.OnGoldChanged -= HandleGloldChanged;
    }

    private void HandleGloldChanged(double gold)
    {
        if (nextStagePopup.activeSelf)
        {
            RefreshPopupUI();
        }
    }

    void UpdateStageName()
    {
        stageNameText.text = $"Stage {stageManager.currentStageIndex}";
    }

    void UpdatePrevButton()
    {
        prevStageBtn.gameObject.SetActive(stageManager.currentStageIndex > 1);
    }


    void RefreshPopupUI()
    {
        var data = stageManager.stages[stageManager.currentStageIndex];
        var cond = data.openCondition;


        //비용,골드 stagedata를 30개 만들어놓았든데 그 중 OpenCondition 안에 goldCost에 값을 저장해두었어 그걸 가져올거야.
        double cost = cond.goldCost;
        double have = GameManager.Instance.playerGold.Gold;
        costText.text = cost.ToString("#,0");
        costText.color = have < cost ? Color.red : Color.black;

        //무기는 현재 장착한 무기와 OpenCondition 안에requiredWeaponIndex값이 같은지 확인할거야.
        if(cond.requiredWeaponIndex == GameManager.Instance.Player.equippedWeaponLevel) 
        {
            check.SetActive(true);
        }
        
        
    }

    

    //nextStageBtn을 눌렀을 때 다음 스테이지가 열려있다면 stagemanager gotostage사용. 안열려있다면 nextstagepopup 활성화(setactive true) 라는 함수 => nextstagebtn에 addlistner할 예정
    private void OnClickNextStage()
    {
        int next = Mathf.Clamp(stageManager.currentStageIndex + 1, 1, stageManager.stages.Count );
        bool unlocked = IsUnlocked(next);
        if (unlocked)
        {
            StageManager.Instance.OpenOrGo(next);
            UpdateStageName();
        }
        else
        {
            nextStagePopup.SetActive(true);
        }

    }

    bool IsUnlocked(int StageNumber)
    {
        var list = GameManager.Instance.Player.unlockStages;
        return list.Contains(StageNumber);
    }

    //prevstageBtn을 눌렀을 때 이전 스테이지로 이동 stagemanager gotostage사용 currentstage -1로 가야할거 같음

    private void OnClickPrevStage()
    {
        int prev = Mathf.Clamp(stageManager.currentStageIndex -1, 1, stageManager.stages.Count);

        stageManager.OpenOrGo(prev);
        UpdateStageName();
        UpdatePrevButton();
    }

    void PopupClose()
    {
        nextStagePopup.SetActive(false);
    }

    //stagepopup이 활성화된 상태에서 requireweapon이 충족됐다면 check 활성화, 충족되지 않으면 check 비활성화
    private void ClickAndGoNext()
    {
        stageManager.OpenOrGo(stageManager.currentStageIndex + 1);
        check.SetActive(false);
        PopupClose();
        UpdatePrevButton();
        UpdateStageName();
    }

    //stagepopup이 활성화된 상태에서 
    //1. (requireweapon이 충족 && player.gold > costgold)면 버튼 활성화
    //2. player.gold < costgold면 costText 글씨 색상 빨간색
    //3. player.gold > costgold 이지만 requireweapon이 충족되지 않는다면 버튼 눌러도 반응없음
    //1번 상태에서 버튼을 누르면 gotostage로 현재스테이지 + 1 스테이지 오픈 및 이동
}
