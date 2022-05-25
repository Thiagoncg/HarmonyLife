using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class User
{
    public string UserID;
    public string Username;
    public string Email;
    public string Password;

    public User(string userId,  string username, string email, string password)
    {
        this.UserID = userId;
        this.Username = Username;
        this.Email = Email;
        this.Password = password;
    }

    public User()
    {

    }
}
