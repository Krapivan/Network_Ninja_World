using System.Collections.Generic;
using UnityEngine;

public class rune_manager : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] rune_db_sc _rune_db;


    //Create Rune
    public Rune Create_Rune(Rune_Create_Setting rune_create_setting, List<Rune> runes)
    {
        Rune rune = new Rune()
        {
            _rune_id = runes.Count + 1,
            _set = rune_create_setting._set,
            _slot = rune_create_setting._slot,
            _rare = rune_create_setting._rare,
            _star = rune_create_setting._star,
            _main_stat = "no",
            _lv = 0,
            _owner = "no",

            _health = 0,
            _health_p = 0,
            _attack = 0,
            _attack_p = 0,
            _defense = 0,
            _defense_p = 0,
            _speed = 0,
            _crit_chance = 0,
            _crit_damage = 0,
            _accuracy = 0,
            _resistance = 0,
            _pierce = 0,
            _armor = 0,
            _lifesteal = 0,
            _regeneration = 0,
            _healing_power = 0,
            _recovering_power = 0,
            _crit_resistance = 0,
            _crit_defense = 0,
            _element_damage = 0,
            _element_resistance = 0
        };

        rune = Add_Main_Stat(rune);

        int need_sub_stat_count = 0;
        switch (rune._rare)
        {
            case "common": need_sub_stat_count = 0; break;
            case "normal": need_sub_stat_count = 1; break;
            case "rare": need_sub_stat_count = 2; break;
            case "epic": need_sub_stat_count = 3; break;
            case "legend": need_sub_stat_count = 4; break;
        }
        for (int i = 1; i <= need_sub_stat_count; i++)
        {
            rune = Add_Sub_State(rune);
        }

        return rune;
    }


    //Main Stat
    public Rune Add_Main_Stat(Rune rune)
    {
        string main_stat = Roll_Main_Stat(rune._slot);
        rune._main_stat = main_stat;

        switch (rune._main_stat)
        {
            case "health": rune._health += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "health_p": rune._health_p += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "attack": rune._attack += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "attack_p": rune._attack_p += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "defense": rune._defense += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "defense_p": rune._defense_p += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "speed": rune._speed += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "crit_chance": rune._crit_chance += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "crit_damage": rune._crit_damage += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "accuracy": rune._accuracy += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "resistance": rune._resistance += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "pierce": rune._pierce += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "armor": rune._armor += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "lifesteal": rune._lifesteal += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "regeneration": rune._regeneration += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "healing_power": rune._healing_power += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "recovering_power": rune._recovering_power += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "crit_resistance": rune._crit_resistance += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "crit_defense": rune._crit_defense += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "element_damage": rune._element_damage += Main_Stat_Value(rune._star, rune._main_stat); break;
            case "element_resistance": rune._element_resistance += Main_Stat_Value(rune._star, rune._main_stat); break;
        }

        return rune;
    }
    string Roll_Main_Stat(int slot)
    {
        int value_num = 0;

        if (slot == 1)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_main_stat._slot_1_main_stat.Count);
            return _rune_db._slot_main_stat._slot_1_main_stat[value_num];
        }
        else if (slot == 2)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_main_stat._slot_2_main_stat.Count);
            return _rune_db._slot_main_stat._slot_2_main_stat[value_num];
        }
        else if (slot == 3)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_main_stat._slot_3_main_stat.Count);
            return _rune_db._slot_main_stat._slot_3_main_stat[value_num];
        }
        else if (slot == 4)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_main_stat._slot_4_main_stat.Count);
            return _rune_db._slot_main_stat._slot_4_main_stat[value_num];
        }
        else if (slot == 5)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_main_stat._slot_5_main_stat.Count);
            return _rune_db._slot_main_stat._slot_5_main_stat[value_num];
        }
        else if (slot == 6)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_main_stat._slot_6_main_stat.Count);
            return _rune_db._slot_main_stat._slot_6_main_stat[value_num];
        }

        return "no";
    }
    int Main_Stat_Value(int star, string stat_name)
    {
        for (int i = 0; i < _rune_db._slot_main_stat._stat_name.Count; i++)
        {
            if (_rune_db._slot_main_stat._stat_name[i] == stat_name)
            {
                if (star == 1)
                {
                    return _rune_db._slot_main_stat._star_1_start_value[i];
                }
                else if (star == 2)
                {
                    return _rune_db._slot_main_stat._star_2_start_value[i];
                }
                else if (star == 3)
                {
                    return _rune_db._slot_main_stat._star_3_start_value[i];
                }
                else if (star == 4)
                {
                    return _rune_db._slot_main_stat._star_4_start_value[i];
                }
                else if (star == 5)
                {
                    return _rune_db._slot_main_stat._star_5_start_value[i];
                }
            }
        }


        return 0;
    }


    //Sub Stat
    public Rune Add_Sub_State(Rune rune)
    {
        if (rune._sub_stats.Count < 4)
        {
            string state_name = "";
            do
            {
                state_name = Roll_New_Sub_Stat(rune._slot);
            }
            while (rune._sub_stats.Contains(state_name) == true || rune._main_stat == state_name);

            rune._sub_stats.Add(state_name);

            switch (state_name)
            {
                case "health": rune._health += Sub_Stat_Value(rune._star, state_name); break;
                case "health_p": rune._health_p += Sub_Stat_Value(rune._star, state_name); break;
                case "attack": rune._attack += Sub_Stat_Value(rune._star, state_name); break;
                case "attack_p": rune._attack_p += Sub_Stat_Value(rune._star, state_name); break;
                case "defense": rune._defense += Sub_Stat_Value(rune._star, state_name); break;
                case "defense_p": rune._defense_p += Sub_Stat_Value(rune._star, state_name); break;
                case "speed": rune._speed += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_chance": rune._crit_chance += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_damage": rune._crit_damage += Sub_Stat_Value(rune._star, state_name); break;
                case "accuracy": rune._accuracy += Sub_Stat_Value(rune._star, state_name); break;
                case "resistance": rune._resistance += Sub_Stat_Value(rune._star, state_name); break;
                case "pierce": rune._pierce += Sub_Stat_Value(rune._star, state_name); break;
                case "armor": rune._armor += Sub_Stat_Value(rune._star, state_name); break;
                case "lifesteal": rune._lifesteal += Sub_Stat_Value(rune._star, state_name); break;
                case "regeneration": rune._regeneration += Sub_Stat_Value(rune._star, state_name); break;
                case "healing_power": rune._healing_power += Sub_Stat_Value(rune._star, state_name); break;
                case "recovering_power": rune._recovering_power += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_resistance": rune._crit_resistance += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_defense": rune._crit_defense += Sub_Stat_Value(rune._star, state_name); break;
                case "element_damage": rune._element_damage += Sub_Stat_Value(rune._star, state_name); break;
                case "element_resistance": rune._element_resistance += Sub_Stat_Value(rune._star, state_name); break;
            }
        }
        else if (rune._sub_stats.Count == 4)
        {
            string state_name = Roll_Have_Sub_Stat(rune);

            switch (state_name)
            {
                case "health": rune._health += Sub_Stat_Value(rune._star, state_name); break;
                case "health_p": rune._health_p += Sub_Stat_Value(rune._star, state_name); break;
                case "attack": rune._attack += Sub_Stat_Value(rune._star, state_name); break;
                case "attack_p": rune._attack_p += Sub_Stat_Value(rune._star, state_name); break;
                case "defense": rune._defense += Sub_Stat_Value(rune._star, state_name); break;
                case "defense_p": rune._defense_p += Sub_Stat_Value(rune._star, state_name); break;
                case "speed": rune._speed += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_chance": rune._crit_chance += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_damage": rune._crit_damage += Sub_Stat_Value(rune._star, state_name); break;
                case "accuracy": rune._accuracy += Sub_Stat_Value(rune._star, state_name); break;
                case "resistance": rune._resistance += Sub_Stat_Value(rune._star, state_name); break;
                case "pierce": rune._pierce += Sub_Stat_Value(rune._star, state_name); break;
                case "armor": rune._armor += Sub_Stat_Value(rune._star, state_name); break;
                case "lifesteal": rune._lifesteal += Sub_Stat_Value(rune._star, state_name); break;
                case "regeneration": rune._regeneration += Sub_Stat_Value(rune._star, state_name); break;
                case "healing_power": rune._healing_power += Sub_Stat_Value(rune._star, state_name); break;
                case "recovering_power": rune._recovering_power += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_resistance": rune._crit_resistance += Sub_Stat_Value(rune._star, state_name); break;
                case "crit_defense": rune._crit_defense += Sub_Stat_Value(rune._star, state_name); break;
                case "element_damage": rune._element_damage += Sub_Stat_Value(rune._star, state_name); break;
                case "element_resistance": rune._element_resistance += Sub_Stat_Value(rune._star, state_name); break;
            }
        }

        return rune;
    }
    string Roll_New_Sub_Stat(int slot)
    {
        int value_num = 0;

        if (slot == 1)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_sub_stat._slot_1_sub_stat.Count);
            return _rune_db._slot_sub_stat._slot_1_sub_stat[value_num];
        }
        else if (slot == 2)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_sub_stat._slot_2_sub_stat.Count);
            return _rune_db._slot_sub_stat._slot_2_sub_stat[value_num];
        }
        else if (slot == 3)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_sub_stat._slot_3_sub_stat.Count);
            return _rune_db._slot_sub_stat._slot_3_sub_stat[value_num];
        }
        else if (slot == 4)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_sub_stat._slot_4_sub_stat.Count);
            return _rune_db._slot_sub_stat._slot_4_sub_stat[value_num];
        }
        else if (slot == 5)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_sub_stat._slot_5_sub_stat.Count);
            return _rune_db._slot_sub_stat._slot_5_sub_stat[value_num];
        }
        else if (slot == 6)
        {
            value_num = UnityEngine.Random.Range(0, _rune_db._slot_sub_stat._slot_6_sub_stat.Count);
            return _rune_db._slot_sub_stat._slot_6_sub_stat[value_num];
        }

        return "no";
    }
    string Roll_Have_Sub_Stat(Rune rune)
    {
        int stat_num = UnityEngine.Random.Range(0, 5);
        string value = rune._sub_stats[stat_num];
        return value;
    }
    int Sub_Stat_Value(int star, string stat_name)
    {
        int value = 0, mx = 0, mn = 0;

        for (int i = 0; i < _rune_db._slot_sub_stat._stat_name.Count; i++)
        {
            if (_rune_db._slot_sub_stat._stat_name[i] == stat_name)
            {
                if (star == 1)
                {
                    mx = _rune_db._slot_sub_stat._star_1_mx[i];
                    mn = _rune_db._slot_sub_stat._star_1_mn[i];
                }
                else if (star == 2)
                {
                    mx = _rune_db._slot_sub_stat._star_2_mx[i];
                    mn = _rune_db._slot_sub_stat._star_2_mn[i];
                }
                else if (star == 3)
                {
                    mx = _rune_db._slot_sub_stat._star_3_mx[i];
                    mn = _rune_db._slot_sub_stat._star_3_mn[i];
                }
                else if (star == 4)
                {
                    mx = _rune_db._slot_sub_stat._star_4_mx[i];
                    mn = _rune_db._slot_sub_stat._star_4_mn[i];
                }
                else if (star == 5)
                {
                    mx = _rune_db._slot_sub_stat._star_5_mx[i];
                    mn = _rune_db._slot_sub_stat._star_5_mn[i];
                }

                return value = UnityEngine.Random.Range(mn, mx + 1);
            }
        }

        return value;
    }


    //Rune Lv Up
    public Rune Rune_Lv_Up(Rune rune)
    {
        if (rune._lv < 15)
        {
            rune._lv++;

            Main_Stat_Lv_Up_Stat(rune);

            if (rune._lv % 3 == 0)
            {
                Add_Sub_State(rune);
            }
        }

        return rune;
    }
    Rune Main_Stat_Lv_Up_Stat(Rune rune)
    {
        int value = Main_Stat_Lv_Up_Value(rune._star, rune._lv, rune._main_stat);
        switch (rune._main_stat)
        {
            case "health": rune._health += value; break;
            case "health_p": rune._health_p += value; break;
            case "attack": rune._attack += value; break;
            case "attack_p": rune._attack_p += value; break;
            case "defense": rune._defense += value; break;
            case "defense_p": rune._defense_p += value; break;
            case "speed": rune._speed += value; break;
            case "crit_chance": rune._crit_chance += value; break;
            case "crit_damage": rune._crit_damage += value; break;
            case "accuracy": rune._accuracy += value; break;
            case "resistance": rune._resistance += value; break;
            case "pierce": rune._pierce += value; break;
            case "armor": rune._armor += value; break;
            case "lifesteal": rune._lifesteal += value; break;
            case "regeneration": rune._regeneration += value; break;
            case "healing_power": rune._healing_power += value; break;
            case "recovering_power": rune._recovering_power += value; break;
            case "crit_resistance": rune._crit_resistance += value; break;
            case "crit_defense": rune._crit_defense += value; break;
            case "element_damage": rune._element_damage += value; break;
            case "element_resistance": rune._element_resistance += value; break;
        }

        return rune;
    }
    int Main_Stat_Lv_Up_Value(int star, int lv, string stat_name)
    {
        int value = 0;

        for (int i = 0; i < _rune_db._slot_main_stat._stat_name.Count; i++)
        {
            if (_rune_db._slot_main_stat._stat_name[i] == stat_name)
            {
                switch (star)
                {
                    case 1:
                        if (lv % 3 == 0 && lv != 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[0]._lv_up_3_value[i];
                        }
                        else if (lv == 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[0]._lv_up_15_value[i];
                        }
                        else if (lv % 3 != 0)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[0]._lv_up_2_value[i];
                        }
                        break;
                    case 2:
                        if (lv % 3 == 0 && lv != 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[1]._lv_up_3_value[i];
                        }
                        else if (lv == 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[1]._lv_up_15_value[i];
                        }
                        else if (lv % 3 != 0)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[1]._lv_up_2_value[i];
                        }
                        break;
                    case 3:
                        if (lv % 3 == 0 && lv != 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[2]._lv_up_3_value[i];
                        }
                        else if (lv == 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[2]._lv_up_15_value[i];
                        }
                        else if (lv % 3 != 0)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[2]._lv_up_2_value[i];
                        }
                        break;
                    case 4:
                        if (lv % 3 == 0 && lv != 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[3]._lv_up_3_value[i];
                        }
                        else if (lv == 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[3]._lv_up_15_value[i];
                        }
                        else if (lv % 3 != 0)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[3]._lv_up_2_value[i];
                        }
                        break;
                    case 5:
                        if (lv % 3 == 0 && lv != 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[4]._lv_up_3_value[i];
                        }
                        else if (lv == 15)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[4]._lv_up_15_value[i];
                        }
                        else if (lv % 3 != 0)
                        {
                            value = _rune_db._slot_main_stat._main_stat_lv_up_value[4]._lv_up_2_value[i];
                        }
                        break;
                }
            }
        }

        return value;
    }


    //Get Rune From List
    public Rune Get_Rune(List<Rune> runes, int _rune_id)
    {
        for (int i = 0; i < runes.Count; i++)
        {
            if (runes[i]._rune_id == _rune_id)
            {
                return runes[i];
            }
        }

        return null;
    }

    
    //Rune Stats
    public int Value_By_Stat_Name(Rune rune, string stat_name)
    {
        switch (stat_name)
        {
            case "health": return rune._health;
            case "health_p": return rune._health_p;
            case "attack": return rune._attack;
            case "attack_p": return rune._attack_p;
            case "defense": return rune._defense;
            case "defense_p": return rune._defense_p;
            case "speed": return rune._speed;
            case "crit_chance": return rune._crit_chance;
            case "crit_damage": return rune._crit_damage;
            case "accuracy": return rune._accuracy;
            case "resistance": return rune._resistance;
            case "pierce": return rune._pierce;
            case "armor": return rune._armor;
            case "lifesteal": return rune._lifesteal;
            case "regeneration": return rune._regeneration;
            case "healing_power": return rune._healing_power;
            case "recovering_power": return rune._recovering_power;
            case "crit_resistance": return rune._crit_resistance;
            case "crit_defense": return rune._crit_defense;
            case "element_damage": return rune._element_damage;
            case "element_resistance": return rune._element_resistance;
        }

        return 0;
    }
    public Hero_Runes_Stat Hero_Runes_Stats(List<Rune> runes, Hero hero)
    {
        Hero_Runes_Stat hero_runes_stat = new();

        for (int i = 1; i <= 6; i++)
        {
            Rune rune = new();
            switch (i) 
            {
                case 1: if (hero._rune_1_id != 0) rune = Get_Rune(runes, hero._rune_1_id); break;
                case 2: if (hero._rune_2_id != 0) rune = Get_Rune(runes, hero._rune_2_id); break;
                case 3: if (hero._rune_3_id != 0) rune = Get_Rune(runes, hero._rune_3_id); break;
                case 4: if (hero._rune_4_id != 0) rune = Get_Rune(runes, hero._rune_4_id); break;
                case 5: if (hero._rune_5_id != 0) rune = Get_Rune(runes, hero._rune_5_id); break;
                case 6: if (hero._rune_6_id != 0) rune = Get_Rune(runes, hero._rune_6_id); break;
            }
            if (rune != null)
            {
                switch (rune._main_stat)
                {
                    case "health": hero_runes_stat._health += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "health_p": hero_runes_stat._health_p += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "attack": hero_runes_stat._attack += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "attack_p": hero_runes_stat._attack_p += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "defense": hero_runes_stat._defense += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "defense_p": hero_runes_stat._defense_p += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "speed": hero_runes_stat._speed += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "crit_chance": hero_runes_stat._crit_chance += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "crit_damage": hero_runes_stat._crit_damage += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "accuracy": hero_runes_stat._accuracy += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "resistance": hero_runes_stat._resistance += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "pierce": hero_runes_stat._pierce += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "armor": hero_runes_stat._armor += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "lifesteal": hero_runes_stat._lifesteal += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "regeneration": hero_runes_stat._regeneration += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "healing_power": hero_runes_stat._healing_power += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "recovering_power": hero_runes_stat._recovering_power += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "crit_resistance": hero_runes_stat._crit_resistance += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "crit_defense": hero_runes_stat._crit_defense += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "element_damage": hero_runes_stat._element_damage += Value_By_Stat_Name(rune, rune._main_stat); break;
                    case "element_resistance": hero_runes_stat._element_resistance += Value_By_Stat_Name(rune, rune._main_stat); break;
                }

                for (int j = 0; j < rune._sub_stats.Count; j++)
                {
                    switch (rune._sub_stats[j])
                    {
                        case "health": hero_runes_stat._health += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "health_p": hero_runes_stat._health_p += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "attack": hero_runes_stat._attack += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "attack_p": hero_runes_stat._attack_p += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "defense": hero_runes_stat._defense += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "defense_p": hero_runes_stat._defense_p += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "speed": hero_runes_stat._speed += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "crit_chance": hero_runes_stat._crit_chance += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "crit_damage": hero_runes_stat._crit_damage += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "accuracy": hero_runes_stat._accuracy += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "resistance": hero_runes_stat._resistance += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "pierce": hero_runes_stat._pierce += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "armor": hero_runes_stat._armor += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "lifesteal": hero_runes_stat._lifesteal += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "regeneration": hero_runes_stat._regeneration += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "healing_power": hero_runes_stat._healing_power += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "recovering_power": hero_runes_stat._recovering_power += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "crit_resistance": hero_runes_stat._crit_resistance += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "crit_defense": hero_runes_stat._crit_defense += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "element_damage": hero_runes_stat._element_damage += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                        case "element_resistance": hero_runes_stat._element_resistance += Value_By_Stat_Name(rune, rune._sub_stats[j]); break;
                    }
                }
            }
        }

        return hero_runes_stat;
    }
    public Rune Find_Hero_Rune_By_Slot(List<Rune> runes, Hero hero, int slot)
    {
        switch (slot)
        {
            case 1: if (hero._rune_1_id != 0) return Get_Rune(runes, hero._rune_1_id); break;
            case 2: if (hero._rune_2_id != 0) return Get_Rune(runes, hero._rune_2_id); break;
            case 3: if (hero._rune_3_id != 0) return Get_Rune(runes, hero._rune_3_id); break;
            case 4: if (hero._rune_4_id != 0) return Get_Rune(runes, hero._rune_4_id); break;
            case 5: if (hero._rune_5_id != 0) return Get_Rune(runes, hero._rune_5_id); break;
            case 6: if (hero._rune_6_id != 0) return Get_Rune(runes, hero._rune_6_id); break;
        }

        return null;
    }


    //Resource From Rune_Db
    public Sprite Get_Rune_Sprite(string rare, string set)
    {
        for (int i = 0; i < _rune_db._rune_sprite._set_name.Count; i++)
        {
            if (_rune_db._rune_sprite._set_name[i] == set)
            {
                switch (rare)
                {
                    case "common": return _rune_db._rune_sprite._set_rune_sprite[i]._common;
                    case "normal": return _rune_db._rune_sprite._set_rune_sprite[i]._normal;
                    case "rare": return _rune_db._rune_sprite._set_rune_sprite[i]._rare;
                    case "epic": return _rune_db._rune_sprite._set_rune_sprite[i]._epic;
                    case "legend": return _rune_db._rune_sprite._set_rune_sprite[i]._legend;
                }
            }
        }

        return null;
    }
    public int Rune_Lv_Up_Cost(int star, int lv)
    {
        return _rune_db._rune_lv_up_cost._start_cost_by_star[star - 1] * (100 + (30 * lv)) / 100;
    }
    public int Rune_Sell_Cost(int star, int lv)
    {
        int cost = Rune_Lv_Up_Cost(star, lv) * 3;

        return cost;
    }


    //Action with Rune From List
    public List<Rune> Update_Rune_From_List(Rune rune, List<Rune> runes)
    {
        for (int i = 0; i < runes.Count; i++)
        {
            if (runes[i]._rune_id == rune._rune_id)
            {
                runes[i] = rune;
            }
        }

        return runes;
    }
    public Runes_Heroes_Return Dell_Rune_From_List(Rune rune, List<Rune> runes, List<Hero> heroes)
    {
        //runes after dell rune
        for (int i = rune._rune_id; i < runes.Count; i++)
        {
            if (runes[i]._owner != "no")
            {
                Hero hero = _inf_db._managers._hero_manager.Get_Hero(heroes, runes[i]._owner);
                switch (runes[i]._slot)
                {
                    case 1: hero._rune_1_id -= 1; break;
                    case 2: hero._rune_2_id -= 1; break;
                    case 3: hero._rune_3_id -= 1; break;
                    case 4: hero._rune_4_id -= 1; break;
                    case 5: hero._rune_5_id -= 1; break;
                    case 6: hero._rune_6_id -= 1; break;
                }
                _inf_db._managers._hero_manager.Update_Hero_From_List(hero, heroes);
            }
            runes[i]._rune_id -= 1;
        }

        //dell rune
        if (rune._owner != "no")
        {
            Hero hero = _inf_db._managers._hero_manager.Get_Hero(heroes, rune._owner);
            switch (rune._slot)
            {
                case 1: hero._rune_1_id = 0; break;
                case 2: hero._rune_2_id = 0; break;
                case 3: hero._rune_3_id = 0; break;
                case 4: hero._rune_4_id = 0; break;
                case 5: hero._rune_5_id = 0; break;
                case 6: hero._rune_6_id = 0; break;
            }
            _inf_db._managers._hero_manager.Update_Hero_From_List(hero, heroes);
        }
        runes.RemoveAt(rune._rune_id - 1);

        Runes_Heroes_Return runes_heroes = new()
        {
            _runes = runes,
            _heroes = heroes
        };

        return runes_heroes;
    }
}
public class Runes_Heroes_Return
{
    public List<Rune> _runes;
    public List<Hero> _heroes;
}
public class Hero_Runes_Stat
{
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