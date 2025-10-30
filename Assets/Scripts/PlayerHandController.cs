using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

public class PlayerHandController : MonoBehaviour
{
    [SerializeField] private float signBufferDecayTime;
    [SerializeField] private float kickRadius;
    [SerializeField] private float kickOffset;

    [Header("Reference")]
    [SerializeField] private PlayerBody playerBody;
    [SerializeField] private RuntimeSpellDatabase spellDatabase;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem signParticle;
    [SerializeField] private Transform partParent;
    [SerializeField] private SerializedDictionary<Handsign, GameObject> handsignSprites;
    [SerializeField] private SerializedDictionary<Handsign, string> handsignSoundKey;

    private List<Handsign> handsigns;

    public Action<List<Handsign>> onHandsignChange;

    [NonSerialized] private bool _allowSignChange = true;
    private Handsign upcomingSign;

    private float _bufferStartTime;
    private Coroutine _bufferRoutine;
    private Action _bufferedAction;

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
            TryPerformSign(Handsign.Fox);
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            TryPerformSign(Handsign.Crane);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            handsigns.Clear();
            onHandsignChange?.Invoke(handsigns);
        }

        if (Input.GetMouseButtonDown(0) && handsigns.Count > 0)
        {
            if (_allowSignChange)
                Fire();
            else
                BufferAction(Fire);
        }

        if (Input.GetMouseButtonDown(1) )
        {
            if (_allowSignChange)
                Kick();
            else
                BufferAction(Kick);
        }
    }

    private void Kick()
    {
        animator.SetTrigger("kick");
        var _overlap = Physics.OverlapSphere(playerBody.transform.position + (playerBody.transform.forward * kickOffset), kickRadius);
        foreach(var _go in _overlap)
        {
            if(_go.TryGetComponent(out Enemy enemy))
            {
                enemy.KnockBack(playerBody.transform.forward,50);
            }
        }
        onHandsignChange?.Invoke(handsigns);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerBody.transform.position + (playerBody.transform.forward * kickOffset),kickRadius);
    }

    private void Fire()
    {
        animator.SetTrigger("Fire");
        spellDatabase.ExecuteSpell(handsigns);
        handsigns.Clear();
        onHandsignChange?.Invoke(handsigns);
    }

    public void PlaySignParticle()
    {
        //signParticle.Stop();
        Instantiate(signParticle, partParent,false);
    }

    private void TryPerformSign(Handsign handsign)
    {
        if (!_allowSignChange)
        {
            BufferAction(() => TryPerformSign(handsign));
            return;
        }
        AudioManager.Instance.PlayGlobalSound(handsignSoundKey[handsign],1.0f);
        animator.SetTrigger("Change");
        upcomingSign = handsign;
        _allowSignChange = false;
    }

    private void BufferAction(Action action)
    {
        _bufferStartTime = Time.time;
        _bufferedAction = action;
        if(_bufferRoutine == null)
            _bufferRoutine = StartCoroutine(ActionBufferIE());
    }

    IEnumerator ActionBufferIE()
    {
        yield return new WaitUntil(() => _allowSignChange);

        if (Time.time - _bufferStartTime < signBufferDecayTime)
            _bufferedAction?.Invoke();

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
    Fist = 2,
    Point = 3,
    Crane = 4,
    Fox = 5
}
