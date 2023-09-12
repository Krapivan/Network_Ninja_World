using Mirror;
using System;
using System.IO;
using UnityEngine;

public class resource_command_sc : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_resource_db_sc _user_resource_db;


    [SerializeField] User_Resource_js _user_resource_js;


    //User Resource
    [Server]
    User_Resource Srv_User_Resorce(int user_id)
    {
        _user_resource_js = JsonUtility.FromJson<User_Resource_js>(_inf_db._user_resource_json.text);

        User_Resource user_resource = null;

        for (int i = 0; i < _user_resource_js._user_resource_js.Count; i++)
        {
            if (_user_resource_js._user_resource_js[i]._user_id == user_id)
            {
                user_resource = _user_resource_js._user_resource_js[i];
                break;
            }
        }

        return user_resource;
    }


    //New User Resource
    [Server]
    public void Srv_New_User_Resource(int user_id)
    {
        _user_resource_js = JsonUtility.FromJson<User_Resource_js>(_inf_db._user_resource_json.text);

        User_Resource user_resource = new()
        {
            _user_name = "player_" + UnityEngine.Random.Range(1, 1000),
            _last_login_date = DateTime.Now.ToShortDateString(),
            _user_id = user_id,
            _money = 0,
            _gold = 0,
            _coin = 0,
            _energy_now = 30,
            _energy_mx = 30
        };

        _user_resource_js._user_resource_js.Add(user_resource);

        string json_text = JsonUtility.ToJson(_user_resource_js);
        File.WriteAllText(Application.dataPath + "/Json/user_resource_json.json", json_text);
    }


    //User Resource Db Load
    [Command]
    public void Cmd_Resource_Db_Load(int user_id)
    {
        Srv_Resource_Db_Load(user_id);
    }
    [Server]
    public void Srv_Resource_Db_Load(int user_id)
    {
        Rpc_Resorce_Db_Load(Srv_User_Resorce(user_id));
    }
    [TargetRpc]
    void Rpc_Resorce_Db_Load(User_Resource user_resource)
    {
        _user_resource_db._user_name = user_resource._user_name;
        _user_resource_db._last_login_date = user_resource._last_login_date;
        _user_resource_db._user_id = user_resource._user_id;
        _user_resource_db._money = user_resource._money;
        _user_resource_db._gold = user_resource._gold;
        _user_resource_db._coin = user_resource._coin;
        _user_resource_db._energy_now = user_resource._energy_now;
        _user_resource_db._energy_mx = user_resource._energy_mx;
    }
}
