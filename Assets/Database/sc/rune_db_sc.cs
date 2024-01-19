using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "db/rune_db")]
public class rune_db_sc : ScriptableObject
{
    public Main_Stat _slot_main_stat;
    public Sub_Stat _slot_sub_stat;

    public Rune_Sprite _rune_sprite;

    public Rune_Lv_Up_Cost _rune_lv_up_cost;
}

public class Rune_Create_Setting
{
    public string _set;
    public int _slot;
    public string _rare;
    public int _star;
}


[Serializable]
public class Main_Stat
{
    public List<string> _slot_1_main_stat;
    public List<string> _slot_2_main_stat;
    public List<string> _slot_3_main_stat;
    public List<string> _slot_4_main_stat;
    public List<string> _slot_5_main_stat;
    public List<string> _slot_6_main_stat;

    public List<string> _stat_name;

    public List<int> _star_1_start_value;
    public List<int> _star_2_start_value;
    public List<int> _star_3_start_value;
    public List<int> _star_4_start_value;
    public List<int> _star_5_start_value;

    public List<Main_Stat_Lv_Up_Value> _main_stat_lv_up_value;
}
[Serializable]
public class Main_Stat_Lv_Up_Value
{
    public int _star;
    public List<int> _lv_up_2_value;   
    public List<int> _lv_up_3_value;
    public List<int> _lv_up_15_value;
}


[Serializable]
public class Sub_Stat
{
    public List<string> _slot_1_sub_stat;
    public List<string> _slot_2_sub_stat;
    public List<string> _slot_3_sub_stat;
    public List<string> _slot_4_sub_stat;
    public List<string> _slot_5_sub_stat;
    public List<string> _slot_6_sub_stat;

    public List<string> _stat_name;
    public List<int> _star_1_mx;
    public List<int> _star_1_mn;
    public List<int> _star_2_mx;
    public List<int> _star_2_mn;
    public List<int> _star_3_mx;
    public List<int> _star_3_mn;
    public List<int> _star_4_mx;
    public List<int> _star_4_mn;
    public List<int> _star_5_mx;
    public List<int> _star_5_mn;
}


[Serializable]
public class Rune_Sprite
{
    public List<string> _set_name;
    public List<Set_Rune_Sprite> _set_rune_sprite;
}
[Serializable]
public class Set_Rune_Sprite
{
    public Sprite _common;
    public Sprite _normal;
    public Sprite _rare;
    public Sprite _epic;
    public Sprite _legend;
}


[Serializable]
public class Rune_Lv_Up_Cost
{
    public int _lv_up_cost_multiplicate;
    public List<int> _start_cost_by_star;
}