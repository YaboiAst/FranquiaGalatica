using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   [BoxGroup("Seleção de nível")] public GameObject fases;
   [BoxGroup("Seleção de nível")] public GameObject viajar; 
   
   [BoxGroup("Seleção de nível")] public Button botaoVaca;
   [BoxGroup("Seleção de nível")] public Button botaoGalinha;
   [BoxGroup("Seleção de nível")] public Button botaoPeixe;
   [BoxGroup("Seleção de nível")] public Button botaoCoelho;
   [BoxGroup("Seleção de nível")] public Button botaoAbelha;
   [BoxGroup("Seleção de nível")] public Button botaoRato;
   
   // ^^^ BOTOES DE SELEÇÃO DE FASE ^^^
   [Space(15)]
   // VVV BOTOES DE ADMNISTRAÇÃO VVV
   
   [BoxGroup("Lojas -- Milkshake")] public Button milkshakeButton;
   [BoxGroup("Lojas -- Milkshake")] public GameObject milkshake;
   [BoxGroup("Lojas -- Milkshake")] public GameObject blockMilkshake;

   [BoxGroup("Lojas -- KFC")] public Button kfcButton;
   [BoxGroup("Lojas -- KFC")] public GameObject kfc;
   [BoxGroup("Lojas -- KFC")] public GameObject blockKfc;

   [BoxGroup("Lojas -- Sushi")] public Button sushiButton;
   [BoxGroup("Lojas -- Sushi")] public GameObject sushi;
   [BoxGroup("Lojas -- Sushi")] public GameObject blockSushi;

   [BoxGroup("Lojas -- Chocolate")] public GameObject chocolate;

    public void Start()
    {
        // Inicializar botões da seleção de fase
        botaoVaca.onClick.AddListener(Viagem);
        botaoGalinha.onClick.AddListener(Viagem);
        botaoPeixe.onClick.AddListener(Viagem);
        botaoCoelho.onClick.AddListener(Viagem);
        botaoAbelha.onClick.AddListener(Viagem);
        botaoRato.onClick.AddListener(Viagem);
        
        // Inicializar botões de admnistração
        milkshakeButton.onClick.AddListener(() => ActivateOverlay(blockMilkshake, true));
        kfcButton.onClick.AddListener(() => ActivateOverlay(blockKfc, true));
        sushiButton.onClick.AddListener(() => ActivateOverlay(blockSushi, true));
        // Outras lojas
    }
    public void Play() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void Exit() => Application.Quit();
    
    public void ActivateOverlay(GameObject go, bool state) => go.SetActive(state);

    #region SelecaoFases
    public void Terra()
    {
        fases.SetActive(true);
        viajar.SetActive(false);
    }
    public void Viagem()
    {
        viajar.SetActive(true);
    }
    public void Viajar()
    { 
        if(!GameManager.Instance.PayTravel()) return;
        SceneManager.LoadScene("TesteMovimentoMG");
    }
    #endregion

    #region UnlockFunctions
    public void UnlockStore(Button unlockButton, GameObject unlockOverlay, GameObject storeOverlay)
    {
        unlockButton.onClick.RemoveAllListeners();
        unlockButton.onClick.AddListener(() => storeOverlay.SetActive(true));
        
        unlockOverlay.SetActive(false);
        storeOverlay.SetActive(true);   
    }
    public void LiberaMilkshake() => UnlockStore(milkshakeButton, blockMilkshake, milkshake);
    public void LiberaKfc()       => UnlockStore(kfcButton, blockKfc, kfc);
    public void LiberaSushi()     => UnlockStore(sushiButton, blockSushi, sushi);
    // Desbloqueio de outras lojas
    #endregion
    
    public void Fechar()
    {       
        fases.SetActive(false);

        blockMilkshake.SetActive(false);
        milkshake.SetActive(false); 

        kfc.SetActive(false);
        blockKfc.SetActive(false);
            
        sushi.SetActive(false);
        blockSushi.SetActive(false);
        
        chocolate.SetActive(false);
    }
}