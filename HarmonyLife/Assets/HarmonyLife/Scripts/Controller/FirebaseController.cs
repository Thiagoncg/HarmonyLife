using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;

public class FirebaseController : MonoBehaviour
{
    [SerializeField]    private Text _textAviso;
    [SerializeField]    private GameObject _imgaviso;
    private string dbUrl = "https://harmonylife-d645c.firebaseio.com/";
    // Use this for initialization
    void Start()
    {
        VerificaCompatibilidade();
    }
    public void VerificaCompatibilidade()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Celular compativel com nossa aplicação");

            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                _textAviso.text = "Atualize o Google Play Service";
                Debug.Log("atualise o GoolePlay Service");
            }
        });
    }


    public void PermicaoBanco()
    {
        //Permicao para Banco (False)
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dbUrl);
        FirebaseApp.DefaultInstance.SetEditorP12FileName("harmonylife-d645c-6a18fdd1ae4a.p12");
        FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("firebase-adminsdk-8vs6e@harmonylife-d645c.iam.gserviceaccount.com");
        FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
    }
}
