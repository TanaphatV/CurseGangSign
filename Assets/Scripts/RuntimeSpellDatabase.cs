using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class RuntimeSpellDatabase : MonoBehaviour
{
    [System.Serializable]
    public class Spell
    {
        public List<Handsign> handsigns;
        public UnityEvent effect;
    }

    public SpellFunctionHub functionHub;
    public List<Spell> spellList;
    private Dictionary<int, Spell> spellDict;

    public void Awake()
    {
        spellDict = new();
        foreach (var spell in spellList)
        {
            spellDict.Add(HandsignToID(spell.handsigns), spell);
        }
    }

    public void ExecuteSpell(int id)
    {
        if (spellDict.ContainsKey(id))
            spellDict[id].effect.Invoke();
        else
            functionHub.Fart();
    }

    public void ExecuteSpell(List<Handsign> handsigns)
    {
        ExecuteSpell(HandsignToID(handsigns));
    }

    private int HandsignToID(List<Handsign> handsigns)
    {
        int spellId = 0;
        for (int i = 0; i < handsigns.Count; i++)
        {
            spellId += ((int)handsigns[i] << i);
        }
        return spellId;
    }
}
