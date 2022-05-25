//Verifica conexão com a rede.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VerificaConexao : MonoBehaviour
{
    //INSPECTOR VARIABLES
    [SerializeField] private Text _textLoadBar;
    [SerializeField] private GameObject _imgAviso;
    [SerializeField] private Text _textAviso;
    [SerializeField] private Image _barraProgresso;


    //PRIVATE VARIABLES
    private bool _conexao;


    void Start()
    {
        if(_imgAviso.activeSelf)
        {
             _imgAviso.SetActive(false);
        }
        VerificaaConexao();
    }
    private void Update()
    {
        ProgressBar();
    }
    private void VerificaaConexao()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogWarning("Ops !, Verifique sua conexão de Internet");
            _imgAviso.SetActive(true);
            _textAviso.text = "Ops !, Verifique sua conexão de Internet";
            _conexao = false;
        }
        else
        {
            Debug.Log("Aguarde Carregando ...");
            _textLoadBar.text = "Aguarde Carregando ...";
            _conexao = false; 
        }
    }
    public void FecharAviso()
    {
        _imgAviso.SetActive(false);
    }  

    private void ProgressBar()
    {
        _barraProgresso.fillAmount = _barraProgresso.fillAmount + 1f / 100;
        if (_conexao = true & _barraProgresso.fillAmount >= 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
