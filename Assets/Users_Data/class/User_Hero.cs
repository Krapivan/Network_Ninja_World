using System;
using System.Collections.Generic;

[Serializable]
public class Hero
{
    public string _name;

    public int _lv;
    public int _exp;
    public int _exp_need;
    public int _star;
    public int _ev_star;

    public int _aa_lv;
    public int _sk_lv;
    public int _ul_lv;
    public int _ps_lv;

    public int _rune_1_id;
    public int _rune_2_id;
    public int _rune_3_id;
    public int _rune_4_id;
    public int _rune_5_id;
    public int _rune_6_id;

    public int _fragments;
}

[Serializable]
public class User_Heroes
{
    public string _user_id;
    public List<Hero> _heroes = new();
}
