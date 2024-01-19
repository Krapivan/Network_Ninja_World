using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class character_pn_sc : MonoBehaviour
{
    public inf_db_sc _inf_db;


    [SerializeField] GameObject _character_pn;
    public void Character_Pn_Op_Cls(string v)
    {
        if (v == "op")
        {
            _character_pn.SetActive(true);
            Character_Pn_Load("no");
        }
        else if (v == "cls")
        {
            _character_pn.SetActive(false);
        }
    }
    public void Character_Pn_Load(string hero_name)
    {
        if (_chosen_hero_name.Length <= 2 && hero_name == "no")
        {
            _chosen_hero_name = _inf_db._database._user_hero_db._heroes[0]._name;
        }
        else if (hero_name != "no")
        {
            _chosen_hero_name = hero_name;
        }

        _stat_pn.SetActive(true);
        _rune_pn.SetActive(false);
        _sk_pn.SetActive(false);

        Heroes_Pn_Load();
        Load_Hero();
        Stat_Pn_Load();
    }


    [SerializeField] GameObject _stat_pn, _rune_pn, _sk_pn;
    public void Inf_Pn_B(string pn_name)
    {
        _stat_pn.SetActive(false);
        _rune_pn.SetActive(false);
        _sk_pn.SetActive(false);

        if (pn_name == "stat")
        {
            _stat_pn.SetActive(true);
            Stat_Pn_Load();
        }
        else if (pn_name == "rune")
        {
            _rune_pn.SetActive(true);
            _rune_inf_pn.SetActive(false);
            Rune_Pn_Load();
        }
        else if (pn_name == "skill")
        {
            _sk_pn.SetActive(true);
            _sk_inf_pn.SetActive(false);
            Sk_Pn_Load();
        }
    }



    #region Hero

    [SerializeField] Transform _heroes_pn_cont;
    [SerializeField] GameObject _hero_icon_character_pn;
    void Heroes_Pn_Load()
    {
        Heroes_Pn_Clear();
        for (int i = 0; i < _inf_db._database._user_hero_db._heroes.Count; i++)
        {
            Instantiate(_hero_icon_character_pn, _heroes_pn_cont).GetComponent<hero_icon_character_pn_sc>().Spawn_Setting(_inf_db._database._user_hero_db._heroes[i], "character_pn_heroes_pn");
        }
    }
    void Heroes_Pn_Clear()
    {
        for (int i = 0; i < _heroes_pn_cont.childCount; i++)
        {
            Destroy(_heroes_pn_cont.GetChild(i).gameObject);
        }
    }


    [SerializeField] string _chosen_hero_name;
    [SerializeField] Image _hero_art, _hero_rare;
    [SerializeField] TextMeshProUGUI _hero_name;
    void Load_Hero()
    {
        Hero_Sprite hero_sprite = _inf_db._managers._hero_manager.Get_Hero_Sprite(_chosen_hero_name);
        Hero_Stat hero_stat = _inf_db._managers._hero_manager.Get_Hero_Stat(_chosen_hero_name);

        _hero_art.sprite = hero_sprite._art;
        _hero_rare.sprite = _inf_db._managers._hero_manager.Get_Rare_Art(hero_stat._rare);
        _hero_name.text = hero_stat._name;
    }
    public string Get_Chosen_Hero_Name()
    {
        return _chosen_hero_name;
    }


    [SerializeField] TextMeshProUGUI _hero_lv, _hero_exp, _hero_stat;
    void Stat_Pn_Load()
    {
        Hero hero = _inf_db._managers._hero_manager.Get_Hero(_inf_db._database._user_hero_db._heroes, _chosen_hero_name);
        Hero_Stat hero_stat_with_runes = _inf_db._managers._hero_manager.Hero_Stat_With_Runes(_inf_db._database._user_rune_db._runes, hero);

        _hero_lv.text = "lv: " + hero._lv;
        _hero_exp.text = "exp: " + hero._exp + " | " + hero._exp_need;
        _hero_stat.text = "health : " + hero_stat_with_runes._health + "\n" +
            "attack : " + hero_stat_with_runes._attack + "\n" +
            "defense : " + hero_stat_with_runes._defense + "\n" +
            "speed : " + hero_stat_with_runes._speed + "\n" +
            "crit chance : " + hero_stat_with_runes._crit_chance + "\n" +
            "crit damage : " + hero_stat_with_runes._crit_damage + "\n" +
            "accuracy : " + hero_stat_with_runes._accuracy + "\n" +
            "resistance : " + hero_stat_with_runes._resistance + "\n" +
            "pierce : " + hero_stat_with_runes._pierce + "\n" +
            "armor : " + hero_stat_with_runes._armor + "\n" +
            "lifesteal : " + hero_stat_with_runes._lifesteal + "\n" +
            "regeneration : " + hero_stat_with_runes._regeneration + "\n" +
            "recovering power : " + hero_stat_with_runes._recovering_power + "\n" +
            "healing power : " + hero_stat_with_runes._healing_power + "\n" +
            "element damage : " + hero_stat_with_runes._element_damage + "\n" +
            "element resistance : " + hero_stat_with_runes._element_resistance + "\n" +
            "crit resistance : " + hero_stat_with_runes._crit_resistance + "\n" +
            "crit defense : " + hero_stat_with_runes._crit_defense;
    }

    #endregion



    #region Rune

    void Rune_Pn_Load()
    {
        Hero_Runes_Pn_Load();
        Hero_Runes_Stat_Load();
        Runes_Pn_Load("no owner", 0);
    }

    [SerializeField] List<Image> _hero_runes_icon;
    [SerializeField] List<GameObject> _hero_runes_plus;
    [SerializeField] List<GameObject> _hero_runes_star_pn;
    [SerializeField] List<TextMeshProUGUI> _hero_runes_lv;
    [SerializeField] List<TextMeshProUGUI> _hero_runes_slot;
    [SerializeField] TextMeshProUGUI _hero_runes_stat;
    public void Hero_Rune_B(int slot)
    {
        Hero hero = _inf_db._managers._hero_manager.Get_Hero(_inf_db._database._user_hero_db._heroes, _chosen_hero_name);
        Rune rune = _inf_db._managers._rune_manager.Find_Hero_Rune_By_Slot(_inf_db._database._user_rune_db._runes, hero, slot);
        if (rune != null)
        {
            Rune_Inf_Pn_Op(rune._rune_id);
        }
    }
    void Hero_Runes_Pn_Load()
    {
        Hero chosen_hero = _inf_db._managers._hero_manager.Get_Hero(_inf_db._database._user_hero_db._heroes, _chosen_hero_name);

        Rune rune;

        rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, chosen_hero._rune_1_id);
        Hero_Rune_Load(rune, 1);
        rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, chosen_hero._rune_2_id);
        Hero_Rune_Load(rune, 2);
        rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, chosen_hero._rune_3_id);
        Hero_Rune_Load(rune, 3);
        rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, chosen_hero._rune_4_id);
        Hero_Rune_Load(rune, 4);
        rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, chosen_hero._rune_5_id);
        Hero_Rune_Load(rune, 5);
        rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, chosen_hero._rune_6_id);
        Hero_Rune_Load(rune, 6);
    }
    void Hero_Rune_Load(Rune rune, int slot)
    {
        if (rune != null)
        {
            for (int i = 0; i < 5; i++) { _hero_runes_star_pn[slot - 1].transform.GetChild(i).gameObject.SetActive(false); }
            for (int i = 0; i < rune._star; i++) { _hero_runes_star_pn[slot - 1].transform.GetChild(i).gameObject.SetActive(true); }

            _hero_runes_plus[slot - 1].SetActive(false);
            _hero_runes_icon[slot - 1].gameObject.SetActive(true);
            _hero_runes_icon[slot - 1].sprite = _inf_db._managers._rune_manager.Get_Rune_Sprite(rune._rare, rune._set);


            _hero_runes_lv[slot - 1].text = "+" + rune._lv;
            _hero_runes_slot[slot - 1].text = rune._slot + "";
        }
        else if (rune == null)
        {
            for (int i = 0; i < 5; i++) { _hero_runes_star_pn[slot - 1].transform.GetChild(i).gameObject.SetActive(false); }

            _hero_runes_plus[slot - 1].SetActive(true);
            _hero_runes_icon[slot - 1].gameObject.SetActive(false);

            _hero_runes_lv[slot - 1].text = "";
            _hero_runes_slot[slot - 1].text = "";
        }
    }
    void Hero_Runes_Stat_Load()
    {
        Hero hero = _inf_db._managers._hero_manager.Get_Hero(_inf_db._database._user_hero_db._heroes, _chosen_hero_name);
        Hero_Runes_Stat hero_runes_stat = _inf_db._managers._rune_manager.Hero_Runes_Stats(_inf_db._database._user_rune_db._runes, hero);

        _hero_runes_stat.text = "health : " + hero_runes_stat._health + "(+" + hero_runes_stat._health_p + "%)" + "\n" +
            "attack : " + hero_runes_stat._attack + "(+" + hero_runes_stat._attack_p + "%)" + "\n" +
            "defense : " + hero_runes_stat._defense + "(+" + hero_runes_stat._defense_p + "%)" + "\n" +
            "speed : " + hero_runes_stat._speed + "\n" +
            "crit chance : " + hero_runes_stat._crit_chance + "\n" +
            "crit damage : " + hero_runes_stat._crit_damage + "\n" +
            "accuracy : " + hero_runes_stat._accuracy + "\n" +
            "resistance : " + hero_runes_stat._resistance + "\n" +
            "pierce : " + hero_runes_stat._pierce + "\n" +
            "armor : " + hero_runes_stat._armor + "\n" +
            "lifesteal : " + hero_runes_stat._lifesteal + "\n" +
            "regeneration : " + hero_runes_stat._regeneration + "\n" +
            "recovering power : " + hero_runes_stat._recovering_power + "\n" +
            "healing power : " + hero_runes_stat._healing_power + "\n" +
            "element damage : " + hero_runes_stat._element_damage + "\n" +
            "element resistance : " + hero_runes_stat._element_resistance + "\n" +
            "crit resistance : " + hero_runes_stat._crit_resistance + "\n" +
            "crit defense : " + hero_runes_stat._crit_defense;
    }


    [SerializeField] GameObject _runes_pn, _rune_character_pn;
    [SerializeField] Transform _runes_pn_cont;
    void Runes_Pn_Load(string filter, int slot)
    {
        Runes_Pn_Clear();

        if (filter == "no owner")
        {
            for (int i = 0; i < _inf_db._database._user_rune_db._runes.Count; i++)
            {
                if (_inf_db._database._user_rune_db._runes[i]._owner == "no")
                {
                    if (slot != 0 && _inf_db._database._user_rune_db._runes[i]._slot == slot)
                    {
                        Instantiate(_rune_character_pn, _runes_pn_cont).GetComponent<rune_character_pn_sc>().Spawn_Setting(_inf_db._database._user_rune_db._runes[i], this);
                    }
                    else
                    {
                        Instantiate(_rune_character_pn, _runes_pn_cont).GetComponent<rune_character_pn_sc>().Spawn_Setting(_inf_db._database._user_rune_db._runes[i], this);
                    }
                }
            }
        }
    }
    void Runes_Pn_Clear()
    {
        for (int i = 0; i < _runes_pn_cont.childCount; i++)
        {
            Destroy(_runes_pn_cont.GetChild(i).gameObject);
        }
    }

    //Rune Inf Pn
    [SerializeField] int _chosen_rune_id;
    [SerializeField] GameObject _rune_inf_pn, _rune_inf_pn_lv_up_b;
    [SerializeField] Image _rune_inf_pn_icon;
    [SerializeField] TextMeshProUGUI _rune_inf_pn_set, _rune_inf_pn_main_stat, _rune_inf_pn_sub_stat, _rune_inf_pn_lv_up_cost, _rune_inf_pn_equip_b_txt, _rune_inf_pn_lv, _rune_inf_pn_slot;
    [SerializeField] List<Image> _rune_inf_pn_stars;
    public void Rune_Inf_Pn_Op(int rune_id)
    {
        if (rune_id != _chosen_rune_id)
        {
            _chosen_rune_id = rune_id;
            if (_rune_inf_pn.activeSelf == false)
            {
                _rune_inf_pn.SetActive(true);
            }
            Rune_Inf_Pn_Load();
        }
        else if (rune_id == _chosen_rune_id)
        {
            _chosen_rune_id = 0;
            _rune_inf_pn.SetActive(false);
        }
    }
    void Rune_Inf_Pn_Load()
    {
        Rune chosen_rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, _chosen_rune_id);

        _rune_inf_pn_set.text = chosen_rune._set;

        _rune_inf_pn_main_stat.text = chosen_rune._main_stat + ": " + _inf_db._managers._rune_manager.Value_By_Stat_Name(chosen_rune, chosen_rune._main_stat);

        for (int i = 0; i < chosen_rune._sub_stats.Count; i++)
        {
            if (i == 0)
            {
                _rune_inf_pn_sub_stat.text = chosen_rune._sub_stats[i] + ": " + _inf_db._managers._rune_manager.Value_By_Stat_Name(chosen_rune, chosen_rune._sub_stats[i]) + "\n";
            }
            else 
            {
                _rune_inf_pn_sub_stat.text += chosen_rune._sub_stats[i] + ": " + _inf_db._managers._rune_manager.Value_By_Stat_Name(chosen_rune, chosen_rune._sub_stats[i]) + "\n";
            }
        }

        if (chosen_rune._lv < 15)
        {
            _rune_inf_pn_lv_up_b.SetActive(true);
            _rune_inf_pn_lv_up_cost.text = _inf_db._managers._rune_manager.Rune_Lv_Up_Cost(chosen_rune._star, chosen_rune._lv) + "";
        }
        else if (chosen_rune._lv == 15)
        {
            _rune_inf_pn_lv_up_b.SetActive(false);
            _rune_inf_pn_lv_up_cost.text = "-";
        }

        if (chosen_rune._owner == "no")
        {
            _rune_inf_pn_equip_b_txt.text = "equip";
        }
        else 
        {
            _rune_inf_pn_equip_b_txt.text = "remove";
        }

        _rune_inf_pn_icon.sprite = _inf_db._managers._rune_manager.Get_Rune_Sprite(chosen_rune._rare, chosen_rune._set);

        int star = chosen_rune._star;
        for (int i = 0; i < _rune_inf_pn_stars.Count; i++)
        {
            _rune_inf_pn_stars[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < star; i++)
        {
            _rune_inf_pn_stars[i].gameObject.SetActive(true);
        }

        _rune_inf_pn_lv.text = chosen_rune._lv + "";
        _rune_inf_pn_slot.text = chosen_rune._slot + "";
    }

    public void Rune_Inf_Pn_Equip_Remove_B()
    {
        Rune chosen_rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, _chosen_rune_id);

        if (chosen_rune._owner == "no")
        {
            _inf_db._commands._rune_command.Cmd_Equip_Rune_To_Hero(_inf_db._database._user_general_db._user_general._user_id, _chosen_rune_id, _chosen_hero_name);
        }
        else if (chosen_rune._owner != "no")
        {
            _inf_db._commands._rune_command.Cmd_Remove_Rune_From_Hero(_inf_db._database._user_general_db._user_general._user_id, _chosen_rune_id);
        }

    }
    public void Update_After_Rune_Remove_Equip()
    {
        Hero_Runes_Pn_Load();
        Hero_Runes_Stat_Load();

        Runes_Pn_Load("no owner", 0);
        Rune_Inf_Pn_Load();
    }

    public void Rune_Inf_Pn_Lv_Up_B()
    {
        Rune chosen_rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, _chosen_rune_id);

        if (chosen_rune._lv < 15)
        {
            _inf_db._commands._rune_command.Cmd_Lv_Up_Rune(_inf_db._database._user_general_db._user_general._user_id, chosen_rune._rune_id);
        }
    }
    public void Update_After_Rune_Lv_Up()
    {
        Rune chosen_rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, _chosen_rune_id);

        if (chosen_rune._owner != "no")
        {
            Hero_Runes_Pn_Load();
            Hero_Runes_Stat_Load();
        }

        Runes_Pn_Load("no owner", 0);
        Rune_Inf_Pn_Load();
    }


    public void Rune_Inf_Pn_Sell_B()
    {
        Rune chosen_rune = _inf_db._managers._rune_manager.Get_Rune(_inf_db._database._user_rune_db._runes, _chosen_rune_id);

        _inf_db._commands._rune_command.Cmd_Sell_Rune(_inf_db._database._user_general_db._user_general._user_id, chosen_rune._rune_id);
    }
    public void Update_After_Rune_Sell(string owner)
    {
        if (owner == _chosen_hero_name)
        {
            Hero_Runes_Pn_Load();
            Hero_Runes_Stat_Load();
        }

        Runes_Pn_Load("no owner", 0);

        _rune_inf_pn.SetActive(false);
        _chosen_rune_id = 0;
    }

    #endregion



    #region Skill

    [SerializeField] List<Image> _sk_inf_icon;
    [SerializeField] List<Image> _sk_inf_icon_border;
    [SerializeField] List<Sprite> _sk_inf_icon_boarder_on_of;
    void Sk_Pn_Load()
    {
        Sk_Inf_Icon_Art_Border_Load();
    }
    void Sk_Inf_Icon_Art_Border_Load()
    {
        for (int i = 0; i < _sk_inf_icon_border.Count; i++)
        {
            _sk_inf_icon_border[i].sprite = _sk_inf_icon_boarder_on_of[0];
        }

        Hero_Sprite hero_sprite = _inf_db._managers._hero_manager.Get_Hero_Sprite(_chosen_hero_name);

        _sk_inf_icon[0].sprite = hero_sprite._aa_art;
        _sk_inf_icon[1].sprite = hero_sprite._sk_art;
        _sk_inf_icon[2].sprite = hero_sprite._ul_art;
        _sk_inf_icon[3].sprite = hero_sprite._ps_art;
    }
    public void Sk_Inf_Icon_B(string sk_type)
    {
        if (_sk_inf_pn.activeSelf == false)
        {
            _sk_inf_pn.SetActive(true);
            Sk_Inf_Pn_Load(sk_type);
        }
        else if (_sk_inf_pn.activeSelf == true && _chosen_sk_type != sk_type)
        {
            Sk_Inf_Pn_Load(sk_type);
        }
        else if (_sk_inf_pn.activeSelf == true && _chosen_sk_type == sk_type)
        {
            _sk_inf_pn.SetActive(false);
        }
    }

    [SerializeField] GameObject _sk_inf_pn, _sk_lv_up_b;
    [SerializeField] TextMeshProUGUI _sk_name_txt, _sk_inf_txt, _sk_lv_up_cost_txt;
    [SerializeField] string _chosen_sk_type;

    public string Get_Chosen_Sk_Type()
    {
        return _chosen_sk_type;
    }

    public void Sk_Inf_Pn_Load(string sk_type)
    {
        Hero_Stat hero_stat = _inf_db._managers._hero_manager.Get_Hero_Stat(_chosen_hero_name);
        Hero_Sprite hero_sprite = _inf_db._managers._hero_manager.Get_Hero_Sprite(_chosen_hero_name);
        Hero hero = _inf_db._managers._hero_manager.Get_Hero(_inf_db._database._user_hero_db._heroes, _chosen_hero_name);

        int sk_lv = 0;
        string sk_name = "";

        switch (sk_type)
        {
            case "aa":
                sk_lv = hero._aa_lv;
                sk_name = hero_stat._aa_name;
                break;
            case "sk":
                sk_lv = hero._sk_lv;
                sk_name = hero_stat._sk_name;
                break;
            case "ul":
                sk_lv = hero._ul_lv;
                sk_name = hero_stat._ul_name;
                break;
            case "ps":
                sk_lv = hero._ps_lv;
                sk_name = hero_stat._ps_name;
                break;
        }

        _sk_name_txt.text = sk_name;
        _sk_inf_txt.text = sk_inf.Sk_Inf(sk_type, _chosen_hero_name) + "\nlv: " + sk_lv;
        _sk_lv_up_cost_txt.text = "cost" + (sk_lv + 1);

        if (sk_lv >= 4)
        {
            _sk_lv_up_cost_txt.gameObject.SetActive(false);
            _sk_lv_up_b.SetActive(false);
        }
        else 
        {
            _sk_lv_up_cost_txt.gameObject.SetActive(true);
            _sk_lv_up_b.SetActive(true);
        }
    }

    #endregion
}
