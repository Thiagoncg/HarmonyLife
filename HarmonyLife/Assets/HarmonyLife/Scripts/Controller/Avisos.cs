using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avisos : MonoBehaviour {

    public GameObject ImgAviso;
    public Text TextAviso;
    // Use this for initialization


    public void ClienteJaCadastrado()
    {
        ImgAviso.SetActive(true);
        ImgAviso.GetComponent<Image>().color = new Color32(0, 185, 156, 100);
        TextAviso.text = "Cliente já cadastrado com outra pessoa!";
    }

    public void ClienteCadastradoComSucesso()
    {
        TextAviso.text = "Cliente cadastrado com sucesso!";
    }

    public void ErroSemInternet()
    {
        TextAviso.text = "ERRO! Sem conexão com a internet";
    }

    public void DadosCadastrados()
    {
        TextAviso.text = "Dados cadastrados com sucesso!";
    }

    private void Carregando()
    {
        TextAviso.text = "Aguarde carrendo aplicação";
    }


    public void BotaoFechar()
    {

        ImgAviso.SetActive(false);

    }

}
