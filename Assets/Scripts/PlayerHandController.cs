using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public class PlayerHandController : MonoBehaviour
{
    [SerializeField] private float signBufferDecayTime;
    
    [Header("Reference")]
    [SerializeField] private RuntimeSpellDatabase spellDatabase;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem signParticle;
    [SerializeField] private SerializedDictionary<Handsign, GameObject> handsignSprites;

    private List<Handsign> handsigns;

    public Action<List<Handsign>> onHandsignChange;

    [NonSerialized] private bool _allowSignChange = true;
    private Handsign upcomingSign;

    private float _bufferStartTime;
    private Coroutine _bufferRoutine;
    private Handsign _bufferedHandsign;

    private void Start()
    {
        handsigns = new();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            TryPerformSign(Handsign.Tiger);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            TryPerformSign(Handsign.Hippo);
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            TryPerformSign(Handsign.Crane);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            TryPerformSign(Handsign.Rat);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TryPerformSign(Handsign.Fox);   
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

    public void PlaySignParticle()
    {
        signParticle.Stop();
        signParticle.Play();
    }

    private void TryPerformSign(Handsign handsign)
    {
        if (!_allowSignChange)
        {
            BufferSign(handsign);
            return;
        }

        animator.SetTrigger("Change");
        upcomingSign = handsign;
        _allowSignChange = false;
    }

    private void BufferSign(Handsign handsign)
    {
        _bufferStartTime = Time.time;
        _bufferedHandsign = handsign;
        if(_bufferRoutine == null)
            _bufferRoutine = StartCoroutine(PerformSignBufferIE());
    }

    IEnumerator PerformSignBufferIE()
    {
        yield return new WaitUntil(() => _allowSignChange);

        if (Time.time - _bufferStartTime < signBufferDecayTime)
            TryPerformSign(_bufferedHandsign);

        _bufferRoutine = null;
    }

    public void SetSign(Handsign handsign)
    {
        foreach (var _sprite in handsignSprites.Values)
            _sprite.SetActive(false);
        handsignSprites[handsign].SetActive(true);
    }

    public void SetAllowNextSign()
    {
        _allowSignChange = true;
    }

    public void TriggerSignChange()
    {
        handsigns.Add(upcomingSign);
        onHandsignChange?.Invoke(handsigns);
        SetSign(upcomingSign);
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
