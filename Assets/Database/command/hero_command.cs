using UnityEngine;
using Mirror;
using System.IO;
using System.Collections.Generic;

public class hero_command : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_hero_db_sñ _user_hero_db;


    //Read & Write User Hero Db
    [Server]
    public User_Heroes Srv_Read_User_Heroes(string user_id)
    {
        try
        {
            User_Heroes user_heroes = JsonUtility.FromJson<User_Heroes>(File.ReadAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/hero_" + user_id + ".json"));
            if (user_heroes._user_id == user_id)
            {
                return user_heroes;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    [Server]
    public void Srv_Write_User_Heroes(string user_id, User_Heroes user_heroes)
    {
        try
        {
            User_Heroes last_user_heroes = Srv_Read_User_Heroes(user_id);
            if (user_heroes._user_id == user_id)
            {
                string json_text = JsonUtility.ToJson(user_heroes);
                File.WriteAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/hero_" + user_id + ".json", json_text);
            }
        }
        catch
        {
        }
    }


    //Add Hero
    [Command]
    public void Cmd_Add_Hero(string user_id, string hero_name)
    {
        Srv_Add_Hero(user_id, hero_name);
    }
    [Server]
    void Srv_Add_Hero(string user_id, string hero_name)
    {
        User_Heroes user_heroes = Srv_Read_User_Heroes(user_id);

        Hero hero = _inf_db._managers._hero_manager.Create_Hero(hero_name);

        user_heroes._heroes.Add(hero);

        Srv_Write_User_Heroes(user_id, user_heroes);
    }


    //New User
    [Server]
    public void Srv_Create_Hero_Json(string user_id)
    {
        File.Create(Application.dataPath + "/Users_Data/user_" + user_id + "/hero_" + user_id + ".json").Dispose();

        Hero hero = _inf_db._managers._hero_manager.Create_Hero("Naruto [Genin]");

        User_Heroes _new_user_heroes = new();
        _new_user_heroes._user_id = user_id;
        _new_user_heroes._heroes.Add(hero);

        string json_text = JsonUtility.ToJson(_new_user_heroes);
        File.WriteAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/hero_" + user_id + ".json", json_text);
    }


    //Hero Db Load
    [Command]
    public void Cmd_Hero_Db_Load()
    {
        Srv_Hero_Db_Load();
    }
    [Server]
    void Srv_Hero_Db_Load()
    {
        Rpc_Hero_Db_Load(_inf_db._database._hero_db._heroes_stat);
    }
    [TargetRpc]
    void Rpc_Hero_Db_Load(List<Hero_Stat> heroes_stats)
    {
        _inf_db._database._hero_db._heroes_stat = heroes_stats;
    }


    //Get User Heroes
    [Command]
    public void Cmd_User_Hero_Db_Load(string user_id)
    {
        Srv_User_Hero_Db_Load(user_id);
    }
    [Server]
    void Srv_User_Hero_Db_Load(string user_id)
    {
        User_Heroes user_heroes = Srv_Read_User_Heroes(user_id);
        Rpc_User_Hero_Db_Load(user_heroes);
    }
    [TargetRpc]
    void Rpc_User_Hero_Db_Load(User_Heroes user_heroes)
    {
        _user_hero_db._heroes = user_heroes._heroes;
    }


    //Lv Up Skill
    [Command]
    public void Cmd_Lv_Up_Hero_Skill(string user_id, string hero_name, string sk_type)
    {
        Srv_Lv_Up_Hero_Skill(user_id, hero_name, sk_type);
    }
    [Server]
    void Srv_Lv_Up_Hero_Skill(string user_id, string hero_name, string sk_type)
    {
        User_Heroes user_heroes = Srv_Read_User_Heroes(user_id);
        Hero hero = _inf_db._managers._hero_manager.Get_Hero(user_heroes._heroes, hero_name);
        int every_lv_cost = _inf_db._database._hero_db._hero_grow_stat._sk_evry_lv_up_fragment_cost;

        switch (sk_type)
        {
            case "aa":
                if (hero._fragments >= every_lv_cost * (hero._aa_lv + 1) && hero._aa_lv < 5)
                {
                    hero._fragments = hero._fragments - every_lv_cost * (hero._aa_lv + 1);
                    hero._aa_lv += 1;
                }
                break;
            case "sk":
                if (hero._fragments >= every_lv_cost * (hero._sk_lv + 1) && hero._sk_lv < 5)
                {
                    hero._fragments = hero._fragments - every_lv_cost * (hero._sk_lv + 1);
                    hero._sk_lv += 1;
                }
                break;
            case "ul":
                if (hero._fragments >= every_lv_cost * (hero._ul_lv + 1) && hero._ul_lv < 5)
                {
                    hero._fragments = hero._fragments - every_lv_cost * (hero._ul_lv + 1);
                    hero._ul_lv += 1;
                }
                break;
            case "ps":
                if (hero._fragments >= every_lv_cost * (hero._ps_lv + 1) && hero._ps_lv < 5)
                {
                    hero._fragments = hero._fragments - every_lv_cost * (hero._ps_lv + 1);
                    hero._ps_lv += 1;
                }
                break;
        }

        user_heroes._heroes = _inf_db._managers._hero_manager.Update_Hero_From_List(hero, user_heroes._heroes);

        Srv_Write_User_Heroes(user_id, user_heroes);

        Srv_Update_Sk_Inf_Pn(user_id);
    }

    [Server]
    void Srv_Update_Sk_Inf_Pn(string user_id)
    {
        User_Heroes user_heroes = Srv_Read_User_Heroes(user_id);
        Rpc_Update_Sk_Inf_Pn(user_heroes);
    }
    [TargetRpc]
    void Rpc_Update_Sk_Inf_Pn(User_Heroes user_heroes)
    {
        _inf_db._database._user_hero_db._heroes = user_heroes._heroes;
        character_pn_sc character_pn_sc = GameObject.Find("character_pn_sc").GetComponent<character_pn_sc>();
        character_pn_sc.Sk_Inf_Pn_Load(character_pn_sc.Get_Chosen_Sk_Type());
    }


    //Hero Lv Up
    [Command]
    public void Cmd_Lv_Up_Hero(string user_id, string hero_name)
    {
        Srv_Lv_Up_Hero(user_id, hero_name);
    }
    [Server]
    void Srv_Lv_Up_Hero(string user_id, string hero_name)
    {
        User_Heroes user_heroes = Srv_Read_User_Heroes(user_id);
        Hero hero = _inf_db._managers._hero_manager.Get_Hero(user_heroes._heroes, hero_name);

        if (hero != null)
        {
            hero = _inf_db._managers._hero_manager.Lv_Up_Hero(hero);

            user_heroes._heroes = _inf_db._managers._hero_manager.Update_Hero_From_List(hero, user_heroes._heroes);

            Srv_Write_User_Heroes(user_id, user_heroes);


        }
    }
}
