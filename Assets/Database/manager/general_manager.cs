using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class general_manager : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] general_db_sc _general_db;


    public task Get_Task()
    {
        return _general_db._task;
    }


    public User_Daily_Task Add_Task_Score(User_Daily_Task user_task, string task_name)
    {
        for (int i = 0; i < user_task._task.Count; i++)
        {
            if (user_task._task[i]._name == task_name)
            {
                if (user_task._task[i]._score < _general_db._task._need_score[i])
                {
                    user_task._task[i]._score++;
                    if (user_task._task[i]._score == _general_db._task._need_score[i])
                    {
                        user_task._comp_task_count++;
                    }
                    break;
                }
            }
        }

        return user_task;
    }


    public User_Resource Add_Resource(User_Resource user_resource, string resource_name, int resource_count)
    {
        switch (resource_name)
        {
            case "money": user_resource._money += resource_count; break;
            case "gold": user_resource._gold += resource_count; break;
            case "coin": user_resource._coin += resource_count; break;
            case "energy": user_resource._energy_now += resource_count; break;

            case "ramen": user_resource._item._ramen += resource_count; break;
            case "chakra": user_resource._item._chakra += resource_count; break;
            case "auto ticket": user_resource._item._auto_ticket += resource_count; break;
            case "scroll": user_resource._item._scroll += resource_count; break;

            case "element mark fire":
                user_resource._item._fire_mark += resource_count;
                 break;
            case "element mark wind":
                user_resource._item._wind_mark += resource_count;
                break;
            case "element mark lightning":
                user_resource._item._lightnig_mark += resource_count;
                break;
            case "element mark earth":
                user_resource._item._earth_mark += resource_count;
                break;
            case "element mark water":
                user_resource._item._water_mark += resource_count;
                break;
            case "element mark":
                for (int i = 1; i <= resource_count; i++)
                {
                    List<string> elements = new() {"fire", "wind", "lightning", "earth", "water"};
                    string element_name = elements[Random.Range(0, 5)];
                    switch (element_name)
                    {
                        case "fire":
                            user_resource._item._fire_mark += resource_count;
                            break;
                        case "wind":
                            user_resource._item._wind_mark += resource_count;
                            break;
                        case "lightning":
                            user_resource._item._lightnig_mark += resource_count;
                            break;
                        case "earth":
                            user_resource._item._earth_mark += resource_count;
                            break;
                        case "water":
                            user_resource._item._water_mark += resource_count;
                            break;
                    }
                }
                break;
        }

        return user_resource;
    }


    public Sprite Get_Ef_Sprite(string ef_name, int ef_lv)
    {
        for (int i = 0; i < _general_db._effect._name.Count; i++)
        {
            if (_general_db._effect._name[i] == ef_name && _general_db._effect._lv[i] == ef_lv)
            {
                return _general_db._effect._sprite[i];
            }
        }

        return null;
    }
}
