using System;
using System.Collections.Generic;

[Serializable]
public class User_General
{
    /*
    public User_General() 
    {
        _user_id = 0;
        _user_name = "";
        _last_login_date = "";

        //resource
        _user_resource._money = 0;
        _user_resource._gold = 0;
        _user_resource._coin = 0;
        _user_resource._energy_now = 30;
        _user_resource._energy_mx = 30;

        //item
        _user_resource._item._ramen = 0;
        _user_resource._item._chakra = 0;
        _user_resource._item._auto_ticket = 0;
        _user_resource._item._scroll = 0;
        _user_resource._item._element_mark_name = new() {"fire", "wind", "lightning", "earth", "water"};
        _user_resource._item._element_mark = new() { 0, 0, 0, 0, 0 };

        //task
        _user_task._comp_task_count = 0;
        _user_task._name = new() {"level up rune", "level  up hero", "play arena", "play rune dungeon", "play element mark dungeon", "play story", "play tower", "summon", "summon4ik"};
        _user_task._score = new() { 0, 0, 0, 0, 0, 0, 0, 0, 0};
        _user_task._box_take = new() {false, false, false, false, false};
    }
    */
    public string _user_id;
    public string _user_name;
    public string _last_login_date;

    public User_Resource _resource = new();
    public User_Daily_Task _daily_task = new();
}

[Serializable]
public class User_Resource
{
    public int _money;
    public int _gold;
    public int _coin;
    public int _energy_now;
    public int _energy_mx;

    public User_Item _item;

    [Serializable]
    public class User_Item
    {
        public int _ramen;
        public int _chakra;
        public int _auto_ticket;
        public int _scroll;

        public int _fire_mark;
        public int _wind_mark;
        public int _lightnig_mark;
        public int _earth_mark;
        public int _water_mark;
    }
}

[Serializable]
public class User_Daily_Task
{
    public int _comp_task_count;
    public List<User_Task> _task = new();
    public List<Daily_Box> _daily_box = new();

    [Serializable]
    public class User_Task
    {
        public string _name;
        public int _score;
    }
    [Serializable]
    public class Daily_Box
    {
        public string _rare;
        public bool _take;
    }
}