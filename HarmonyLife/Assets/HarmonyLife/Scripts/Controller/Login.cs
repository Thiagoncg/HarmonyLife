using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Google;
// using Google.Impl;

public class Login : MonoBehaviour
{
    //UI Variables SerializeVariables
    [SerializeField] private InputField _textPassword, _textMail;
    [SerializeField] private Text _textAviso;
    [SerializeField] private GameObject _imgAviso;

    string googleIdToken;
    string googleAccessToken;



    User user = new User();
    // Use this for initialization
    private string email;
    private string password;

    //EmailAuth
    Firebase.Auth.FirebaseAuth auth;
    FirebaseController firebaseController = new FirebaseController();

    void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        firebaseController.VerificaCompatibilidade();
    }
    // Update is called once per frame

    public void LoginMail()
    {
        email = _textMail.text;
        password = _textPassword.text;

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning("CreateUserWithEmailAndPasswordAsync was canceled.");
                _imgAviso.SetActive(true);
                _textAviso.text = "ERRO:" + task.Exception;
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogWarning("Create User With Email And Password Async encountered an error: " + task.Exception);
                _imgAviso.SetActive(true);
                _textAviso.text = "ERRO:" + task.Exception;
                return;
            }
            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Usuário Cadastrado: {0} ({1} {2})", newUser.DisplayName, newUser.UserId, newUser.Email);
            _imgAviso.GetComponent<Image>().color = new Color32(0, 185, 156, 100);
            _imgAviso.SetActive(true);
            _textAviso.text = "Login bem sucedido";

            //Guarda os Dados do Usuário Logado.
            PlayerPrefs.SetString("userEmail", newUser.Email);
            PlayerPrefs.SetString("userID", newUser.UserId);
            PlayerPrefs.SetString("displayName", newUser.DisplayName); 

            Debug.Log("Dados Coleados Do Usuário " + PlayerPrefs.GetString("userEmail") + PlayerPrefs.GetString("displayName"));

            StartCoroutine("Aguardar");

        });
    }

    public void IrParaCadastro()
    {
        SceneManager.LoadScene(2);
    }

    //public void EsqueciMinhaSenha()
    //{
    //    Firebase.Auth.FirebaseUser user = auth.CurrentUser;
    //    string newPassword = "324050";
    //    if (user != null)
    //    {
    //        user.UpdatePasswordAsync(newPassword).ContinueWith(task => {
    //            if (task.IsCanceled)
    //            {
    //                Debug.LogError("UpdatePasswordAsync was canceled.");
    //                return;
    //            }
    //            if (task.IsFaulted)
    //            {
    //                Debug.LogError("UpdatePasswordAsync encountered an error: " + task.Exception);
    //                return;
    //            }

    //            Debug.Log("Password updated successfully.");
    //        });
    //    }

    //}
    public void LoginGoogle()
    { 
        



        Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }
    IEnumerator Aguardar()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(3);
    }
}
