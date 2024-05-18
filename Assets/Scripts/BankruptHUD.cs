using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankruptHUD : MonoBehaviour
{
    [SerializeField] private GameObject bankrtupOverlay;

    private void Start()
    {
        bankrtupOverlay.SetActive(false);
        ResourceManager.OnBankrupt.AddListener(() => bankrtupOverlay.SetActive(true));
    }
}
