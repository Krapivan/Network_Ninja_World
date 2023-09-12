using System;
using System.Collections.Generic;

[Serializable]
public class User_Login
{
    public string _login;
    public string _password;
    public int _user_id;
}
[Serializable]
public class User_Login_js
{
    public List<User_Login> _user_login_js;
}
