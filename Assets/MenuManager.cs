using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public GameObject fases; 
   public GameObject milkshake;
   public GameObject blockmilkshake;
   public Button milk;
   public GameObject kfc;
   public GameObject blockkfc;
   public Button kfcbutton;
   public GameObject sushi;
   public GameObject chocolate;

    public void Start()
    {
        milk.onClick.AddListener(Milkshake);
        kfcbutton.onClick.AddListener(Kfc);   
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Terra()
    {
       fases.SetActive(true);
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
        chocolate.SetActive(false);
    }
}