using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;
using System;

public class CadastrarAvaliacao : MonoBehaviour
{
    [SerializeField]
    private string dbUrl = "https://harmonylife-d645c.firebaseio.com/";

    [SerializeField]
    ScenesController scenesController;

    [SerializeField]
    private InputField  _inputAbdomem,
                        _InputbicepsDireito,
                        _InputBicepsEsquerdo,
                        _inputBusto,
                        _inputCoxaDireita,
                        _InputCoxaEsquerda,
                        _InputGorduraCorporal,
                        _inputGosduraVisceral,
                        _InputIdadeBiologica,
                        _InputMassaMagra,
                        _InputMetabolismoBasal,
                        _inputPescoco,
                        _inputPeso,
                        _inputQuadril;

    [SerializeField]
    private Text _inputImc;
    [SerializeField]
    private Dropdown _dropListaCliente;
    private int _index;
    private string _data;

    DatabaseReference referencia;


    Avaliacao avaliacao = new Avaliacao();
    Cliente cliente = new Cliente();
    [SerializeField]
    ListaDeClientes listadeclientes;

    [SerializeField]
    GameObject avisoErro;

    [SerializeField]
    private Text textoErro;

    Firebase.Auth.FirebaseAuth auth;

    void Start()
    {
        PermicaoBanco();
        referencia = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void BtCadastrarAvaliação()
    {

        if (_inputAbdomem.text.Trim() != "" &&
                        _InputbicepsDireito.text.Trim() != "" &&
                        _InputBicepsEsquerdo.text.Trim() != "" &&
                        _inputBusto.text.Trim() != "" &&
                        _inputCoxaDireita.text.Trim() != "" &&
                        _InputCoxaEsquerda.text.Trim() != "" &&
                        _InputGorduraCorporal.text.Trim() != "" &&
                        _inputGosduraVisceral.text.Trim() != "" &&
                        _InputIdadeBiologica.text.Trim() != "" &&
                        _InputMassaMagra.text.Trim() != "" &&
                        _InputMetabolismoBasal.text.Trim() != "" &&
                        _inputPescoco.text.Trim() != "" &&
                        _inputPeso.text.Trim() != "" &&
                        _inputQuadril.text.Trim() != "" &&
                        _inputImc.text.Trim() != "")
        {
            avaliacao.Abdomen = float.Parse(_inputAbdomem.text);//95f;
            avaliacao.BicepsDireito = float.Parse(_InputbicepsDireito.text);//35f;
            avaliacao.BicepsEsquerdo = float.Parse(_InputBicepsEsquerdo.text);//35f;
            avaliacao.Busto = float.Parse(_inputBusto.text);//105f;
            avaliacao.CoxaDireita = float.Parse(_inputCoxaDireita.text);// 65f;
            avaliacao.CoxaEsquerda = float.Parse(_InputCoxaEsquerda.text);// 65f;
            avaliacao.DataAvaliacao = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            avaliacao.GorduraCorporal = float.Parse(_InputGorduraCorporal.text); //15f;
            avaliacao.GorduraVisceral = float.Parse(_inputGosduraVisceral.text); //1f;
            avaliacao.IdadeBiologica = int.Parse(_InputIdadeBiologica.text);// 20;
            avaliacao.Imc = float.Parse(_inputImc.text); //15.5f;
                                  //avaliacao.Imc = int.Parse(_inputImc.text); //15.5f;
            avaliacao.MassaMagra = float.Parse(_InputMassaMagra.text); //56f;
            avaliacao.MetabolismoBasal = float.Parse(_InputMetabolismoBasal.text);//= 50f;
            avaliacao.Pescoso = float.Parse(_inputPescoco.text);// 10f;
            avaliacao.Peso = float.Parse(_inputPescoco.text); //65f;
            avaliacao.Quadril = float.Parse(_inputQuadril.text); //105f;


            avaliacao.DataAvaliacao = avaliacao.DataAvaliacao.Replace("/", "-");
            string json = JsonUtility.ToJson(avaliacao);

            referencia.Child("CONSULTORES").Child(PlayerPrefs.GetString("userID")).Child("CLIENTES").Child(PlayerPrefs.GetString("ClienteID")).Child("AVALIACOES").Child(avaliacao.DataAvaliacao).SetRawJsonValueAsync(json);
            Debug.Log("Avaliação cadastrada com sucesso em " + avaliacao.DataAvaliacao);

            //scenesController.BtnScenePerfil();
            avisoErro.SetActive(true);
            avisoErro.GetComponent<Image>().color = new Color32(0, 185, 156, 100);
            textoErro.text = "Avaliação Cadastrada";
            StartCoroutine("Aguardar");

        }
        else
        {
            avisoErro.SetActive(true);
            textoErro.text = "Todos os campos devem ser preenchidos";
        }

        
     }


    // Update is called once per frame

    private void PermicaoBanco()
    {
        //Permicao para Banco (False)
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dbUrl);
        FirebaseApp.DefaultInstance.SetEditorP12FileName("harmonylife-d645c-6a18fdd1ae4a.p12");
        FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("firebase-adminsdk-8vs6e@harmonylife-d645c.iam.gserviceaccount.com");
        FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
    }

    IEnumerator Aguardar()
    {
        yield return new WaitForSeconds(2f);
        scenesController.BtnScenePerfil();

    }
}
