using System;
using UnityEngine;

[CreateAssetMenu(menuName = "db/inf_db")]
public class inf_db_sc : ScriptableObject
{
    public user_login_setting _user_login_setting;

    public user_backend_sc _user_backend_sc;
    public commands _commands;
    public managers _managers;
    public database _database;
}
[Serializable]
public class commands
{
    public connection_command _connection_command;
    public general_command _general_command;
    public hero_command _hero_command;
    public rune_command _rune_command;
}
[Serializable]
public class managers
{
    public general_manager _general_manager;
    public hero_manager _hero_manager;
    public rune_manager _rune_manager;
    public story_manager _story_manager;
}
[Serializable]
public class database
{
    public user_general_db_sc _user_general_db;
    public user_hero_db_sñ _user_hero_db;
    public user_rune_db_sc _user_rune_db;
    public user_story_db_sc _user_story_db;

    public general_db_sc _general_db;
    public hero_db_sc _hero_db;
    public rune_db_sc _rune_db;
    public story_db_sc _story_db;

    public fight_db_sc _fight_db;
}


public class user_login_setting
{
    public bool _is_login;
    public string _login;
    public string _password;
    public string _mail;
}