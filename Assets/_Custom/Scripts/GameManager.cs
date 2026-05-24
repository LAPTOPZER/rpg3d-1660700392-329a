using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] heroPrefabs;
    public GameObject[] HeroPrefabs { get { return heroPrefabs; } }

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (Settings.isNewGame)
        {
            Settings.isNewGame = false;
            GeneratePlayerHero();
            AudioManager.instance.PlayBGM(1);
        }

        if (Settings.isWarping)
        {
            Settings.isWarping = false;
            WarpPlayers();
        }
    }

    private void GeneratePlayerHero()
    {
        int i = Settings.playerPrefabId;

        GameObject heroObj = Instantiate(heroPrefabs[i],
            new Vector3(46f, 10f, 38f), Quaternion.identity); //จุด Spawn

        heroObj.tag = "Player";

        Characters hero = heroObj.GetComponent<Characters>();
        PartyManager.instance.Members.Add(hero);

        hero.CharInit(UIManager.instance, InventoryManager.instance,
            PartyManager.instance);

        InventoryManager.instance.AddItem(hero, 0); //health potion
        InventoryManager.instance.AddItem(hero, 2); //Shield A
    }

    private void WarpPlayers()
    {
        PartyManager.instance.LoadAllHeroData();
    }
}
