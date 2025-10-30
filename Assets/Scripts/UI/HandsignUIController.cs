using UnityEngine;
using System.Collections.Generic;

public class HandsignUIController : MonoBehaviour
{
    [SerializeField] private PlayerHandController playerHandController;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private List<HandsignUI> handsignUIs; 

    private void Start()
    {
        playerHandController.onHandsignChange += OnHandsignChange;
    }

    private void OnHandsignChange(List<Handsign> handsigns)
    {
        for(int i = 0; i < handsignUIs.Count; i++)
        {
            if (i >= handsigns.Count)
            {
                handsignUIs[i].gameObject.SetActive(false);
                continue;
            }


            handsignUIs[i].gameObject.SetActive(true);
            handsignUIs[i].SetSprite(handsigns[i]);
        }

        AudioManager.Instance.PlayOneShot("handsign_" + handsigns.Count.ToString(), 0.5f); // handsigns_ is 1 - 4, see audio dict.
    }
}
