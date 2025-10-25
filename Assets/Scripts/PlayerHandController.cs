using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class PlayerHandController : MonoBehaviour
{
    [SerializeField] private RuntimeSpellDatabase spellDatabase;

    private List<Handsign> handsign;

    private void Start()
    {
        handsign = new();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            handsign.Add(Handsign.Tiger);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            handsign.Add(Handsign.Hippo);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            handsign.Add(Handsign.Crane);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            handsign.Clear();
        }


        if (Input.GetMouseButtonDown(0))
            spellDatabase.TryExecuteSpell(handsign);


    }
}

public enum Handsign
{ 
    Empty = 0,
    Tiger = 1,
    Hippo = 2,
    Crane = 3,
    Rat = 4,
    Fox = 5
}
