using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using Firebase.Database;

public class PerfilControler : MonoBehaviour {

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    [SerializeField]
    private Text _textEmail;

    [SerializeField]
    private Text _textPerfilName;

    [SerializeField]
    private Text contadorCliente;

    [SerializeField]
    private Text _textDataCadastro;

    #region controle do scroll

    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private RectTransform itemPrefab = null;

    [SerializeField]
    private GameObject itemInstanciar = null;

    [SerializeField]
    private RectTransform content = null;

    [SerializeField]
    private long numberOfItems = 0;


    public List<Cliente> clientesS;

    #endregion


    private void Start()
    {
        InitializeFirebase();
        DisplayPerfil();
        LoadClientes();
    }

    public void BtnCadastrarCliente()
    {
        SceneManager.LoadScene("3_CadastrarCliente");
    }

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }
    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;

        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                DisplayPerfil();
            }
        }
    }
    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    public void DisplayPerfil()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            //_textPerfilName.text = user.DisplayName;
            _textEmail.text = user.Email;
            _textPerfilName.text = user.DisplayName;
            //System.Uri photo_url = user.PhotoUrl;
            // The user's Id, unique to the Firebase project.
            // Do NOT use this value to authenticate with your backend server, if you
            // have one; use User.TokenAsync() instead.
            string uid = user.UserId;
        }
    }



    public void LoadClientes()
    {

        FirebaseDatabase.DefaultInstance.GetReference("CONSULTORES").Child(PlayerPrefs.GetString("userID")).Child("CLIENTES").GetValueAsync().ContinueWith(task =>

        {
            if (task.IsFaulted)
            {
                Debug.LogError("Falha ao carregar dados " + task.Exception.ToString());
                return;
            }
            if (task.IsCompleted)
            {
                bool first = true;
                if (task.Result != null && task.Result.Exists)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("DataManager: Carregado Dados");

                    clientesS = new List<Cliente>();
                    numberOfItems = snapshot.ChildrenCount;
                    content.sizeDelta = new Vector2(0, numberOfItems * itemPrefab.rect.height);
                    float spawnY = itemPrefab.rect.height;
                    contadorCliente.text = numberOfItems.ToString(); 
                    foreach (var item in snapshot.Children)
                    {

                        Cliente clienteGet = new Cliente();
                        clienteGet.Nome = item.Child("Nome").Value.ToString();
                        clienteGet.IdCliente = item.Key.ToString();
                        clienteGet.Telefone = item.Child("Telefone").Value.ToString();
                        clienteGet.Email = item.Child("Email").Value.ToString();//verificação de e-mail

                        clientesS.Add(clienteGet);

                        Vector3 pos;
                        //newSpawn Position

                     
                            pos = new Vector3(SpawnPoint.localPosition.x, -spawnY, SpawnPoint.localPosition.z);

                       


                        //instantiate item
                        GameObject SpawnedItem = Instantiate(itemInstanciar, pos, SpawnPoint.rotation);
                        //setParent
                        SpawnedItem.transform.SetParent(SpawnPoint, false);
                        //get ItemDetails Component
                       


                        SpawnedItem.GetComponent<ClientePrefabController>().cliente = clienteGet;

                    }

                    for (int i = 0; i < numberOfItems; i++)
                    {
                        


                    }


                }
            }
        });

    }



    public void PesquisaCliente()
    {


        


        //foreach (Cliente item in clientesS)
        //{

        //    Cliente clienteGet = new Cliente();
        //    clienteGet.Nome = item.Child("Nome").Value.ToString();
        //    clienteGet.IdCliente = item.Key.ToString();
        //    clienteGet.Telefone = item.Child("Telefone").Value.ToString();

        //    clientesS.Add(clienteGet);

        //    Vector3 pos;
        //    //newSpawn Position

        //    if (first)
        //    {
        //        pos = new Vector3(SpawnPoint.position.x, -spawnY / 2, SpawnPoint.position.z);
        //        first = false;
        //    }
        //    else
        //    {
        //        pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);

        //    }


        //    //instantiate item
        //    GameObject SpawnedItem = Instantiate(itemInstanciar, pos, SpawnPoint.rotation);
        //    //setParent
        //    SpawnedItem.transform.SetParent(SpawnPoint, false);
        //    //get ItemDetails Component
        //    spawnY -= -spawnY / 2;


        //    SpawnedItem.GetComponent<ClientePrefabController>().cliente = clienteGet;

        //}

    }
}
