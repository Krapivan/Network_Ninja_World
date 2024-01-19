using UnityEngine;
using Mirror;
using System.IO;

public class rune_command : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_rune_db_sc _user_rune_db;


    //Read & Write User Runes
    [Server]
    User_Runes Srv_Read_User_Runes(string user_id)
    {
        try
        {
            User_Runes user_runes = JsonUtility.FromJson<User_Runes>(File.ReadAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/rune_" + user_id + ".json"));
            if (user_runes._user_id == user_id)
            {
                return user_runes;
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
    void Srv_Write_User_Runes(string user_id, User_Runes user_runes)
    {
        try
        {
            User_Runes last_user_runes = Srv_Read_User_Runes(user_id);
            if (last_user_runes._user_id == user_id && user_runes._user_id == user_id)
            {
                string json_text = JsonUtility.ToJson(user_runes);
                File.WriteAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/rune_" + user_id + ".json", json_text);
            }
        }
        catch
        {
        }
    }


    //New User
    [Server]
    public void Srv_Create_Rune_Json(string user_id)
    {
        File.Create(Application.dataPath + "/Users_Data/user_" + user_id + "/rune_" + user_id + ".json").Dispose();

        User_Runes _new_user_runes = new();
        _new_user_runes._user_id = user_id;

        string json_text = JsonUtility.ToJson(_new_user_runes);
        File.WriteAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/rune_" + user_id + ".json", json_text);
    }


    //Insert Rune
    [Server]
    void Srv_Insert_Rune(string user_id, Rune rune)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);

        if (user_runes != null && rune != null)
        {
            user_runes._runes.Add(rune);
            Srv_Write_User_Runes(user_id, user_runes);
        }
    }


    //Add Rune
    [Command]
    public void Cmd_Add_Rune(string user_id, Rune_Create_Setting rune_create_setting)
    {
        Srv_Add_Rune(user_id,  rune_create_setting);
    }
    [Server]
    void Srv_Add_Rune(string user_id, Rune_Create_Setting rune_create_setting)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        Rune rune = _inf_db._managers._rune_manager.Create_Rune(rune_create_setting, user_runes._runes);
        Srv_Insert_Rune(user_id, rune);
    }


    //User Rune Db Load
    [Command]
    public void Cmd_User_Rune_Db_Load(string user_id)
    {
        Srv_User_Rune_Db_Load(user_id);
    }
    [Server]
    void Srv_User_Rune_Db_Load(string user_id)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        Rpc_User_Rune_Db_Load(user_runes);
    }
    [TargetRpc]
    void Rpc_User_Rune_Db_Load(User_Runes user_runes)
    {
        _user_rune_db._runes = user_runes._runes;
    }


    //Equip Rune
    [Command]
    public void Cmd_Equip_Rune_To_Hero(string user_id, int rune_id, string hero_name)
    {
        Srv_Equip_Rune_To_Hero(user_id, rune_id, hero_name);
    }
    [Server]
    void Srv_Equip_Rune_To_Hero(string user_id, int rune_id, string hero_name)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        Rune rune = _inf_db._managers._rune_manager.Get_Rune(user_runes._runes, rune_id);

        User_Heroes user_heroes = _inf_db._commands._hero_command.Srv_Read_User_Heroes(user_id);
        Hero hero = _inf_db._managers._hero_manager.Get_Hero(user_heroes._heroes, hero_name);

        if (hero != null && rune != null)
        {
            if (rune._owner == "no")
            {
                if (_inf_db._managers._hero_manager.Have_hero_Rune_From_Slot(hero, rune._slot) == true)
                {
                    int past_rune_id = _inf_db._managers._hero_manager.Hero_Rune_Id_By_Slot(hero, rune._slot);
                    Rune past_rune = _inf_db._managers._rune_manager.Get_Rune(user_runes._runes, past_rune_id);
                    past_rune._owner = "no";
                    switch (past_rune._slot)
                    {
                        case 1: hero._rune_1_id = 0; break;
                        case 2: hero._rune_2_id = 0; break;
                        case 3: hero._rune_3_id = 0; break;
                        case 4: hero._rune_4_id = 0; break;
                        case 5: hero._rune_5_id = 0; break;
                        case 6: hero._rune_6_id = 0; break;
                    }
                }

                rune._owner = hero._name;
                switch (rune._slot)
                {
                    case 1: hero._rune_1_id = rune._rune_id; break;
                    case 2: hero._rune_2_id = rune._rune_id; break;
                    case 3: hero._rune_3_id = rune._rune_id; break;
                    case 4: hero._rune_4_id = rune._rune_id; break;
                    case 5: hero._rune_5_id = rune._rune_id; break;
                    case 6: hero._rune_6_id = rune._rune_id; break;
                }

                user_runes._runes = _inf_db._managers._rune_manager.Update_Rune_From_List(rune, user_runes._runes);
                user_heroes._heroes = _inf_db._managers._hero_manager.Update_Hero_From_List(hero, user_heroes._heroes);

                Srv_Write_User_Runes(user_id, user_runes);
                _inf_db._commands._hero_command.Srv_Write_User_Heroes(user_id, user_heroes);
            }

            Srv_Update_Rune_After_Remove_Equip(user_id);
        }
    }


    //Remove Rune
    [Command]
    public void Cmd_Remove_Rune_From_Hero(string user_id, int rune_id)
    {
        Srv_Remove_Rune_From_Hero(user_id, rune_id);
    }
    [Server]
    void Srv_Remove_Rune_From_Hero(string user_id, int rune_id)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        Rune rune = _inf_db._managers._rune_manager.Get_Rune(user_runes._runes, rune_id);

        if (rune._owner != "no")
        {
            string hero_name = rune._owner;
            User_Heroes user_heroes = _inf_db._commands._hero_command.Srv_Read_User_Heroes(user_id);
            Hero hero = _inf_db._managers._hero_manager.Get_Hero(user_heroes._heroes, hero_name);

            if (hero != null && rune != null)
            {
                switch (rune._slot)
                {
                    case 1: hero._rune_1_id = 0; break;
                    case 2: hero._rune_2_id = 0; break;
                    case 3: hero._rune_3_id = 0; break;
                    case 4: hero._rune_4_id = 0; break;
                    case 5: hero._rune_5_id = 0; break;
                    case 6: hero._rune_6_id = 0; break;
                }
                rune._owner = "no";

                user_runes._runes = _inf_db._managers._rune_manager.Update_Rune_From_List(rune, user_runes._runes);
                user_heroes._heroes = _inf_db._managers._hero_manager.Update_Hero_From_List(hero, user_heroes._heroes);

                Srv_Write_User_Runes(user_id, user_runes);
                _inf_db._commands._hero_command.Srv_Write_User_Heroes(user_id, user_heroes);

                Srv_Update_Rune_After_Remove_Equip(user_id);
            }
        }
    }


    //Lv Up Rune
    [Command]
    public void Cmd_Lv_Up_Rune(string user_id, int rune_id)
    {
        Srv_Lv_Up_Rune(user_id, rune_id);
    }
    [Server]
    void Srv_Lv_Up_Rune(string user_id, int rune_id)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        Rune rune = _inf_db._managers._rune_manager.Get_Rune(user_runes._runes, rune_id);
        User_General user_general = _inf_db._commands._general_command.Srv_Read_User_General(user_id);

        if (rune != null)
        {
            if (rune._lv < 15)
            {
                int cost = _inf_db._managers._rune_manager.Rune_Lv_Up_Cost(rune._star, rune._lv);
                if (user_general._resource._coin > cost)
                {
                    user_general._resource._coin -= cost;
                    rune = _inf_db._managers._rune_manager.Rune_Lv_Up(rune);
                    user_runes._runes = _inf_db._managers._rune_manager.Update_Rune_From_List(rune, user_runes._runes);

                    Srv_Write_User_Runes(user_id, user_runes);
                    _inf_db._commands._general_command.Srv_Write_User_General(user_id, user_general);

                    Srv_Update_Rune_After_Lv_Up(user_id);
                }
            }
        }
    }


    //Sell Rune
    [Command]
    public void Cmd_Sell_Rune(string user_id, int rune_id)
    {
        Srv_Sell_Rune(user_id, rune_id);
    }
    [Server]
    void Srv_Sell_Rune(string user_id, int rune_id)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        Rune rune = _inf_db._managers._rune_manager.Get_Rune(user_runes._runes, rune_id);

        User_Heroes user_heroes = _inf_db._commands._hero_command.Srv_Read_User_Heroes(user_id);

        User_General user_general = _inf_db._commands._general_command.Srv_Read_User_General(user_id);

        if (rune != null)
        {
            int cost = _inf_db._managers._rune_manager.Rune_Sell_Cost(rune._star, rune._lv);
            user_general._resource._coin += cost;

            string owner = rune._owner;

            Runes_Heroes_Return heroes_runes = _inf_db._managers._rune_manager.Dell_Rune_From_List(rune, user_runes._runes, user_heroes._heroes);

            user_runes._runes = heroes_runes._runes;
            user_heroes._heroes = heroes_runes._heroes;

            Srv_Write_User_Runes(user_id, user_runes);
            _inf_db._commands._hero_command.Srv_Write_User_Heroes(user_id, user_heroes);
            _inf_db._commands._general_command.Srv_Write_User_General(user_id, user_general);

            Srv_Update_Rune_Inf_Pn_After_Sell(user_id, owner);
        }
    }


    //Update Rune After Action
    [Server]
    void Srv_Update_Rune_After_Remove_Equip(string user_id)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        User_Heroes user_heroes = _inf_db._commands._hero_command.Srv_Read_User_Heroes(user_id);

        Rpc_Update_Rune_After_Remove_Equip(user_runes, user_heroes);
    }
    [TargetRpc]
    void Rpc_Update_Rune_After_Remove_Equip(User_Runes user_runes, User_Heroes user_heroes)
    {
        _inf_db._database._user_rune_db._runes = user_runes._runes;
        _inf_db._database._user_hero_db._heroes = user_heroes._heroes;

        GameObject.Find("character_pn_sc").GetComponent<character_pn_sc>().Update_After_Rune_Remove_Equip();
    }

    [Server]
    void Srv_Update_Rune_After_Lv_Up(string user_id)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        User_General user_general = _inf_db._commands._general_command.Srv_Read_User_General(user_id);

        Rpc_Update_Rune_After_Lv_Up(user_runes, user_general);
    }
    [TargetRpc]
    void Rpc_Update_Rune_After_Lv_Up(User_Runes user_runes, User_General user_general)
    {
        _inf_db._database._user_general_db._user_general._resource._coin = user_general._resource._coin;
        _inf_db._database._user_rune_db._runes = user_runes._runes;

        GameObject.Find("character_pn_sc").GetComponent<character_pn_sc>().Update_After_Rune_Lv_Up();
    }


    [Server]
    void Srv_Update_Rune_Inf_Pn_After_Sell(string user_id, string owner)
    {
        User_Runes user_runes = Srv_Read_User_Runes(user_id);
        User_Heroes user_heroes = _inf_db._commands._hero_command.Srv_Read_User_Heroes(user_id);
        User_General user_general = _inf_db._commands._general_command.Srv_Read_User_General(user_id);

        Rpc_Update_Rune_Inf_Pn_After_Sell(user_runes, user_heroes, user_general, owner);
    }
    [TargetRpc]
    void Rpc_Update_Rune_Inf_Pn_After_Sell(User_Runes user_runes, User_Heroes user_heroes, User_General user_general, string owner)
    {
        _inf_db._database._user_general_db._user_general._resource._coin = user_general._resource._coin;
        _inf_db._database._user_hero_db._heroes = user_heroes._heroes;
        _inf_db._database._user_rune_db._runes = user_runes._runes;

        GameObject.Find("character_pn_sc").GetComponent<character_pn_sc>().Update_After_Rune_Sell(owner);
    }
}
