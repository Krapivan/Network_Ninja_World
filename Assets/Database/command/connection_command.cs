using UnityEngine;
using Mirror;
using System.IO;

public class connection_command : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;

    [SerializeField] user_backend_sc _user_backend_sc;

    [SerializeField] general_command _general_command;
    [SerializeField] hero_command _hero_command;
    [SerializeField] rune_command _rune_command;


    //Read & Write User Login Json
    [Server]
    Users_Logins Srv_Read_Users_Logins()
    {
        try
        {
            Users_Logins users_logins = JsonUtility.FromJson<Users_Logins>(File.ReadAllText(Application.dataPath + "/Users_Data/users_logins.json"));
            return users_logins;
        }
        catch
        {
            return null;
        }
    }

    [Server]
    void Srv_Write_Users_Logins(Users_Logins users_logins)
    {
        try
        {
            Users_Logins last_users_logins = Srv_Read_Users_Logins();
            if (users_logins != null)
            {
                string json_text = JsonUtility.ToJson(users_logins);
                File.WriteAllText(Application.dataPath + "/Users_Data/users_logins.json", json_text);
            }
        }
        catch
        {
        }
    }


    //Login
    [Command]
    public void Cmd_Login(user_login_setting user_login_setting)
    {
        bool have = Srv_Check_User(user_login_setting);

        if (user_login_setting._is_login == true)
        {
            if (have == false)
            {
                Rpc_Disconnect_User();
            }
            else if (have == true)
            {
                Rpc_Login(Srv_Get_User_Login(user_login_setting));
            }
        }
        else if (user_login_setting._is_login == false)
        {
            if (have == true)
            {
                Rpc_Disconnect_User();
            }
            else if (have == false && Srv_Is_Unique_Mail(user_login_setting._mail) == true)
            {

                Srv_Registration_User(user_login_setting);
            }
        }
    }

    [TargetRpc]
    void Rpc_Login(User_Login user_login)
    {
        _user_backend_sc.Data_Load(user_login._user_id);
    }

    [TargetRpc]
    void Rpc_Disconnect_User()
    {
        if (isClientOnly == true)
        {
            NetworkManager.singleton.StopClient();
        }
    }


    //Sub
    [Server]
    User_Login Srv_Get_User_Login(user_login_setting user_login_setting)
    {
        Users_Logins users_logins = Srv_Read_Users_Logins();

        for (int i = 0; i < users_logins._users_logins.Count; i++)
        {
            if (users_logins._users_logins[i]._login == user_login_setting._login && users_logins._users_logins[i]._password == user_login_setting._password)
            {
                return users_logins._users_logins[i];
            }
        }

        return null;
    }
    [Server]
    bool Srv_Check_User(user_login_setting user_login_setting)
    {
        Users_Logins users_logins = Srv_Read_Users_Logins();

        for (int i = 0; i < users_logins._users_logins.Count; i++)
        {
            if (users_logins._users_logins[i]._login == user_login_setting._login && users_logins._users_logins[i]._password == user_login_setting._password)
            {
                return true;
            }
        }

        return false;
    }
    [Server]
    bool Srv_Is_Unique_UID(string user_id)
    {
        Users_Logins users_logins = Srv_Read_Users_Logins();

        for (int i = 0; i < users_logins._users_logins.Count; i++)
        {
            if (users_logins._users_logins[i]._user_id == user_id)
            {
                return false;
            }
        }

        return true;
    }
    [Server]
    bool Srv_Is_Unique_Mail(string mail)
    {
        Users_Logins users_logins = Srv_Read_Users_Logins();

        for (int i = 0; i < users_logins._users_logins.Count; i++)
        {
            if (users_logins._users_logins[i]._mail == mail)
            {
                return false;
            }
        }

        return true;
    }
    string UID_Generation()
    {
        string uid = "";
        char[] symbols = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        for (int i = 1; i <= 8; i++)
        {
            uid += symbols[Random.Range(0, symbols.Length)];
        }

        Debug.Log(uid);
        return uid;
    }


    //New User
    [Server]
    void Srv_Registration_User(user_login_setting user_login_setting)
    {
        Users_Logins users_logins = Srv_Read_Users_Logins();

        string user_id;
        do
        {
            user_id = UID_Generation();
        }
        while (Srv_Is_Unique_UID(user_id) == false);

        User_Login user_login = new User_Login()
        {
            _login = user_login_setting._login,
            _password = user_login_setting._password,
            _mail = user_login_setting._mail,
            _user_id = user_id
        };

        users_logins._users_logins.Add(user_login);

        Srv_Write_Users_Logins(users_logins);

        Srv_Create_New_User_Data_Folder_And_File(user_id);

        Rpc_Login(user_login);
    }

    [Server]
    void Srv_Create_New_User_Data_Folder_And_File(string user_id)
    {
        Directory.CreateDirectory(Application.dataPath + "/Users_Data/user_" + user_id);

        _general_command.Srv_Create_General_Json(user_id);
        _hero_command.Srv_Create_Hero_Json(user_id);
        _rune_command.Srv_Create_Rune_Json(user_id);
    }
}
