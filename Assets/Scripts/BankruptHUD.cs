using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BankruptHUD : MonoBehaviour
{
    [SerializeField] private GameObject bankrtupOverlay;
    [SerializeField] private GameObject winOverlay;

    private void Start()
    {
        bankrtupOverlay.SetActive(false);
        winOverlay.SetActive(false);
        
        ResourceManager.OnBankrupt.AddListener(() => bankrtupOverlay.SetActive(true));
        ResourceManager.OnEndGame.AddListener(() => winOverlay.SetActive(true));
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
