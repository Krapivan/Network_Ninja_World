using UnityEngine;
using TMPro;

public class main_pn_sc : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_general_db_sc _user_resource_db;


    private void FixedUpdate()
    {
        Resource_Pn_Load();
    }


    public void Add_Rune()
    {
        Rune_Create_Setting rune_create_setting = new()
        {
            _set = "energy",
            _slot = 1,
            _rare = "rare",
            _star = 2
        };

        _inf_db._commands._rune_command.Cmd_Add_Rune(_user_resource_db._user_general._user_id, rune_create_setting);
        _inf_db._commands._rune_command.Cmd_User_Rune_Db_Load(_user_resource_db._user_general._user_id);
    }


    [SerializeField] TextMeshProUGUI _money_txt, _gold_txt, _coin_txt, _energy_txt;
    void Resource_Pn_Load()
    {
        _money_txt.text = _user_resource_db._user_general._resource._money + "";
        _gold_txt.text = _user_resource_db._user_general._resource._gold + "";
        _coin_txt.text = _user_resource_db._user_general._resource._coin + "";
        _energy_txt.text = _user_resource_db._user_general._resource._energy_now + " | " + _user_resource_db._user_general._resource._energy_mx;
    }


    [SerializeField] character_pn_sc _character_pn_sc;
    [SerializeField] story_pn_sc _story_pn_sc;
    [SerializeField] task_pn_sc _task_pn_sc;
    public void Main_B(string pn_name)
    {
        switch (pn_name)
        {
            case "character": _character_pn_sc.Character_Pn_Op_Cls("op"); break;
            case "story": _story_pn_sc.Story_Pn_Op_Cls("op"); break;
            case "arena": _character_pn_sc.Character_Pn_Op_Cls("op"); break;
            case "dungeon": _character_pn_sc.Character_Pn_Op_Cls("op"); break;
            case "summon": _character_pn_sc.Character_Pn_Op_Cls("op"); break;
            case "shop": _character_pn_sc.Character_Pn_Op_Cls("op"); break;
            case "task": _task_pn_sc.Task_Pn_Op_Cls("op"); break;
        }
    }
}
