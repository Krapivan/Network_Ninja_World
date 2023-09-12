using System;
using UnityEngine;

[CreateAssetMenu(menuName = "db/inf_db")]
public class inf_db_sc : ScriptableObject
{
    public TextAsset _user_login_json;
    public TextAsset _user_resource_json;
    public TextAsset _user_heroes_json;

    public user_login_setting _user_login_setting;

    public user_backend_sc _user_backend_sc;
    public user_command _user_command;
}
[Serializable]
public class user_command
{
    public connection_command_sc _connection_command_sc;
    public resource_command_sc _resource_command_sc;
    public hero_command_sc _hero_command_sc;
}
public class user_login_setting
{
    public bool _is_login;
    public string _login;
    public string _password;
}