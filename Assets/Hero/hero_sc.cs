using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hero_sc : MonoBehaviour
{
    public class ef_from_fight
    {
        public string _name;
        public int _lv;
        public int _time;
    }


    [SerializeField] inf_db_sc _inf_db;


    [SerializeField]
    Hero _hero;
    [SerializeField]
    Hero_Stat _hero_stat;
    [SerializeField]
    bool _is_ally;
    [SerializeField]
    int _health_now, _speed_bar;
    [SerializeField]
    int _health, _attack, _defense, _speed, _crit_chance, _crit_damage, _accuracy, _resistance, _pierce, _armor, _lifesteal, _regeneration, _healing_power, _recovering_power, _crit_resistance, _crit_defense, _element_damage, _element_resistance;
    [SerializeField]
    int _sk_cd, _ul_cd, _ps_cd;

    void FixedUpdate()
    {
        if (_move == true)
        {
            Move_To_Target();
        }
    }

    #region Stat
    public void Spawn_Setting(string hero_name, bool is_allly, bool is_pvp)
    {
        _is_ally = is_allly;

        if (is_allly == true)
        {
            Load_Ally_Stat(hero_name);
        }
        else if (is_allly == false)
        {
            if (_inf_db._database._fight_db._is_pvp == false)
            {
                Load_Enemy_Bot_Stat(hero_name);
            }
            else if (_inf_db._database._fight_db._is_pvp == true)
            {
                Load_Enemy_Stat(hero_name);
            }
        }
    }
    void Load_Ally_Stat(string hero_name)
    {
        _hero = _inf_db._managers._hero_manager.Get_Hero(_inf_db._database._user_hero_db._heroes, hero_name);
        _hero_stat = _inf_db._managers._hero_manager.Hero_Stat_With_Runes(_inf_db._database._user_rune_db._runes, _hero);

        _health_now = _hero_stat._health;
        _speed_bar = 0;

        _health = _hero_stat._health;
        _attack = _hero_stat._attack;
        _defense = _hero_stat._defense;
        _speed = _hero_stat._speed;
        _crit_chance = _hero_stat._crit_chance;
        _crit_damage = _hero_stat._crit_damage;
        _accuracy = _hero_stat._accuracy;
        _resistance = _hero_stat._resistance;
        _pierce = _hero_stat._pierce;
        _armor = _hero_stat._armor;
        _lifesteal = _hero_stat._lifesteal;
        _regeneration = _hero_stat._regeneration;
        _healing_power = _hero_stat._healing_power;
        _recovering_power = _hero_stat._recovering_power;
        _crit_resistance = _hero_stat._crit_resistance;
        _crit_defense = _hero_stat._crit_defense;
        _element_damage = _hero_stat._element_damage;
        _element_resistance = _hero_stat._element_resistance;

        _sk_cd = _hero_stat._sk_pcd;
        _ul_cd = _hero_stat._ul_pcd;
        _ps_cd = _hero_stat._ps_pcd;
    }
    void Load_Enemy_Stat(string hero_name)
    {

    }
    void Load_Enemy_Bot_Stat(string hero_name)
    {
        _hero_stat = _inf_db._managers._hero_manager.Hero_Stat_For_PVE_Enemy(hero_name, _inf_db._database._fight_db._enemy_lv, _inf_db._database._fight_db._enemy_bonus);

        _health_now = _hero_stat._health;
        _speed_bar = 0;

        _health = _hero_stat._health;
        _attack = _hero_stat._attack;
        _defense = _hero_stat._defense;
        _speed = _hero_stat._speed;
        _crit_chance = _hero_stat._crit_chance;
        _crit_damage = _hero_stat._crit_damage;
        _accuracy = _hero_stat._accuracy;
        _resistance = _hero_stat._resistance;
        _pierce = _hero_stat._pierce;
        _armor = _hero_stat._armor;
        _lifesteal = _hero_stat._lifesteal;
        _regeneration = _hero_stat._regeneration;
        _healing_power = _hero_stat._healing_power;
        _recovering_power = _hero_stat._recovering_power;
        _crit_resistance = _hero_stat._crit_resistance;
        _crit_defense = _hero_stat._crit_defense;
        _element_damage = _hero_stat._element_damage;
        _element_resistance = _hero_stat._element_resistance;

        _sk_cd = _hero_stat._sk_pcd;
        _ul_cd = _hero_stat._ul_pcd;
        _ps_cd = _hero_stat._ps_pcd;
    }
    #endregion

    #region Effect
    [SerializeField]
    List<ef_from_fight> _effect;
    [SerializeField]
    Transform _ef_cont;
    public void Add_Ef(ef_from_fight ef)
    {
        bool have = false;
        int ef_num = 0;
        for (int i = 0; i < _effect.Count; i++)
        {
            if (_effect[i]._name == ef._name)
            {
                have = true;
                ef_num = i + 1;
                break;
            }
        }

        if (have == true)
        {
            if (_effect[ef_num - 1]._lv < ef._lv)
            {
                Add_Remove_Ef_Visual(false, _effect[ef_num - 1]._name, 0);
                _effect.RemoveAt(ef_num - 1);
                _effect.Add(ef);
                Add_Remove_Ef_Visual(true, ef._name, ef._lv);
            }
            else if (_effect[ef_num - 1]._lv == ef._lv && _effect[ef_num - 1]._time < ef._time)
            {
                _effect.RemoveAt(ef_num - 1);
                Add_Remove_Ef_Visual(false, _effect[ef_num - 1]._name, 0);
                _effect.Add(ef);
                Add_Remove_Ef_Visual(true, ef._name, ef._lv);
            }
        }
        else if (have == false)
        {
            _effect.Add(ef);
            Add_Remove_Ef_Visual(true, ef._name, ef._lv);
        }
    }
    void Add_Remove_Ef_Visual(bool add, string ef_name, int ef_lv)
    {
        if (add == true)
        {
            GameObject ef = new();
            ef.AddComponent<Image>();
            ef.GetComponent<Image>().sprite = _inf_db._managers._general_manager.Get_Ef_Sprite(ef_name, ef_lv);
            ef.name = ef_name;
            Instantiate(ef, _ef_cont);
        }
        else if (add == false)
        {
            int ef_num = 0;
            for (int i = 0; i < _ef_cont.childCount; i++)
            {
                if (_ef_cont.GetChild(i).name == ef_name)
                {
                    ef_num = i + 1; break;
                }
            }
            Destroy(_ef_cont.GetChild(ef_num - 1));
        }
    }
    #endregion

    #region Animation
    [SerializeField]
    Animator _animator;
    void Change_Animator_Parameter(string par_name, bool value)
    {
        _animator.SetBool(par_name, value);
    }
    #endregion

    #region Move
    [SerializeField]
    float _move_speed;
    [SerializeField]
    GameObject _move_target;
    [SerializeField]
    bool _move;
    void Set_Move_Target(GameObject target)
    {
        _move_target = target;
        _move = true;
    }
    void Move_To_Target()
    {
        transform.position = Vector3.MoveTowards(transform.position, _move_target.transform.position, _move_speed);
        if(transform.position == _move_target.transform.position)
        {
            _move = false;
            _move_target = null;
        }
    }
    #endregion

    #region Take Damage
    void Take_Damage()
    {
        
    }
    #endregion
}
