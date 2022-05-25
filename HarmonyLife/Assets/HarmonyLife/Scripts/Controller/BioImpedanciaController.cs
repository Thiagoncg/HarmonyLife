using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScript.Steps;

public class BioImpedanciaController : MonoBehaviour
{
    DatabaseReference referencia;
    FirebaseController fbController;

    [SerializeField]
    private Text textoTopo;
    [SerializeField]
    private Text txtNome;
    [SerializeField]
    private Text txtDataAvaliacao;
    [SerializeField]
    private Text txtIdade;
    [SerializeField]
    private Text txtSexo;
    [SerializeField]
    private Text txtIMC;
    [SerializeField]
    private Text txtGordura;

    [SerializeField]
    private Image imagemImc;

    [SerializeField]
    private Sprite[] imagensFem;
    [SerializeField]
    private Sprite[] imagensMas;


    [SerializeField]
    private RectTransform[] barrasGraficoGordura;
    [SerializeField]
    private Text[] numerosGraficoGordura;

    [SerializeField]
    private RectTransform[] barrasGraficoMusculo;
    [SerializeField]
    private Text[] numerosGraficoMusculo;

    [SerializeField]
    private Text[] dataAvaliacao;

    // Start is called before the first frame update

    private int contadorIndice;
    private List<Avaliacao> listaAval;
    private List<Cliente> listaCli;
    



    void Start()
    {
        fbController = new FirebaseController();
        fbController.PermicaoBanco();
        referencia = FirebaseDatabase.DefaultInstance.RootReference;

        listaAval = new List<Avaliacao>();
        listaCli = new List<Cliente>();

        CarregaBioimpedancias();


    }

