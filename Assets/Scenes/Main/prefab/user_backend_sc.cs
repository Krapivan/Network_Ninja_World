using UnityEngine;
using Mirror;

public class user_backend_sc : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_resource_db_sc _user_resource_db;

    [SerializeField] connection_command_sc _connection_command_sc;
    [SerializeField] resource_command_sc _resource_command_sc;
    [SerializeField] hero_command_sc _hero_command_sc;

    [SerializeField] GameObject _user;

    private void Start()
    {
        _connection_command_sc.Cmd_Login(_inf_db._user_login_setting._is_login, _inf_db._user_login_setting._login, _inf_db._user_login_setting._password);
    }
    public void Inf_Db_Con(int user_id)
    {
        _inf_db._user_backend_sc = this;

        _inf_db._user_command._connection_command_sc = _connection_command_sc;
        _inf_db._user_command._resource_command_sc = _resource_command_sc;
        _inf_db._user_command._hero_command_sc = _hero_command_sc;

        Db_Load(user_id);
    }

    void Db_Load(int user_id)
    {
        //User Resource Db
        _inf_db._user_command._resource_command_sc.Cmd_Resource_Db_Load(user_id);
        //Hero & User Hero Db
        _inf_db._user_command._hero_command_sc.Update_Db(user_id);
    }
}
