using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour {

    private int scene = 0;


    public void AvancarCena()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void VoltarCena()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void BtnScenePerfil()
    {
        SceneManager.LoadScene("2_Perfil");
    }
    public void BtnCadastrarCliente()
    {
        SceneManager.LoadScene("3_CadastrarCliente");
    }
    public void BtnCadastrarAvaliacao()
    {
        SceneManager.LoadScene("5_CadastrarAvaliacao");
    }
    public void BtnHistorico()
    {
        SceneManager.LoadScene("4_Bioimpedancia");
    }
}
