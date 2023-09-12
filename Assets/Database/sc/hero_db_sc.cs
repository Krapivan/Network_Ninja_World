using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "db/hero_db")]
public class hero_db_sc : ScriptableObject
{
    public List<Hero_Stat> _heroes_stat;
    public List<Hero_Sprite> _heroes_sprite;

    public Rare_Art _rare_art;


    public Sprite Get_Rare_Art(string rare)
    {
        switch (rare) 
        {
            case "b": return _rare_art._b;
            case "a": return _rare_art._a;
            case "s": return _rare_art._s;
            case "ss": return _rare_art._ss;
        }

        return null;
    }
    public Hero_Stat Get_Hero_Stat(string name)
    {
        for (int i = 0; i < _heroes_stat.Count; i++)
        {
            if (_heroes_stat[i]._name == name)
            {
                return _heroes_stat[i];
            }
        }

        return null;
    }
    public Hero_Sprite Get_Hero_Sprite(string name)
    {
        for (int i = 0; i < _heroes_sprite.Count; i++)
        {
            if (_heroes_sprite[i]._name == name)
            {
                return _heroes_sprite[i];
            }
        }

        return null;
    }
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
    public int _recovering_power;
    public int _healing_power;
    public int _element_damage;
    public int _element_resistance;
    public int _crit_resistance;
    public int _crit_defense;


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

    public Sprite _aa_art;
    public Sprite _sk_art;
    public Sprite _ul_art;
    public Sprite _ps_art;

    public Sprite _idle;
    public Sprite _hit;
    public Sprite _pain;
    public Sprite _action;
    public Sprite _die;
}
[Serializable]
public class Rare_Art
{
    public Sprite _b;
    public Sprite _a;
    public Sprite _s;
    public Sprite _ss;
}
