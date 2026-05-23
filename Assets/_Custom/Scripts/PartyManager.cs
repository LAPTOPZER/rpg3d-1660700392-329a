using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private List<Characters> member = new List<Characters>();
    public List<Characters> Members { get { return member; } }

    [SerializeField]
    private List<Characters> selectChars = new List<Characters>();
    public List<Characters> SelectChars { get { return selectChars; } }

    [SerializeField]
    private List<Quest> questsList = new List<Quest>();
    public List<Quest> QuestList { get { return questsList; } }

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (Characters c in member)
        {
            c.CharInit(VFXManager.instance,
                 UIManager.instance, InventoryManager.instance);
        }

        SelectSingleHero(0);

        member[0].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[0]));

        member[1].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[1]));

        //Male
        InventoryManager.instance.AddItem(member[0], 0); //Health Potion
        InventoryManager.instance.AddItem(member[0], 1); //Sword A
        InventoryManager.instance.AddItem(member[0], 3); //Sword B
        InventoryManager.instance.AddItem(member[0], 2); //Shield A
        InventoryManager.instance.AddItem(member[0], 4); //Shield B
        InventoryManager.instance.AddItem(member[0], 5); //Dagger
        InventoryManager.instance.AddItem(member[0], 10); //Key
        InventoryManager.instance.AddItem(member[0], 9); //Turkey

        //Female
        InventoryManager.instance.AddItem(member[1], 0); //Health Potion
        InventoryManager.instance.AddItem(member[1], 1); //Sword A
        InventoryManager.instance.AddItem(member[1], 3); //Sword B
        InventoryManager.instance.AddItem(member[1], 2); //Shield A
        InventoryManager.instance.AddItem(member[1], 4); //Shield B
        InventoryManager.instance.AddItem(member[1], 8); //Steak
        InventoryManager.instance.AddItem(member[1], 7); //BoostPotion
        InventoryManager.instance.AddItem(member[1], 6); //SlowPotion

        UIManager.instance.ShowMagicToggles();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{

        //    if (selectChars.Count > 0)
        //    {

        //        selectChars[0].IsMagicMode = true;
        //        selectChars[0].CurMagicCast = selectChars[0].MagicSkills[0];
        //    }

        //}
    }

    public void SelectSingleHero(int i)
    {
        foreach (Characters c in selectChars)
            c.ToggleRingSelection(false);

        selectChars.Clear();

        selectChars.Add(member[i]);
        selectChars[0].ToggleRingSelection(true);
    }

    public void HeroSelectMagicSkill(int i)
    {
        if (selectChars.Count <= 0)
            return;

        selectChars[0].IsMagicMode = true;
        selectChars[0].CurMagicCast = selectChars[0].MagicSkills[i];
    }

    public int FindIndexFromClass(Characters hero)
    {
        for (int i = 0; i < member.Count; i++)
        {
            if (member[i] == hero)
                return i;
        }
        return 0;
    }

    public void SelectSingleHeroByToggle(int i)
    {
        //Debug.Log($"Select {i}");

        if (selectChars.Contains(member[i]))
        {
            member[i].ToggleRingSelection(true);
            UIManager.instance.ShowMagicToggles();
        }
        else
        {
            selectChars.Add(member[i]);
            member[i].ToggleRingSelection(true);
            UIManager.instance.ShowMagicToggles();
        }
    }

    public void UnSelectSingleHeroByToggle(int i)
    {
        if (selectChars.Count <= 1)
        {
            UIManager.instance.ToggleAvatar[i].isOn = true;
            return;
        }

        if (selectChars.Contains(member[i]))
        {
            selectChars.Remove(member[i]);
            member[i].ToggleRingSelection(false);
        }
    }

    public void RemoveHeroFromParty(int id)
    {
        if (id == -1 || id == 0)
            return;

        if (selectChars.Contains(member[id]))
            selectChars.Remove(member[id]);

        member.Remove(member[id]);
    }
}
