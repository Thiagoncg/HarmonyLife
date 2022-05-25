using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalcularAvaliacao : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private InputField _inputPeso;

    [SerializeField]
    private InputField _inputAltura;

    [SerializeField]
    private Text _TextMsg;

    [SerializeField]
    private Text txtIMC;

    [SerializeField]
    private Text TextNomeCliente;

    [SerializeField]
    private Dropdown _dropNome;

    [SerializeField]
    private Slider SliderImc;

    string Msg = "";

    // Update is called once per frame

    private void Start()
    {
        TextNomeCliente.text = PlayerPrefs.GetString("ClienteNome").ToUpper();
        _TextMsg.text = Msg;
    }

    public void CalculaImc()
    {
        string Speso = _inputPeso.text;
        string SAltura = _inputAltura.text;

        float Peso = float.Parse(Speso);;
        float Altura = float.Parse(SAltura) / 100f;  


        float Imc = Peso / (Altura * Altura);

        Msg = "";

        if (Imc < 16)
        {
            Msg += (" Magreza Severa ");
        }
        else if(Imc < 17)
        {
            Msg += (" Magreza Moderada ");
        }
        else if(Imc < 18.5)
        {
             Msg += (" Magreza Leve ");
        }
        else if(Imc < 25)
        {
             Msg += (" Peso Normal ");
        }
        else if(Imc < 30)
        {
             Msg += (" Sobre Peso ");
        }
        else if(Imc < 35)
        {
             Msg += (" Obesidade Classe I ");
        }
        else if (Imc < 40)
        {
             Msg += (" Obseidade Classe II ");
        }
        else
        {
             Msg += (" Obesidade Classe III ");
        }
        // Msg = "O IMC de " + PlayerPrefs.GetString("ClienteNome") + " corresponde a: ";

        //txtIMC.text = Msg +"" + Imc.ToString();

        _TextMsg.text = "";
        _TextMsg.text = (Msg);

        txtIMC.text = "";
        txtIMC.text = Imc.ToString();

        Debug.LogWarning(Msg + "(IMC) = " + Imc);
        SliderImc.value = Imc;




    }
}
