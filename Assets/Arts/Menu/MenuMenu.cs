using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Play()
    {
        Destroy(GameManager.Instance.gameObject, .1f);
        GameManager.Instance = null;
        
        Destroy(ResourceManager.Instance.gameObject, .1f);
        ResourceManager.Instance = null;

        SceneManager.LoadScene("Tutorial");
    }

     public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
     public void Creditos()
    {
        SceneManager.LoadScene("Créditos");
    }
    
}