    public void CarregaBioimpedancias()
    {

        FirebaseDatabase.DefaultInstance.GetReference("CONSULTORES").Child(PlayerPrefs.GetString("userID")).Child("CLIENTES").Child(PlayerPrefs.GetString("ClienteID")).GetValueAsync().ContinueWith(task =>

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

                    Cliente cli = new Cliente();
                    cli.DetalhesCliente = task.Result.Child("DetalhesCliente").Value.ToString();
                    cli.Idade = Convert.ToInt32(task.Result.Child("Idade").Value.ToString());
                    cli.Nome = task.Result.Child("Nome").Value.ToString();
                    cli.Sexo = task.Result.Child("Sexo").Value.ToString();
                    cli.Telefone = task.Result.Child("Telefone").Value.ToString();
                    cli.Email = task.Result.Child("Email").Value.ToString();


                    listaCli.Add(cli);

                    // List<string> clientesS = new List<string>();
                    foreach (var item in snapshot.Children)
                    {

                        if (item.Key.ToString() == "Nome")
                        {
                            textoTopo.text = string.Format("Olá {0}, veja como está a sua composição /n corporal e acompanhe o seu histórico", item.Value.ToString());


                        }



                        contadorIndice = 0;




                    }

                }



                FirebaseDatabase.DefaultInstance.GetReference("CONSULTORES").Child(PlayerPrefs.GetString("userID")).Child("CLIENTES").Child(PlayerPrefs.GetString("ClienteID")).Child("AVALIACOES").GetValueAsync().ContinueWith(task2 =>

                {
                    if (task2.IsFaulted)
                    {
                        Debug.LogError("Falha ao carregar dados " + task2.Exception.ToString());
                        return;
                    }
                    if (task2.IsCompleted)
                    {
                        if (task2.Result != null && task2.Result.Exists)
                        {
                            DataSnapshot snapshot = task2.Result;
                            Debug.Log("DataManager: Carregado Dados");

                            
                            // List<string> clientesS = new List<string>();
                            foreach (var item in snapshot.Children)
                            {
                                Avaliacao aval = new Avaliacao();
                                aval.GorduraCorporal = Single.Parse(item.Child("GorduraCorporal").Value.ToString());
                                aval.MassaMagra = Single.Parse(item.Child("MassaMagra").Value.ToString());
                                aval.DataAvaliacao = item.Child("DataAvaliacao").Value.ToString();
                                aval.Imc = float.Parse(item.Child("Imc").Value.ToString());

                                listaAval.Add(aval);
                                contadorIndice = 0;
                            }


                            int cont = 0;
                            int posInicialAval = 0;
                            if (listaAval.Count > 11)
                            {
                                posInicialAval = listaAval.Count - 11;
                            }

                            for (int i = posInicialAval; i < listaAval.Count; i++)                          
                            {
                                numerosGraficoGordura[cont].text = listaAval[i].GorduraCorporal.ToString();
                                barrasGraficoGordura[cont].localScale = new Vector3(1, listaAval[i].GorduraCorporal / 100f, 0);

                                numerosGraficoMusculo[cont].text = listaAval[i].MassaMagra.ToString();
                                barrasGraficoMusculo[cont].localScale = new Vector3(1, listaAval[i].MassaMagra / 100f, 0);

                                dataAvaliacao[cont].text = listaAval[i].DataAvaliacao.ToString();
                               
                                cont++;
                            }



                            if (cont < 10)
                            {
                                for (int i = cont; i < barrasGraficoGordura.Length; i++)
                                {
                                    barrasGraficoGordura[i].localScale = new Vector3(1, 0, 0);
                                    numerosGraficoGordura[i].text = "";
                                    barrasGraficoMusculo[i].localScale = new Vector3(1, 0, 0);
                                    numerosGraficoMusculo[i].text = "";
                                    dataAvaliacao[i].text = "";
                                }
                            }

                        }

                        if (listaAval.Count > 0)
                        {
                            PreencheCampos();

                        }
                        else
                        {
                            SceneManager.LoadScene("5_CadastrarAvaliacao");
                        }


                        AtualizaImagem();

                    }
                });


            }
        });


    }

    private void AtualizaImagem()
    {
        if (txtSexo.text.ToString().ToLower().Contains("masc"))
        {
            string texto = txtIMC.text.ToString();
            texto = texto.Replace("IMC: ", string.Empty);
            if (float.Parse(texto) <= 20f)
            {
                imagemImc.sprite = imagensMas[0];
            }
            else
            {
                if (float.Parse(texto) <= 40f)
                {
                    imagemImc.sprite = imagensMas[1];
                }
                else
                {
                    if (float.Parse(texto) <= 60f)
                    {
                        imagemImc.sprite = imagensMas[2];
                    }
                    else
                    {
                        if (float.Parse(texto) <= 80f)
                        {
                            imagemImc.sprite = imagensMas[3];
                        }
                        else
                        {
                            if (float.Parse(texto) > 80f)
                            {
                                imagemImc.sprite = imagensMas[4];
                            }
                        }
                    }
                }
            }

        }
        else
        {
            string texto = txtIMC.text.ToString();
            texto = texto.Replace("IMC: ", string.Empty);
            if (float.Parse(texto) <= 20f)
            {
                imagemImc.sprite = imagensFem[0];
            }
            else
            {
                if (float.Parse(texto) <= 40f)
                {
                    imagemImc.sprite = imagensFem[1];
                }
                else
                {
                    if (float.Parse(texto) <= 60f)
                    {
                        imagemImc.sprite = imagensFem[2];
                    }
                    else
                    {
                        if (float.Parse(texto) <= 80f)
                        {
                            imagemImc.sprite = imagensFem[3];
                        }
                        else
                        {
                            if (float.Parse(texto) > 80f)
                            {
                                imagemImc.sprite = imagensFem[4];
                            }
                        }
                    }
                }
            }
        }

    }

    public void BtnAvancar()
    {

        if (listaAval.Count > 0)
        {
            if (contadorIndice < listaAval.Count - 1)
            {
                contadorIndice++;
                PreencheCampos();

            }
            else
            {
                contadorIndice = 0;
                PreencheCampos();
            }
        }

    }

    public void BtnVoltar()
    {

        if (listaAval.Count > 0)
        {
            if (contadorIndice == 0)
            {
                contadorIndice = listaAval.Count - 1;
                PreencheCampos();


            }
            else
            {
                contadorIndice--;
                PreencheCampos();
            }
        }

    }



    public void PreencheCampos()
    {

        txtGordura.text = listaAval[contadorIndice].GorduraCorporal.ToString() + "%";
        txtDataAvaliacao.text = "Data: " + listaAval[contadorIndice].DataAvaliacao.ToString();
        txtIMC.text = "IMC: " + listaAval[contadorIndice].Imc.ToString();
        txtNome.text = "Nome: " + listaCli[0].Nome.ToString();
        txtIdade.text = "Idade: " + listaCli[0].Idade.ToString();
        txtSexo.text = listaCli[0].Sexo.ToString();
        AtualizaImagem();

    }

}
