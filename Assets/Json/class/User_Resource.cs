using System;
using System.Collections.Generic;

[Serializable]
public class User_Resource
{
    public string _user_name;
    public string _last_login_date;
    public int _user_id;
    public int _money;
    public int _gold;
    public int _coin;
    public int _energy_now;
    public int _energy_mx;
}
[Serializable]
public class User_Resource_js
{
    public List<User_Resource> _user_resource_js;
}