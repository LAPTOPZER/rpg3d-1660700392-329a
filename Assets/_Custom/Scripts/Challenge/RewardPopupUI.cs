using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardPopupUI : MonoBehaviour
{
    public static RewardPopupUI instance;

    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private TMP_Text rewardNameText;
    [SerializeField] private TMP_Text rewardExpText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        instance = this;
        popupPanel.SetActive(false);
        closeButton.onClick.AddListener(ClosePopup);
    }

    public void ShowReward(ItemData rewardItem, int exp)
    {
        popupPanel.SetActive(true);
        Time.timeScale = 0f;

        // ดึงข้อมูลไอเทมรางวัลมาแสดงผล
        if (rewardItem != null)
        {
            rewardIcon.sprite = rewardItem.icon; // สมมติว่าใน ItemData มีตัวแปรเก็บภาพ icon
            rewardNameText.text = $"Got Item: {rewardItem.itemName}";
        }
        else
        {
            rewardIcon.gameObject.SetActive(false);
            rewardNameText.text = "";
        }

        // แสดงผล Exp ที่ได้รับ
        rewardExpText.text = $"+ {exp} EXP";
    }

    private void ClosePopup()
    {
        popupPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}