using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public GameObject fases;
   public Button botaoVaca;
   public GameObject viajar; 
   public GameObject milkshake;
   public GameObject blockmilkshake;
   public Button milk;
   public GameObject kfc;
   public GameObject blockkfc;
   public Button kfcbutton;
   public GameObject sushi;
   public GameObject blocksushi;
   public Button sushib;
   public GameObject chocolate;

    public void Start()
    {
        milk.onClick.AddListener(Milkshake);
        kfcbutton.onClick.AddListener(Kfc);
        botaoVaca.onClick.AddListener(ViagemVaca);
        sushib.onClick.AddListener(Sushi);   
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    //CONTROLE DE VIAGEM PRA TERRA
    public void Terra()
    {
       fases.SetActive(true);
       viajar.SetActive(false);
    }
    public void ViagemVaca()
    {
       viajar.SetActive(true);
    }
     public void Viajar()
    {
        SceneManager.LoadScene("TesteMovimentoMG");
    }
    
    //CONTROLE DE BOTOES MILKSHAKE
     public void Milkshake()
    {
        blockmilkshake.SetActive(true);     
    }
     public void LiberaMilkshake()
    {       
        milk.onClick.RemoveListener(Milkshake);
        milk.onClick.AddListener(()=>milkshake.SetActive(true));
        blockmilkshake.SetActive(false);
        milkshake.SetActive(true);       
    }

    //CONTROLE DE BOTOES KFC
    public void Kfc()
    {
        blockkfc.SetActive(true);     
    }
    public void LiberaKFC()
    {       
        kfcbutton.onClick.RemoveListener(Kfc);
        kfcbutton.onClick.AddListener(()=>kfc.SetActive(true));
        blockkfc.SetActive(false);
        kfc.SetActive(true);       
    }
    public void Sushi()
    {
        blocksushi.SetActive(true); 
    }
    public void LiberaSushi()
    {
        sushib.onClick.RemoveListener(Sushi);
        sushib.onClick.AddListener(()=>sushi.SetActive(true));
        blocksushi.SetActive(false);
        sushi.SetActive(true); 
    }
     public void Chocolate()
    {
        chocolate.SetActive(true);     
    }
    public void Fechar()
    {       
        fases.SetActive(false);

        blockmilkshake.SetActive(false);
        milkshake.SetActive(false); 

        kfc.SetActive(false);
        blockkfc.SetActive(false);
            
        sushi.SetActive(false);
        blocksushi.SetActive(false);
        
        chocolate.SetActive(false);
    }
}