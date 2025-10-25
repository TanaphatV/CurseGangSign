using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
public class HandsignUI : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<Handsign, GameObject> handsignSprites;
    
    public void SetSprite(Handsign handsign)
    {
        foreach (var _sprite in handsignSprites.Values)
            _sprite.SetActive(false);
        handsignSprites[handsign].SetActive(true);
    }
}
