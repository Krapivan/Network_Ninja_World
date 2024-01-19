using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class task_pn_sc : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;


    [SerializeField] GameObject task_pn;
    public void Task_Pn_Op_Cls(string v)
    {
        if (v == "op")
        {
            task_pn.SetActive(true);
            Task_Pn_Load();
        }
        else if (v == "cls")
        {
            task_pn.SetActive(false);
        }
    }


    public void Task_Pn_Cls_B()
    {
        Task_Pn_Op_Cls("cls");
    }


    [SerializeField] List<TextMeshProUGUI> _task_name_txt;
    [SerializeField] List<TextMeshProUGUI> _task_score_txt;
    [SerializeField] List<GameObject> _comp_tag;
    [SerializeField] List<Image> _box;
    [SerializeField] List<GameObject> _box_aura;
    [SerializeField] List<Sprite> _box_sprite;
    [SerializeField] List<Sprite> _open_box_sprite;
    public void Task_Pn_Load()
    {
        for (int i = 0; i < _inf_db._database._general_db._task._name.Count; i++)
        {
            _task_name_txt[i].text = _inf_db._database._general_db._task._name[i];
            _task_score_txt[i].text = _inf_db._database._user_general_db._user_general._daily_task._task[i]._score + "|" + _inf_db._database._general_db._task._need_score[i];

            if (_inf_db._database._user_general_db._user_general._daily_task._task[i]._score == _inf_db._database._general_db._task._need_score[i])
            {
                _comp_tag[i].SetActive(true);
            }
            else
            {
                _comp_tag[i].SetActive(false);
            }
        }

        int comp_task_count = _inf_db._database._user_general_db._user_general._daily_task._comp_task_count;
        for (int i = 0; i < 5; i++)
        {
            if ((i + 1) <= comp_task_count && _inf_db._database._user_general_db._user_general._daily_task._daily_box[i]._take == false)
            {
                _box_aura[i].SetActive(true);
            }
            else
            {
                _box_aura[i].SetActive(false);
            }

            if (_inf_db._database._user_general_db._user_general._daily_task._daily_box[i]._take == true)
            {
                _box[i].sprite = _open_box_sprite[i];
            }
            else
            {
                _box[i].sprite = _box_sprite[i];
            }
        }
    }


    public void Reward_Box_B(int box_num)
    {
        if (_inf_db._database._user_general_db._user_general._daily_task._comp_task_count >= box_num && _inf_db._database._user_general_db._user_general._daily_task._daily_box[box_num - 1]._take == false)
        {
            _inf_db._commands._general_command.Cmd_Reward_Box(_inf_db._database._user_general_db._user_general._user_id, box_num);
        }
    }
}
