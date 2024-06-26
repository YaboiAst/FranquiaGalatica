using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    
    [System.Serializable]
    public class StoreInfo
    {
        public BlockManager.Tipo recursoUsado;
        public int consumptionPerMin;
        public int cashPerMin;
        
        [Space(5)]
        
        public int unlockCostCurrency;
        public int unlockCostResource;
        public bool isUnlocked;
        
        [Space(5)]
        
        public int upgradeCostCurrency;
        public int upgradeCostResource;
        public bool isUpgraded;
        public float upgradeModifier;

        public void Upgrade() => cashPerMin = (int) (cashPerMin * upgradeModifier);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private List<StoreInfo> _stores;
    private GameManager _gm;
    
    public static readonly UnityEvent<StoreInfo> OnStoreUpdate = new();
    public static readonly UnityEvent OnBankrupt = new(), OnEndGame = new();

    private void Start()
    {
        Instance = this;
        _gm = GameManager.Instance;

        if(!CheckFinances())
        {
            var seq = DOTween.Sequence();
            seq.AppendInterval(0.5f);
            seq.AppendCallback(() => OnBankrupt?.Invoke());
            return;
        }
        
        MenuManager.OnTryUnlock.AddListener(UnlockStore);
        MenuManager.OnTryUpgrade.AddListener(UpgradeStore);
    }

    private bool CheckFinances()
    {
        if ((_gm.totalCurrency < 2) 
        && (_gm.totalRecursos.qtdVacas    <= 0) 
        && (_gm.totalRecursos.qtdGalinhas <= 0) 
        && (_gm.totalRecursos.qtdPeixes   <= 0) 
        && (_gm.totalRecursos.qtdCoelhos  <= 0) 
        && (_gm.totalRecursos.qtdAbelhas  <= 0) 
        && (_gm.totalRecursos.qtdRatos    <= 0)) return false;
         
        return true;
    }
    
    public void UnlockStore(BlockManager.Tipo tipo)
    {
        var storeToUnlock = _stores.Find(loja => loja.recursoUsado == tipo);
        
        if(_gm.totalCurrency < storeToUnlock.unlockCostCurrency) return;
        _gm.totalCurrency -= storeToUnlock.unlockCostCurrency;
        
        if (!_gm.totalRecursos.Pay(tipo, storeToUnlock.unlockCostResource)) return;
        
        GameManager.OnResourceUpdate?.Invoke(_gm.totalRecursos, _gm.totalCurrency);

        if(storeToUnlock.isUnlocked) return;
        storeToUnlock.isUnlocked = true;
        
        OnStoreUpdate?.Invoke(storeToUnlock);
        CheckWin();
    }

    private void CheckWin()
    {
        foreach (var store in _stores)
        {
            if(!store.isUnlocked) return;
        }
        
        OnEndGame?.Invoke();
    }

    public void UpgradeStore(BlockManager.Tipo tipo)
    {
        var storeToUpgrade = _stores.Find(loja => loja.recursoUsado == tipo);
        
        if(_gm.totalCurrency < storeToUpgrade.upgradeCostCurrency) return;
        _gm.totalCurrency -= storeToUpgrade.upgradeCostCurrency;

        if (!_gm.totalRecursos.Pay(tipo, storeToUpgrade.upgradeCostResource)) return;
        
        GameManager.OnResourceUpdate?.Invoke(_gm.totalRecursos, _gm.totalCurrency);

        if(storeToUpgrade.isUpgraded) return;
        storeToUpgrade.isUpgraded = true;

        storeToUpgrade.Upgrade();
        
        OnStoreUpdate?.Invoke(storeToUpgrade);
    }

    public bool IsUnlocked(BlockManager.Tipo tipo)
    {
        var store = _stores.Find(loja => loja.recursoUsado == tipo);
        return store.isUnlocked;
    }
    
    public bool IsUpgraded(BlockManager.Tipo tipo)
    {
        var store = _stores.Find(loja => loja.recursoUsado == tipo);
        return store.isUpgraded;
    }
}
