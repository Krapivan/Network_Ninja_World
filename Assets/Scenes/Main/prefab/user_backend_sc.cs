using UnityEngine;
using Mirror;

public class user_backend_sc : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_general_db_sc _user_resource_db;

    [SerializeField] connection_command _connection_command_sc;
    [SerializeField] general_command _resource_command_sc;
    [SerializeField] hero_command _hero_command_sc;
    [SerializeField] rune_command _rune_command_sc;

    [SerializeField] general_manager _general_manager;
    [SerializeField] hero_manager _hero_manager;
    [SerializeField] rune_manager _rune_manager;

    [SerializeField] GameObject _user;

    private void Start()
    {
        Command_Manager_Load();
        user_login_setting user_login_setting = new()
        {
            _is_login = _inf_db._user_login_setting._is_login,
            _login = _inf_db._user_login_setting._login,
            _password = _inf_db._user_login_setting._password,
            _mail = _inf_db._user_login_setting._mail
        };
        _connection_command_sc.Cmd_Login(user_login_setting);
    }
    void Command_Manager_Load()
    {
        _inf_db._user_backend_sc = this;

        //commands
        _inf_db._commands._connection_command = _connection_command_sc;
        _inf_db._commands._general_command = _resource_command_sc;
        _inf_db._commands._hero_command = _hero_command_sc;
        _inf_db._commands._rune_command = _rune_command_sc;

        //managers
        _inf_db._managers._general_manager = _general_manager;
        _inf_db._managers._hero_manager = _hero_manager;
        _inf_db._managers._rune_manager = _rune_manager;
    }

    public void Data_Load(string user_id)
    {
        //User General Db
        _inf_db._commands._general_command.Cmd_User_General_Db_Load(user_id);

        //Hero & User Hero Db
        _inf_db._commands._hero_command.Cmd_Hero_Db_Load();
        _inf_db._commands._hero_command.Cmd_User_Hero_Db_Load(user_id);
        //Rune Db
        _inf_db._commands._rune_command.Cmd_User_Rune_Db_Load(user_id);
    }
}
