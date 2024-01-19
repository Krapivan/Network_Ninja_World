using System.Collections.Generic;
using UnityEngine;

public class hero_manager : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] hero_db_sc _hero_db;


    //Create Hero
    public Hero Create_Hero(string hero_name)
    {
        Hero hero = new()
        {
            _name = hero_name,
            _lv = 0,
            _exp = 0,
            _exp_need = _hero_db._hero_grow_stat._first_hero_lv_up_exp_cost * (100 + _hero_db._hero_grow_stat._next_hero_lv_up_exp_multiplier * 0) / 100,
            _star = 1,
            _ev_star = 0,
            _aa_lv = 0,
            _sk_lv = 0,
            _ul_lv = 0,
            _ps_lv = 0,
            _rune_1_id = 0,
            _rune_2_id = 0,
            _rune_3_id = 0,
            _rune_4_id = 0,
            _rune_5_id = 0,
            _rune_6_id = 0,
            _fragments = 0
        };

        return hero;
    }


    //Any Resource From Hero_Db
    public Sprite Get_Rare_Art(string rare)
    {
        switch (rare)
        {
            case "n": return _hero_db._rare_art._n;
            case "c": return _hero_db._rare_art._c;
            case "r": return _hero_db._rare_art._r;
            case "e": return _hero_db._rare_art._e;
            case "l": return _hero_db._rare_art._l;
        }

        return null;
    }
    public Sprite Get_Rare_Back_Sprite(string rare)
    {
        switch (rare)
        {
            case "c": return _hero_db._rare_art._c_back;
            case "n": return _hero_db._rare_art._n_back;
            case "r": return _hero_db._rare_art._r_back;
            case "e": return _hero_db._rare_art._e_back;
            case "l": return _hero_db._rare_art._l_back;
        }

        return null;
    }
    public Hero_Stat Get_Hero_Stat(string name)
    {
        for (int i = 0; i < _hero_db._heroes_stat.Count; i++)
        {
            if (_hero_db._heroes_stat[i]._name == name)
            {
                return _hero_db._heroes_stat[i];
            }
        }

        return null;
    }
    public Hero_Sprite Get_Hero_Sprite(string name)
    {
        for (int i = 0; i < _hero_db._heroes_sprite.Count; i++)
        {
            if (_hero_db._heroes_sprite[i]._name == name)
            {
                return _hero_db._heroes_sprite[i];
            }
        }

        return null;
    }


    //Get Hero From List
    public Hero Get_Hero(List<Hero> heroes, string name)
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            if (heroes[i]._name == name)
            {
                return heroes[i];
            }
        }
        return null;
    }


    //Hero Stat
    public Hero_Stat Hero_Stat_Without_Runes(Hero hero)
    {
        Hero_Stat hero_stat_without_runes = new();

        Hero_Stat hero_stat = Get_Hero_Stat(hero._name);

        hero_stat_without_runes._health = hero_stat._health;
        hero_stat_without_runes._attack = hero_stat._attack;
        hero_stat_without_runes._defense = hero_stat._defense;
        hero_stat_without_runes._speed = hero_stat._speed;
        hero_stat_without_runes._accuracy = hero_stat._accuracy;
        hero_stat_without_runes._resistance = hero_stat._resistance;
        hero_stat_without_runes._crit_chance = hero_stat._crit_chance;
        hero_stat_without_runes._crit_damage = hero_stat._crit_damage;
        hero_stat_without_runes._pierce = hero_stat._pierce;
        hero_stat_without_runes._armor = hero_stat._armor;
        hero_stat_without_runes._lifesteal = hero_stat._lifesteal;
        hero_stat_without_runes._regeneration = hero_stat._regeneration;
        hero_stat_without_runes._healing_power = hero_stat._healing_power;
        hero_stat_without_runes._recovering_power = hero_stat._recovering_power;
        hero_stat_without_runes._crit_resistance = hero_stat._crit_resistance;
        hero_stat_without_runes._crit_defense = hero_stat._crit_defense;
        hero_stat_without_runes._element_damage = hero_stat._element_damage;
        hero_stat_without_runes._element_resistance = hero_stat._element_resistance;

        int lv = hero._lv;
        for (int i = 1; i <= lv; i++)
        {
            hero_stat_without_runes._health = hero_stat_without_runes._health * 101 / 100;
            hero_stat_without_runes._attack = hero_stat_without_runes._attack * 101 / 100;
            hero_stat_without_runes._defense = hero_stat_without_runes._defense * 101 / 100;
        }

        return hero_stat_without_runes;
    }
    public Hero_Stat Hero_Stat_With_Runes(List<Rune> runes, Hero hero)
    {
        Hero_Stat hero_stat_without_runes = Hero_Stat_Without_Runes(hero);
        Hero_Runes_Stat hero_runes_stat = _inf_db._managers._rune_manager.Hero_Runes_Stats(runes, hero);
        Hero_Stat hero_stat_with_runes = new()
        {
            _health = (hero_stat_without_runes._health * (100 + hero_runes_stat._health_p) / 100) + hero_runes_stat._health,
            _attack = (hero_stat_without_runes._attack * (100 + hero_runes_stat._attack_p) / 100) + hero_runes_stat._attack,
            _defense = (hero_stat_without_runes._defense * (100 + hero_runes_stat._defense_p) / 100) + hero_runes_stat._defense,
            _speed = hero_stat_without_runes._speed + hero_runes_stat._speed,
            _accuracy = hero_stat_without_runes._accuracy + hero_runes_stat._accuracy,
            _resistance = hero_stat_without_runes._resistance + hero_runes_stat._resistance,
            _crit_chance = hero_stat_without_runes._crit_chance + hero_runes_stat._crit_chance,
            _crit_damage = hero_stat_without_runes._crit_damage + hero_runes_stat._crit_damage,
            _pierce = hero_stat_without_runes._pierce + hero_runes_stat._pierce,
            _armor = hero_stat_without_runes._armor + hero_runes_stat._armor,
            _lifesteal = hero_stat_without_runes._lifesteal + hero_runes_stat._lifesteal,
            _regeneration = hero_stat_without_runes._regeneration + hero_runes_stat._regeneration,
            _healing_power = hero_stat_without_runes._healing_power + hero_runes_stat._healing_power,
            _recovering_power = hero_stat_without_runes._recovering_power + hero_runes_stat._recovering_power,
            _crit_resistance = hero_stat_without_runes._crit_resistance + hero_runes_stat._crit_resistance,
            _crit_defense = hero_stat_without_runes._crit_defense + hero_runes_stat._crit_defense,
            _element_damage = hero_stat_without_runes._element_damage + hero_runes_stat._element_damage,
            _element_resistance = hero_stat_without_runes._element_resistance + hero_runes_stat._element_resistance
        };
        return hero_stat_with_runes;
    }
    public Hero_Stat Hero_Stat_For_PVE_Enemy(string hero_name, int enemy_lv, int enemy_bonus)
    {
        Hero_Stat hero_stat = Get_Hero_Stat(hero_name);

        //lv
        hero_stat._health = hero_stat._health * (100 + enemy_lv) / 100;
        hero_stat._attack = hero_stat._attack * (100 + enemy_lv) / 100;
        hero_stat._defense = hero_stat._defense * (100 + enemy_lv) / 100;

        //bonus
        hero_stat._health = hero_stat._health * (100 + (enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._health)) / 100;
        hero_stat._attack = hero_stat._attack * (100 + (enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._attack)) / 100;
        hero_stat._defense = hero_stat._defense * (100 + (enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._defense)) / 100;
        hero_stat._speed += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._speed;
        hero_stat._crit_chance += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._crit_chance;
        hero_stat._crit_damage += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._crit_damage;
        hero_stat._accuracy += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._accuracy;
        hero_stat._resistance += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._resistance;

        hero_stat._pierce += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._pierce;
        hero_stat._armor += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._armor;
        hero_stat._lifesteal += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._lifesteal;
        hero_stat._regeneration += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._regeneration;
        hero_stat._healing_power += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._healing_power;
        hero_stat._recovering_power += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._recovering_power;
        hero_stat._crit_resistance += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._crit_resistance;
        hero_stat._crit_defense += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._crit_defense;
        hero_stat._element_damage += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._element_damage;
        hero_stat._element_resistance += enemy_bonus / _inf_db._database._hero_db._hero_grow_stat._pve_stat_bonus_cost._element_resistance;

        return hero_stat;
    }


    //Hero Runes
    public bool Have_hero_Rune_From_Slot(Hero hero, int slot)
    {
        switch (slot)
        {
            case 1: if (hero._rune_1_id != 0) return true; break;
            case 2: if (hero._rune_2_id != 0) return true; break;
            case 3: if (hero._rune_3_id != 0) return true; break;
            case 4: if (hero._rune_4_id != 0) return true; break;
            case 5: if (hero._rune_5_id != 0) return true; break;
            case 6: if (hero._rune_6_id != 0) return true; break;
        }

        return false;
    }
    public int Hero_Rune_Id_By_Slot(Hero hero, int slot)
    {
        switch (slot)
        {
            case 1: return hero._rune_1_id;
            case 2: return hero._rune_2_id;
            case 3: return hero._rune_3_id;
            case 4: return hero._rune_4_id;
            case 5: return hero._rune_5_id;
            case 6: return hero._rune_6_id;
        }

        return 0;
    }


    //Update Hero From List
    public List<Hero> Update_Hero_From_List(Hero hero, List<Hero> heroes)
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            if (heroes[i]._name == hero._name)
            {
                heroes[i] = hero;
            }
        }

        return heroes;
    }


    //Lv Up Hero
    public Hero Lv_Up_Hero(Hero hero)
    {
        if (hero._exp >= Need_Exp_For_Lv_Up(hero._lv))
        {
            hero._exp -= Need_Exp_For_Lv_Up(hero._lv);
            hero._lv++;
        }

        return hero;
    }
    public int Need_Exp_For_Lv_Up(int lv_now)
    {
        int exp = _hero_db._hero_grow_stat._first_hero_lv_up_exp_cost;

        for (int i = 1; i <= lv_now; i++)
        {
            exp = exp * (100 + _hero_db._hero_grow_stat._next_hero_lv_up_exp_multiplier) / 100;
        }

        return exp;
    }
}
