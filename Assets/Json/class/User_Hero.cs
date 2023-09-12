using System;
using System.Collections.Generic;

[Serializable]
public class User_Hero
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
}
[Serializable]
public class User_Heroes
{
    public int _user_id;
    public List<User_Hero> _heroes;
}
[Serializable]
public class User_Heroes_js
{
    public List<User_Heroes> _user_heroes_js;
}
