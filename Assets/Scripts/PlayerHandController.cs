using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;

public class PlayerHandController : MonoBehaviour
{
    [SerializeField] private RuntimeSpellDatabase spellDatabase;

    private List<Handsign> handsigns;

    public Action<List<Handsign>> onHandsignChange;

    [Header("UI Elements")]
    public Image HandVisual;
    public Image Tiger;
    public Image Hippo;
    public Image Crane;
    public Image Rat;
    public Image Fox;

    private void Start()
    {
        handsigns = new();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            AddSign(Handsign.Tiger);
            HandVisual.sprite = Tiger.sprite;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AddSign(Handsign.Hippo);
            HandVisual.sprite = Hippo.sprite;
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddSign(Handsign.Crane);
            HandVisual.sprite = Crane.sprite;
            
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            AddSign(Handsign.Rat);
            HandVisual.sprite = Rat.sprite;

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AddSign(Handsign.Fox);
            HandVisual.sprite = Fox.sprite;
            
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
