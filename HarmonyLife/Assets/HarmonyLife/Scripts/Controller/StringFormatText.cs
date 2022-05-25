using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringFormatText : MonoBehaviour
{

    [SerializeField]
    private InputField _InputTelefone;

    [SerializeField]
    private string _Telefone;


    public void OnValueChanged() // Supposing input = "953"
    {
        // _Telefone = _InputTelefone.text;

        //     System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(_Telefone, @"(\d{2})(\d{5})(\d{4})");
        //     if ( match.Success )
        //     {
        //         // match.Groups[0] will return the whole input
        //         string output = string.Format("({0}) {1}-{2}", match.Groups[1], match.Groups[2], match.Groups[3]);
        //         Debug.Log( output ); // Will log "(954) 555-1212"

        //         //_InputTelefone.text = "";
        //         _InputTelefone.text = output.ToString();
        //     }
        //Regex.Replace("1112224444", @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");

        System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(_InputTelefone.text, @"(\d{2})(\d{5})(\d{4})");

    }
}
