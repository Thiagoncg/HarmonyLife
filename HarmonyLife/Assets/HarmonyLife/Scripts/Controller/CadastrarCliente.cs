using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using LitJson;
using System;
using UnityEngine.SceneManagement;

public class CadastrarCliente : MonoBehaviour {
          
    [SerializeField]
    string dbUrl = "https://harmonylife-d645c.firebaseio.com/";

    //UIVariables
    [SerializeField]
    private InputField _inputNome;

    [SerializeField]
    private InputField _inputTelefone;

    [SerializeField]
    private InputField _inputEmail;

    [SerializeField]
    private InputField _inputDetalhesCliente;

    [SerializeField]
    private InputField _inputIdade;

    [SerializeField]
    private Dropdown _dropSexo;

    [SerializeField]
    private GameObject _imgAviso;

    [SerializeField]
    private Text _textAviso;

    private int _idCliente;

    DatabaseReference referencia;



    Cliente cliente = new Cliente();
     Firebase.Auth.FirebaseAuth auth;
    // Firebase.Auth.FirebaseUser user;


    private void Start()
    {
        PermicaoBanco();
        referencia = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void BtCadastrarCliente()
    {
        cliente.Nome = _inputNome.text;
        cliente.Telefone = _inputTelefone.text;
        cliente.Email = _inputEmail.text;
        cliente.DetalhesCliente = _inputDetalhesCliente.text;
        cliente.Idade = int.Parse(_inputIdade.text);
        cliente.Sexo = _dropSexo.captionText.text;
        
        //Cria um id de usuário uma chave unica
        string key = referencia.Child(cliente.Nome).Push().Key;

        string json = JsonUtility.ToJson(cliente);
        //referencia.Child("CONSULTORES").Child(PlayerPrefs.GetString("userID")).Child("CLIENTES").Child(cliente.Nome).SetRawJsonValueAsync(json);
        referencia.Child("CONSULTORES").Child(PlayerPrefs.GetString("userID")).Child("CLIENTES").Child(key).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning("Erro ao Cadastrar Cliente");
                _imgAviso.SetActive(true);
                _textAviso.text = "ERRO:" + task.Exception;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogWarning("Erro ao cadastrar Cliente: " + task.Exception);
                _imgAviso.SetActive(true);
                _textAviso.text = "ERRO:" + task.Exception;
                return;
            }

            _imgAviso.SetActive(true);
            _imgAviso.GetComponent<Image>().color = new Color32(0, 185, 156, 100);
            _textAviso.text = "Cliente Cadastrado com Sucesso";
            Debug.Log("cadastro realizado com Sucesso");
            StartCoroutine("Aguarde"); // Apos a confirmação do cadastro ira para a pagina de perfil 3 cegundo
        });

    }

    private void PermicaoBanco()
    {
        //Permicao para Banco (False)
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dbUrl);
        FirebaseApp.DefaultInstance.SetEditorP12FileName("harmonylife-d645c-6a18fdd1ae4a.p12");
        FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("firebase-adminsdk-8vs6e@harmonylife-d645c.iam.gserviceaccount.com");
        FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
    }

    IEnumerator Aguarde()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("2_Perfil");
    }
}
