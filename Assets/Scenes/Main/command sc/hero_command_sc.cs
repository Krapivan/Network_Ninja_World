using UnityEngine;
using Mirror;
using System.IO;
using System.Collections.Generic;

public class hero_command_sc : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] hero_db_sc _hero_db;
    [SerializeField] user_hero_db_sñ _user_hero_db;

    [SerializeField] User_Heroes_js _user_heroes_js;


    //Update Db
    public void Update_Db(int user_id)
    {
        Cmd_Get_User_Heroes(user_id);
        //Cmd_Get_Heroes();
    }


    //Add Hero
    [Command]
    public void Cmd_Add_Hero(int user_id, string hero_name)
    {
        Srv_Add_Hero(user_id, hero_name);
    }
    [Server]
    void Srv_Add_Hero(int user_id, string hero_name)
    {
        _user_heroes_js = new();
        _user_heroes_js = JsonUtility.FromJson<User_Heroes_js>(_inf_db._user_heroes_json.text);

        Hero_Stat hero_stats = _hero_db.Get_Hero_Stat(hero_name);

        User_Hero user_hero = new()
        {
            _name = hero_stats._name,

            _lv = 1,
            _star = 1,
            _ev_star = 0,

            _aa_lv = 1,
            _sk_lv = 1,
            _ul_lv = 1,
            _ps_lv = 1
        };

        for (int i = 0; i < _user_heroes_js._user_heroes_js.Count; i++)
        {
            if (_user_heroes_js._user_heroes_js[i]._user_id == user_id)
            {
                if (_user_heroes_js._user_heroes_js[i]._heroes.Count == 0)
                {
                    _user_heroes_js._user_heroes_js[i]._heroes = new()
                    {
                        user_hero
                    };
                }
            }
        }

        _user_heroes_js = new();
        string json_text = JsonUtility.ToJson(_user_heroes_js);
        File.WriteAllText(Application.dataPath + "/Json/user_heroes_json.json", json_text);

        Srv_Get_User_Heroes(user_id);
    }


    //New User
    [Server]
    public void Srv_New_User_Hero(int user_id)
    {
        _user_heroes_js = JsonUtility.FromJson<User_Heroes_js>(_inf_db._user_heroes_json.text);

        Hero_Stat hero_stats = _hero_db.Get_Hero_Stat("Naruto [Genin]");

        User_Hero user_hero = new()
        {
            _name = hero_stats._name,

            _lv = 1,
            _star = 1,
            _ev_star = 0,

            _aa_lv = 1,
            _sk_lv = 1,
            _ul_lv = 1,
            _ps_lv = 1
        };

        User_Heroes user_heroes = new()
        {
            _user_id = user_id,
            _heroes = new()
            {
                user_hero
            }
        };

        _user_heroes_js._user_heroes_js.Add(user_heroes);

        string json_text = JsonUtility.ToJson(_user_heroes_js);
        File.WriteAllText(Application.dataPath + "/Json/user_heroes_json.json", json_text);

        Srv_Get_User_Heroes(user_id);
    }


    //Hero Db Load
    [Command]
    public void Cmd_Get_Heroes()
    {
        Srv_Get_Heroes();
    }
    [Server]
    void Srv_Get_Heroes()
    {
        Rpc_Get_Heroes(_hero_db._heroes_stat);
    }
    [TargetRpc]
    void Rpc_Get_Heroes(List<Hero_Stat> heroes_stats)
    {
        _hero_db._heroes_stat = heroes_stats;
    }


    //Get User Heroes
    [Command]
    public void Cmd_Get_User_Heroes(int user_id)
    {
        Srv_Get_User_Heroes(user_id);
    }
    [Server]
    void Srv_Get_User_Heroes(int user_id)
    {
        _user_heroes_js = JsonUtility.FromJson<User_Heroes_js>(_inf_db._user_heroes_json.text);

        for (int i = 0; i < _user_heroes_js._user_heroes_js.Count; i++)
        {
            if (_user_heroes_js._user_heroes_js[i]._user_id == user_id)
            {
                Rpc_Get_User_Heroes(_user_heroes_js._user_heroes_js[i]);
            }
        }
    }
    [TargetRpc]
    void Rpc_Get_User_Heroes(User_Heroes user_heroes)
    {
        _user_hero_db._heroes = user_heroes._heroes;
    }
}
