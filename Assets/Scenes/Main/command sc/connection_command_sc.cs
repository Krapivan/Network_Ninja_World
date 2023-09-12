using UnityEngine;
using Mirror;
using System.IO;

public class connection_command_sc : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;

    [SerializeField] user_backend_sc _user_backend_sc;

    [SerializeField] resource_command_sc _resource_command_sc;
    [SerializeField] hero_command_sc _hero_command_sc;

    [SerializeField] User_Login_js _user_login_js;


    [Command]
    public void Cmd_Login(bool is_login, string login, string password)
    {
        bool have = Srv_Check_User(login, password);

        if (is_login == true)
        {
            if (have == false)
            {
                Rpc_Disconnect_User();
            }
        }
        else if (is_login == false)
        {
            if (have == true)
            {
                Rpc_Disconnect_User();
            }
            else if (have == false)
            {
                Srv_Registration_User(login, password);
            }
        }

        _user_backend_sc.Inf_Db_Con(Srv_User_Id(login, password));
    }
    [Server]
    int Srv_User_Id(string login, string password)
    {
        int _user_id = 0;

        _user_login_js = JsonUtility.FromJson<User_Login_js>(_inf_db._user_login_json.text);

        for (int i = 0; i < _user_login_js._user_login_js.Count; i++)
        {
            if (_user_login_js._user_login_js[i]._login == login && _user_login_js._user_login_js[i]._password == password)
            {
                _user_id = _user_login_js._user_login_js[i]._user_id;
            }
        }

        return _user_id;
    }
    [Server]
    bool Srv_Check_User(string login, string password)
    {
        bool have = false;

        _user_login_js = JsonUtility.FromJson<User_Login_js>(_inf_db._user_login_json.text);

        for (int i = 0; i < _user_login_js._user_login_js.Count; i++)
        {
            if (_user_login_js._user_login_js[i]._login == login && _user_login_js._user_login_js[i]._password == password)
            {
                have = true;
            }
        }

        return have;
    }


    //New User
    [Server]
    void Srv_Registration_User(string login, string password)
    {
        _user_login_js = JsonUtility.FromJson<User_Login_js>(_inf_db._user_login_json.text);

        bool good_id = false;
        int user_id = 0;
        do
        {
            user_id = Random.Range(10000, 99999);
            good_id = true;
            for (int i = 0; i < _user_login_js._user_login_js.Count; i++)
            {
                if (_user_login_js._user_login_js[i]._user_id == user_id)
                {
                    good_id = false; break;
                }
            }
        }
        while (good_id == false);

        User_Login user_login = new User_Login()
        {
            _login = login,
            _password = password,
            _user_id = user_id
        };

        _user_login_js._user_login_js.Add(user_login);

        string json_text = JsonUtility.ToJson(_user_login_js);
        File.WriteAllText(Application.dataPath + "/Json/user_login_json.json", json_text);

        Srv_Create_New_User(user_id);
    }
    [Server]
    void Srv_Create_New_User(int user_id)
    {
        _resource_command_sc.Srv_New_User_Resource(user_id);
        _hero_command_sc.Srv_New_User_Hero(user_id);
    }


    //Disconnect
    [TargetRpc]
    void Rpc_Disconnect_User()
    {
        if (isClientOnly == true)
        {
            NetworkManager.singleton.StopClient();
        }
        else if (isServer == true)
        {
            NetworkManager.singleton.StopHost();
        }
    }
}
