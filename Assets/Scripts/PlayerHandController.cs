using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
public class PlayerHandController : MonoBehaviour
{
    [SerializeField] private RuntimeSpellDatabase spellDatabase;

    private List<Handsign> handsigns;

    public Action<List<Handsign>> onHandsignChange;

    private void Start()
    {
        handsigns = new();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            AddSign(Handsign.Tiger);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AddSign(Handsign.Hippo);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddSign(Handsign.Crane);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AddSign(Handsign.Rat);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AddSign(Handsign.Fox);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            handsigns.Clear();
            onHandsignChange?.Invoke(handsigns);
        }

        if (Input.GetMouseButtonDown(0))
        {
            spellDatabase.TryExecuteSpell(handsigns);
            onHandsignChange?.Invoke(handsigns);
        }
    }

    private void AddSign(Handsign handsign)
    {
        handsigns.Add(handsign);
        onHandsignChange?.Invoke(handsigns);
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
