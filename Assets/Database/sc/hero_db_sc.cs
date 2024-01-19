using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "db/hero_db")]
public class hero_db_sc : ScriptableObject
{
    public List<Hero_Stat> _heroes_stat;
    public List<Hero_Sprite> _heroes_sprite;
    public Hero_Grow_Stat _hero_grow_stat;

    public Rare_Art _rare_art;

}

[Serializable]
public class Hero_Stat
{
    public string _name;

    public string _rare;
    public string _el;

    public int _health;
    public int _attack;
    public int _defense;
    public int _speed;
    public int _crit_chance;
    public int _crit_damage;
    public int _accuracy;
    public int _resistance;

    public int _pierce;
    public int _armor;
    public int _lifesteal;
    public int _regeneration;
    public int _healing_power;
    public int _recovering_power;
    public int _crit_resistance;
    public int _crit_defense;
    public int _element_damage;
    public int _element_resistance;


    public string _aa_name;
    public string _sk_name;
    public string _ul_name;
    public string _ps_name;


    public int _sk_cd;
    public int _ul_cd;
    public int _ps_cd;
    public int _sk_pcd;
    public int _ul_pcd;
    public int _ps_pcd;
}
[Serializable]
public class Hero_Sprite
{
    public string _name;

    public Sprite _art;
    public Sprite _icon;

    public Sprite _aa_art;
    public Sprite _sk_art;
    public Sprite _ul_art;
    public Sprite _ps_art;
}
[Serializable]
public class Rare_Art
{
    public Sprite _c;
    public Sprite _n;
    public Sprite _r;
    public Sprite _e;
    public Sprite _l;

    public Sprite _c_back;
    public Sprite _n_back;
    public Sprite _r_back;
    public Sprite _e_back;
    public Sprite _l_back;
}
[Serializable]
public class Hero_Grow_Stat
{
    public int _first_hero_lv_up_exp_cost;
    public int _next_hero_lv_up_exp_multiplier;

    public int _sk_evry_lv_up_fragment_cost;


    public Pve_Stat_Bonus_Cost _pve_stat_bonus_cost;
}
[Serializable]
public class Pve_Stat_Bonus_Cost
{
    public int _health;
    public int _attack;
    public int _defense;
    public int _speed;
    public int _crit_chance;
    public int _crit_damage;
    public int _accuracy;
    public int _resistance;

    public int _pierce;
    public int _armor;
    public int _lifesteal;
    public int _regeneration;
    public int _healing_power;
    public int _recovering_power;
    public int _crit_resistance;
    public int _crit_defense;
    public int _element_damage;
    public int _element_resistance;
}
