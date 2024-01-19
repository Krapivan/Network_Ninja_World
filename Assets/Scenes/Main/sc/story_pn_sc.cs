using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class story_pn_sc : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;


    [SerializeField] GameObject _story_pn;
    public void Story_Pn_Op_Cls(string v)
    {
        if (v == "op")
        {
            _story_pn.SetActive(true);
            Story_Pn_Load();
        }
        else if (v == "cls")
        {
            _story_pn.SetActive(false);
        }
    }


    [SerializeField] TextMeshProUGUI _chapter_name;
    [SerializeField] List<GameObject> _part_point;
    void Story_Pn_Load()
    {
        int chosen_chapter_num = _inf_db._database._user_story_db._chosen_chapter_num;
        if (chosen_chapter_num == 0)
        {
            _inf_db._database._user_story_db._chosen_chapter_num = 1;
            chosen_chapter_num = _inf_db._database._user_story_db._chosen_chapter_num;
        }
        _chapter_name.text = _inf_db._database._story_db._chapter_name[chosen_chapter_num - 1];

        Part_Point_Load();
    }
    void Part_Point_Load()
    {
        int chosen_chapter_num = _inf_db._database._user_story_db._chosen_chapter_num;
        int part_num = 0;

        for (int i = 0; i < _inf_db._database._story_db._chapter_parts[chosen_chapter_num - 1]._part_name.Count; i++)
        {
            part_num = i + 1;
            if (_inf_db._managers._story_manager.Is_Open_Part(_inf_db._database._user_story_db._chapter_parts, chosen_chapter_num, part_num) == true)
            {
                _part_point[part_num - 1].GetComponent<Image>().sprite = _inf_db._managers._story_manager.Part_Point_Art(chosen_chapter_num, part_num);
                _part_point[part_num - 1].GetComponent<Button>().interactable = true;
            }
        }
    }

    [SerializeField] Transform _heroes_cont;
    [SerializeField] GameObject _hero_icon;
    void Heroes_Pn_Load()
    {
        Heroes_Pn_Clear();
        for (int i = 0; i < _inf_db._database._user_hero_db._heroes.Count; i++)
        {
            Instantiate(_hero_icon, _heroes_cont).GetComponent<hero_icon_character_pn_sc>().Spawn_Setting(_inf_db._database._user_hero_db._heroes[i], "story_pn");
        }
    }
    void Heroes_Pn_Clear()
    {
        for (int i = 0; i < _heroes_cont.childCount; i++ )
        {
            Destroy(_heroes_cont.GetChild(i).gameObject);
        }
    }
}
