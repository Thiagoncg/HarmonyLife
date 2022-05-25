using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClientePrefabController : MonoBehaviour
{
   public Cliente cliente;

    [SerializeField]
    private Text nome;
    
    [SerializeField]
    private Text telefone;

    [SerializeField]
    private Text email;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (cliente != null)
        {
            nome.text = cliente.Nome;
            telefone.text = cliente.Telefone;
            email.text = cliente.Email;

        }

    }


    public void NovaAvaliacao()
    {
        PlayerPrefs.SetString("ClienteID", cliente.IdCliente.ToString());
        PlayerPrefs.SetString("ClienteNome", cliente.Nome.ToString());
        SceneManager.LoadScene("5_CadastrarAvaliacao");
    }

    public void Bioimpedancia()
    {
        PlayerPrefs.SetString("ClienteID", cliente.IdCliente.ToString());
        PlayerPrefs.SetString("ClienteNome", cliente.Nome.ToString());
        SceneManager.LoadScene("4_Bioimpedancia");
    }
}
