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

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (Characters c in member)
        {
            c.CharInit(VFXManager.instance, UIManager.instance);
        }

        SelectSingleHero(0);

        member[0].MagicSkills.Add(new Magic(0, "Power Glow", 10f, 20, 3f, 1f, 2, 2));
        member[0].MagicSkills.Add(new Magic(1, "Power Glow2", 10f, 20, 3f, 1f, 2, 2));
        member[0].MagicSkills.Add(new Magic(2, "Power Glow3", 10f, 20, 3f, 1f, 2, 2));

        member[1].MagicSkills.Add(new Magic(0, "Fire Ball", 10f, 35, 3f, 4f, 0, 1));
        member[1].MagicSkills.Add(new Magic(1, "Fire Ball2", 10f, 35, 3f, 4f, 0, 1));
        member[1].MagicSkills.Add(new Magic(2, "Fire Ball3", 10f, 35, 3f, 4f, 0, 1));

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
}
