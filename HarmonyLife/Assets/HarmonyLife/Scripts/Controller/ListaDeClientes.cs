using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class ListaDeClientes : MonoBehaviour

{
    [SerializeField]
    private Dropdown _listagemCliente;

    public string IdDoCliente;
    
    Cliente cliente = new Cliente();
   public List<Cliente> clientes = new List<Cliente>();
    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://harmonylife-d645c.firebaseio.com/");

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        CarregaClientes();
    }

    // Update is called once per frame

    public void CarregaClientes()
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
                if (task.Result != null && task.Result.Exists)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("DataManager: Carregado Dados");

                   // List<string> clientesS = new List<string>();
                    foreach (var item in snapshot.Children)
                    {

                        Cliente clienteGet = new Cliente();
                        clienteGet.Nome = item.Child("Nome").Value.ToString();
                        clienteGet.IdCliente = item.Key.ToString();
                        clienteGet.Telefone = item.Child("Telefone").Value.ToString();

                        clientes.Add(clienteGet);
                      //  clientesS.Add(clienteGet.Nome);
                        Debug.Log("teste");
                        List<string> clientesS = new List<string>() { item.Child("Nome").Value.ToString() + "-" +item.Child("Telefone").Value.ToString() };


                        _listagemCliente.AddOptions(clientesS);
                        int paradinha = clientes.Count;
                        Debug.Log("parada");
                       
                    }
                
                }
            }
        });
    }
}