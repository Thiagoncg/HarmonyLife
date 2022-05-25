using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Cliente
{
    public string IdCliente;
    public string Nome;
    public string Telefone;
    public string Email;
    public string DetalhesCliente;
    public int Idade;
    public string Sexo;


    public  Cliente(string IdCliente, string Nome, string Telefone, string Email, string DetalhesCliente, int Idade, string Sexo)
    {
        this.IdCliente = IdCliente;
        this.Nome = Nome;
        this.Telefone = Telefone;
        this.Email = Email;
        this.DetalhesCliente = DetalhesCliente;
        this.Idade = Idade;
        this.Sexo = Sexo;
    }
    public Cliente()
    {

    }
 }


