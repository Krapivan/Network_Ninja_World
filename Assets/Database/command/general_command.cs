using Mirror;
using System;
using System.IO;
using UnityEngine;

public class general_command : NetworkBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_general_db_sc _user_general_db;


    //Read & Write User General
    [Server]
    public User_General Srv_Read_User_General(string user_id)
    {
        try
        {
            User_General user_general = JsonUtility.FromJson<User_General>(File.ReadAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/general_" + user_id + ".json"));
            if (user_general._user_id == user_id)
            {
                return user_general;
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
    public void Srv_Write_User_General(string user_id, User_General user_general)
    {
        try
        {
            User_General last_user_general = Srv_Read_User_General(user_id);
            if (last_user_general._user_id == user_id && user_general._user_id == user_id)
            {
                string json_text = JsonUtility.ToJson(user_general);
                File.WriteAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/general_" + user_id + ".json", json_text);
            }
        }
        catch
        {
        }
    }


    [Server]
    public void Srv_Check_Data(string user_id)
    {
        
    }


    //New User
    [Server]
    public void Srv_Create_General_Json(string user_id)
    {
        File.Create(Application.dataPath + "/Users_Data/user_" + user_id + "/general_" + user_id + ".json").Dispose();


        User_General new_user_general = new();

        new_user_general._user_id = user_id;
        new_user_general._user_name = "player_" + UnityEngine.Random.Range(1, 1000);
        new_user_general._last_login_date = DateTime.Now.ToShortDateString();

        new_user_general._resource = _inf_db._database._general_db._new_user_general._resource;
        new_user_general._daily_task = _inf_db._database._general_db._new_user_general._daily_task;


        string json_text = JsonUtility.ToJson(new_user_general);
        File.WriteAllText(Application.dataPath + "/Users_Data/user_" + user_id + "/general_" + user_id + ".json", json_text);
    }


    //User General Db Load
    [Command]
    public void Cmd_User_General_Db_Load(string user_id)
    {
        Srv_User_General_Db_Load(user_id);
    }
    [Server]
    void Srv_User_General_Db_Load(string user_id)
    {
        Srv_Check_Data(user_id);
        User_General user_general = Srv_Read_User_General(user_id);
        Rpc_User_General_Db_Load(user_general);
    }
    [TargetRpc]
    void Rpc_User_General_Db_Load(User_General user_ganeral)
    {
        _user_general_db._user_general._user_id = user_ganeral._user_id;
        _user_general_db._user_general._user_name = user_ganeral._user_name;
        _user_general_db._user_general._last_login_date = user_ganeral._last_login_date;

        _user_general_db._user_general._resource = user_ganeral._resource;

        _user_general_db._user_general._daily_task = user_ganeral._daily_task;
    }


    //Daily Task
    [Command]
    public void Cmd_Reward_Box(string user_id, int box_num)
    {
        Srv_Reward_Box(user_id, box_num);
    }
    [Server]
    void Srv_Reward_Box(string user_id, int box_num)
    {
        User_General user_general = Srv_Read_User_General(user_id);
        task task = _inf_db._managers._general_manager.Get_Task();

        int comp_task_count = user_general._daily_task._comp_task_count;

        if (comp_task_count >= box_num && user_general._daily_task._daily_box[box_num - 1]._take == false)
        {
            for (int i = 0; i < task._box_reward[box_num - 1]._item_name.Count; i++)
            {
                user_general._resource = _inf_db._managers._general_manager.Add_Resource(user_general._resource, task._box_reward[box_num - 1]._item_name[i], task._box_reward[box_num - 1]._item_count[i]);
            }

            user_general._daily_task._daily_box[box_num - 1]._take = true;

            Srv_Write_User_General(user_id, user_general);

            Srv_Task_Pn_Load_After_Reward_Box(user_id);
        }
    }
    [Server]
    void Srv_Task_Pn_Load_After_Reward_Box(string user_id)
    {
        User_General user_general = Srv_Read_User_General(user_id);
        Rpc_Task_Pn_Load_After_Reward_Box(user_general);
    }
    [ClientRpc]
    void Rpc_Task_Pn_Load_After_Reward_Box(User_General user_general)
    {
        _user_general_db._user_general._resource = user_general._resource;
        _user_general_db._user_general._daily_task = user_general._daily_task;

        GameObject.Find("task_pn_sc").GetComponent<task_pn_sc>().Task_Pn_Load();
    }


    [Server]
    public void Srv_Add_Task_Score(string user_id, string task_name)
    {
        User_General user_general = Srv_Read_User_General(user_id);

        user_general._daily_task = _inf_db._managers._general_manager.Add_Task_Score(user_general._daily_task, task_name);

        Srv_Write_User_General(user_id, user_general);

        Srv_User_General_Db_Load(user_id);
    }
}
