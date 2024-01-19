using System;
using System.Collections.Generic;

[Serializable]
public class Rune
{
    public int _rune_id;
    public string _set;
    public int _slot;
    public string _rare;
    public int _star;
    public string _main_stat;
    public List<string> _sub_stats = new();
    public int _lv;
    public string _owner;

    public int _health;
    public int _health_p;
    public int _attack;
    public int _attack_p;
    public int _defense;
    public int _defense_p;
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

[Serializable]
public class User_Runes
{
    public string _user_id;
    public List<Rune> _runes = new();
}
