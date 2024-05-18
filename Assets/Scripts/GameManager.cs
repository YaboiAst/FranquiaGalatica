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

        public bool Pay(BlockManager.Tipo tipo, int price)
        {
            switch (tipo)
            {
                case BlockManager.Tipo.Vaca:
                    if(qtdVacas < price) return false;
                    qtdVacas -= price;
                    break;
                case BlockManager.Tipo.Galinha:
                    if(qtdGalinhas < price) return false;
                    qtdGalinhas -= price;
                    break;
                case BlockManager.Tipo.Peixe:
                    if(qtdPeixes < price) return false;
                    qtdPeixes -= price;
                    break;
                case BlockManager.Tipo.Coelho:
                    if(qtdCoelhos < price) return false;
                    qtdCoelhos -= price;
                    break;
                case BlockManager.Tipo.Abelha:
                    if(qtdAbelhas < price) return false;
                    qtdAbelhas -= price;
                    break;
                case BlockManager.Tipo.Rato:
                    if(qtdRatos < price) return false;
                    qtdRatos -= price;
                    break;
            }

            return true;
        }
    }
    
    public static GameManager Instance;

    public Level.LevelInfo currentLevel;
    public TabelaValores totalRecursos;
    public int totalCurrency;

    public static readonly UnityEvent<TabelaValores, int> OnResourceUpdate = new();
    public static readonly UnityEvent<BlockManager.Tipo, bool> OnResourceAvailable = new();
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

    private void Start()
    {
        ResourceManager.OnStoreUpdate.AddListener(RunStore);
    }

    private void RunStore(ResourceManager.StoreInfo info)
    {
        StopCoroutine(RunStoreCoroutine(info));
        StartCoroutine(RunStoreCoroutine(info));
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

    private IEnumerator RunStoreCoroutine(ResourceManager.StoreInfo storeInfo)
    {
        var minute = new WaitForSeconds(1f);
        
        while (true)
        {
            yield return minute;
            if (totalRecursos.Pay(storeInfo.recursoUsado, storeInfo.consumptionPerMin))
            {
                totalCurrency += storeInfo.cashPerMin;
                OnResourceUpdate?.Invoke(totalRecursos, totalCurrency);
                OnResourceAvailable?.Invoke(storeInfo.recursoUsado, true);
            }
            else OnResourceAvailable?.Invoke(storeInfo.recursoUsado, false);
        }
    }
}