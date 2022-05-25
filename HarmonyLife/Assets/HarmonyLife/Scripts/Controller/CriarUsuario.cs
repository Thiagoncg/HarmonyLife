using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CriarUsuario : MonoBehaviour
{
    //VARIABLES SERIALIZEDS
    [SerializeField]
    private GameObject _telaCadastro;

    [SerializeField]
    private GameObject _imgDeAvisos;

    [SerializeField]
    private Text _textAviso;

    //[SerializeField]
    //private Text _textEmail, _texSenha, _textReptSenha;

    [SerializeField]
    private InputField _inputMail;

    [SerializeField]
    private InputField _inputPassword;

    [SerializeField]
    private InputField _inputReptPassword;
    

    //PRIVATE VARIBLES
    private string _email;
    private string _password;

    Firebase.Auth.FirebaseAuth auth;
    FirebaseController firebaseController = new FirebaseController();

    User user = new User();

    private void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        firebaseController.VerificaCompatibilidade();
    }
    void Start()
    {
        _telaCadastro.SetActive(false);
    }
    //Abre e Fecha tela de Cadastro
    public void AbriTelaCadastro()
    {
        if (_telaCadastro.activeSelf == false)
        {
            _telaCadastro.SetActive(true);
        }
        else
        {
            _telaCadastro.SetActive(false);
        }
        _imgDeAvisos.SetActive(false);
    }
    public void ValidarEmailESenha()
    {
        string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        if (System.Text.RegularExpressions.Regex.IsMatch(_inputMail.text, strModelo))
        {
            if (_inputPassword.text == _inputReptPassword.text)
            {
                //Senhas Iguais e email confere
                CadastrarPorEmail();
                Debug.Log("Senhas Iguais");
            }
            else
            {
                _imgDeAvisos.SetActive(true);
                _textAviso.text = "As senhas não conferem!";
                Debug.LogWarning("As senhas não conferem!", transform);
            }
        }
        else
        {
            //email não valido
            _imgDeAvisos.SetActive(true);
            _textAviso.text = "Seu email esta invalido !";
            Debug.LogWarning("Seu email esta invalido!", transform);
        }
    }
    public void CadastrarPorEmail()
    {
         _email = _inputMail.text;
        _password = _inputPassword.text;
        user.Email = _inputMail.text;


        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.CreateUserWithEmailAndPasswordAsync(_email, _password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                _imgDeAvisos.SetActive(true);
                _textAviso.text = "Erro ao criar usuário: " + task.Exception;
                Debug.LogWarning("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogWarning("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                _imgDeAvisos.SetActive(true);
                _textAviso.text = "Erro ao criar usuário: " + task.Exception;
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            _imgDeAvisos.SetActive(true);
            _imgDeAvisos.GetComponent<Image>().color = new Color32(0, 185, 156, 100);
            _textAviso.text = " Cadastro Criado com sucesso !" + newUser.DisplayName + newUser.UserId;

            //Aguarda antes de ir para a prixima tela
            StartCoroutine("Aguardar");


            //Send Emails Verification
            //newUser.SendEmailVerificationAsync().ContinueWith(t => {
            //Debug.Log("Verifique o seu e-mail");
            //_textAviso.text = "Um E-mail de confirmação de senha foi enviado!" + newUser.DisplayName + newUser.UserId;
            //});
        });
    }
    public void FecharAvido()
    {
        _imgDeAvisos.SetActive(false);
    }
    public void JaSouCadastrado()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator Aguardar()
    { 
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
    }
}
