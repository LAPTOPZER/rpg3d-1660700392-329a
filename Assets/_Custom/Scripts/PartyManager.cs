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

    [SerializeField]
    private int partyMoney = 1000;
    public int PartyMoney { get { return partyMoney; } set { partyMoney = value; } }

    [SerializeField]
    private int totalExp;

    [SerializeField]
    private HeroData[] heroData;
    public HeroData[] HeroData { get { return heroData; } }

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //foreach (Characters c in member)
        //{
        //    c.CharInit(VFXManager.instance,
        //         UIManager.instance, InventoryManager.instance,this);
        //}

        SelectSingleHero(0);

        //member[0].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[0]));

        //member[1].MagicSkills.Add(new Magic(VFXManager.instance.MagicData[1]));

        //Male
        //InventoryManager.instance.AddItem(member[0], 0); //Health Potion
        //InventoryManager.instance.AddItem(member[0], 1); //Sword A
        //InventoryManager.instance.AddItem(member[0], 2); //Shield A
        //InventoryManager.instance.AddItem(member[0], 3); //Sword B
        //InventoryManager.instance.AddItem(member[0], 4); //Shield B
        //InventoryManager.instance.AddItem(member[0], 5); //Dagger
        //InventoryManager.instance.AddItem(member[0], 10); //Key
        //InventoryManager.instance.AddItem(member[0], 9); //Turkey

        //Female
        //InventoryManager.instance.AddItem(member[1], 0); //Health Potion
        //InventoryManager.instance.AddItem(member[1], 1); //Sword A
        //InventoryManager.instance.AddItem(member[1], 3); //Sword B
        //InventoryManager.instance.AddItem(member[1], 2); //Shield A
        //InventoryManager.instance.AddItem(member[1], 4); //Shield B
        //InventoryManager.instance.AddItem(member[1], 8); //Steak
        //InventoryManager.instance.AddItem(member[1], 7); //BoostPotion
        //InventoryManager.instance.AddItem(member[1], 6); //SlowPotion

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
        //if (selectChars.Count <= 1)
        //{
        //    UIManager.instance.ToggleAvatar[i].isOn = true;
        //    return;
        //}

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

    public void DistributeTotalExp(int n)
    {
        totalExp = n;
        int eachHeroExp = totalExp / member.Count;

        foreach (Hero hero in member)
            hero.ReceiveExp(eachHeroExp);
    }

    public bool HeroJoinParty(Characters hero)
    {
        if (member.Count >= 6)
            return false;

        hero.CharInit(VFXManager.instance, UIManager.instance,
            InventoryManager.instance, this);

        member.Add(hero);
        return true;
    }

    public void SaveAllHeroData()
    {
        for (int i = 0; i < member.Count; i++)
        {
            Hero hero = (Hero)member[i];
            heroData[i].prefabId = hero.PrefabID;
            heroData[i].curHp = hero.CurHP;

            for (int j = 0; j < hero.MagicSkills.Count; j++)
                heroData[i].magicIds[j] = hero.MagicSkills[j].ID;

            for (int k = 0; k < hero.InventoryItems.Length; k++)
            {
                if (hero.InventoryItems[k] == null)
                    heroData[i].inventoryItemIds[k] = -1;
                else
                    heroData[i].inventoryItemIds[k] = hero.InventoryItems[k].ID;
            }

            heroData[i].attackDamage = hero.AttackDamage;
            heroData[i].defensePower = hero.DefensePower;
            heroData[i].exp = hero.Exp;
            heroData[i].level = hero.Level;
            heroData[i].nextExp = hero.NextExp;
        }
    }

    public void LoadAllHeroData()
    {
        int enterId = Settings.enterPointId;
        Vector3 pos = MapManager.instance.EnterPoints[enterId].position;

        for (int i = 0; i < Settings.partyCount; i++)
        {
            GameObject heroObj =
                Instantiate(GameManager.instance.HeroPrefabs[heroData[i].prefabId],
                pos, Quaternion.identity);

            if (i == 0)
                heroObj.gameObject.tag = "Player";

            Hero hero = heroObj.GetComponent<Hero>();
            hero.CharInit(VFXManager.instance, UIManager.instance,
                InventoryManager.instance, this);
            hero.CurHP = heroData[i].curHp;

            for (int j = 0; j < heroData[i].magicIds.Count; j++)
            {
                int magicId = heroData[i].magicIds[j];
                hero.MagicSkills.Add(new Magic(VFXManager.instance.MagicData[magicId]));
            }

            for (int k = 0; k < heroData[i].inventoryItemIds.Length; k++)
            {
                int itemId = heroData[i].inventoryItemIds[k];
                if (itemId != -1)
                    hero.InventoryItems[k] =
                        new Item(InventoryManager.instance.ItemData[itemId]);
            }

            hero.AttackDamage = heroData[i].attackDamage;
            hero.DefensePower = heroData[i].defensePower;
            hero.Exp = heroData[i].exp;
            hero.Level = heroData[i].level;
            hero.NextExp = heroData[i].nextExp;
            member.Add(hero);
        }
    }
}
