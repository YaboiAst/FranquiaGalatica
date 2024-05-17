using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class TabelaValores
    {
        public int qtdVacas = 0;
        public int qtdGalinhas = 0;
        public int qtdPeixes = 0;
        public int qtdCoelhos = 0;
        public int qtdAbelhas = 0;
        public int qtdRatos = 0;
        
        public void Copy(TabelaValores copyFrom)
        {
            qtdVacas    = copyFrom.qtdVacas;
            qtdGalinhas = copyFrom.qtdGalinhas;
            qtdPeixes   = copyFrom.qtdPeixes;
            qtdCoelhos  = copyFrom.qtdCoelhos;
            qtdAbelhas  = copyFrom.qtdAbelhas;
            qtdRatos    = copyFrom.qtdRatos;
        }
    }
    
    public static GameManager Instance;

    public Level.LevelInfo currentLevel;
    public TabelaValores totalRecursos;
    public int totalCurrency;

    public static readonly UnityEvent OnLose = new(), OnWin = new();
    
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void LoadCurrentLevel(Level.LevelInfo level)
    {
        currentLevel = new Level.LevelInfo
        {
            levelDrops = level.levelDrops,
            objetivos = level.objetivos,
            travelCost = level.travelCost
        };
    }

    public bool PayTravel()
    {
        var travelCost = currentLevel.travelCost;
        if (totalCurrency < travelCost) return false;

        totalCurrency -= travelCost;
        return true;
    }
}