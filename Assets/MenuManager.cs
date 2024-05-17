using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   public GameObject fases; 
   public GameObject milkshake;
   public GameObject kfc;
   public GameObject sushi;
   public GameObject chocolate;
  
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

     public void Milkshake()
    {
            milkshake.SetActive(true);     
    }
    public void Kfc()
    {
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
            milkshake.SetActive(false);
            kfc.SetActive(false);
            sushi.SetActive(false);
            chocolate.SetActive(false);
    }
}