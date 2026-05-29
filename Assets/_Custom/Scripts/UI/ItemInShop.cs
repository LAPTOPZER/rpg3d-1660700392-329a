using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInShop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int id;
    public int ID { get { return id; } set { id = value; } }

    [SerializeField]
    private Item item;
    public Item Item { get { return item; } set { item = value; } }

    [SerializeField]
    private Toggle iconToggle;
    public Toggle IconToggle { get { return iconToggle; } }

    [SerializeField]
    private TMP_Text itemText;

    [SerializeField]
    private TMP_Text priceText;

    [SerializeField]
    private UIManager uiMgr;

    public void SetupItemInShop(UIManager uiManager, float discount)
    {
        uiMgr = uiManager;
        iconToggle.targetGraphic.GetComponent<Image>().sprite = item.Icon;
        itemText.text = item.ItemName;
        priceText.text = ((int)(item.NormalPrice * discount)).ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (iconToggle == null || !iconToggle.interactable)
            return;

        iconToggle.isOn = !iconToggle.isOn;
    }
}
