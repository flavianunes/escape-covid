using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void CarregarJogo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void SairJogo()
    {
        UnityEngine.Application.Quit();
    }

    public void Help()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ajuda", LoadSceneMode.Single);
    }
}
