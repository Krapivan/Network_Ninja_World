using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class hero_icon_character_pn_sc : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;

    [SerializeField] string _where_use;

    [SerializeField] Hero _hero;
    [SerializeField] Hero_Stat _hero_stat;
    [SerializeField] Hero_Sprite _hero_sprite;

    [SerializeField] string _hero_name;


    public void Spawn_Setting(Hero hero, string where_use)
    {
        _where_use = where_use;

        _hero = hero;
        _hero_stat = _inf_db._managers._hero_manager.Get_Hero_Stat(hero._name);
        _hero_sprite = _inf_db._managers._hero_manager.Get_Hero_Sprite(hero._name);

        Art_Backr_Load();
        Star_Load();
        Lv_Load();
    }

    [SerializeField] Image _icon, _back;
    void Art_Backr_Load()
    {
        string rare = _hero_stat._rare;
        _icon.sprite = _hero_sprite._icon;
        _back.sprite = _inf_db._managers._hero_manager.Get_Rare_Back_Sprite(rare);
    }

    [SerializeField] Sprite _ev_star;
    [SerializeField] List<Image> _stars;
    void Star_Load()
    {
        int star = _hero._star;
        int ev_star = _hero._ev_star;

        for (int i = 0; i < star; i++)
        {
            _stars[i].gameObject.SetActive(true);
        }

        if (star == 5 && ev_star != 0)
        {
            for (int i = 0; i < ev_star; i++)
            {
                _stars[i].sprite = _ev_star;
            }
        }
    }

    [SerializeField] TextMeshProUGUI _lv_txt;
    void Lv_Load()
    {
        _lv_txt.text = _hero._lv + "";
    }

    public void Hero_Icon_B()
    {
        if (_where_use == "character_pn_heroes_pn")
        {
            character_pn_sc character_pn_sc = GameObject.Find("character_pn_sc").GetComponent<character_pn_sc>();
            if (_hero._name != character_pn_sc.Get_Chosen_Hero_Name())
            {
                character_pn_sc.Character_Pn_Load(_hero._name);
            }
        }
    }
}
