using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharAnimation : MonoBehaviour
{
    private Characters character;

    void Start()
    {
        character = GetComponent<Characters>();
    }

    void Update()
    {
        ChooseAnimation(character);
    }

    private void ChooseAnimation(Characters c)
    {
        c.Anim.SetBool("IsIdle", false);
        c.Anim.SetBool("IsWalk", false);

        switch (c.State)
        {
            case CharState.Idle:
                c.Anim.SetBool("IsIdle", true);
                break;
            case CharState.Walk:
            case CharState.WalkToEnemy:
            case CharState.WalkToMagicCast:
            case CharState.WalkToNPC:
                c.Anim.SetBool("IsWalk", true);
                break;
        }
    }
}
