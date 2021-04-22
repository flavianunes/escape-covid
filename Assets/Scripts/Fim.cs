using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{
    public void CarregarJogo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void RetornarMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void SairJogo()
    {
        
        UnityEngine.Application.Quit();
    }
}
