using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject winOverlay;
    [SerializeField] private GameObject loseOverlay;

    private void Start()
    {
        winOverlay.SetActive(false);
        loseOverlay.SetActive(false);
        
        GameManager.OnWin.AddListener(() => winOverlay.SetActive(true));
        GameManager.OnLose.AddListener(() => loseOverlay.SetActive(true));
    }

    public void BackToHub() => SceneManager.LoadScene("UI");
}
