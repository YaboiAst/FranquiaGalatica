using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopColorControl : MonoBehaviour
{
    [SerializeField] private BlockManager.Tipo tipo;
    
    [SerializeField] private Color unlockColor;
    [SerializeField] private Color availableColor;
    [SerializeField] private Color suspendedColor;
    private Image shopSprite;
    
    private void Start()
    {
        shopSprite = GetComponent<Image>();
        shopSprite.color = unlockColor;
        
        GameManager.OnResourceAvailable.AddListener(UpdateColor);
        ResourceManager.OnStoreUpdate.AddListener(UpdateColor);
    }

    private void UpdateColor(ResourceManager.StoreInfo info)
    {
        if (info.isUnlocked)
        {
            shopSprite.color = availableColor;
        }
    }

    private void UpdateColor(BlockManager.Tipo tipoLoja, bool available)
    {
        if(tipo != tipoLoja) return;

        shopSprite.color = available ?  availableColor : suspendedColor;
    }
}
