using System;
using System.Collections.Generic;

[Serializable]
public class User_Login
{
    public string _login;
    public string _password;
    public string _mail;
    public string _user_id;
}
[Serializable]
public class Users_Logins
{
    public List<User_Login> _users_logins = new();
}
