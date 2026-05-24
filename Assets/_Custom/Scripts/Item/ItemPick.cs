using UnityEngine;
using UnityEngine.TextCore.Text;

public class ItemPick : MonoBehaviour
{
    [SerializeField]
    private Item item;
    public Item Item { get { return item; } }

    private InventoryManager inventoryManager;
    private PartyManager partyManager;

    public void Init(Item item, InventoryManager invManager, PartyManager ptyManager)
    {
        this.item = item;
        inventoryManager = invManager;
        partyManager = ptyManager;
    }

    public void PickUpItem()
    {
        if (partyManager.SelectChars.Count == 0)
            return;

        if (inventoryManager.AddItem(partyManager.SelectChars[0], item.ID))
            Destroy(gameObject);
    }
}
