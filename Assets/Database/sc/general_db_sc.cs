using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "db/general_db")]
public class general_db_sc : ScriptableObject
{
    public task _task;

    public effect _effect;

    public User_General _new_user_general;
}

[Serializable]
public class task
{
    public List<string> _name;
    public List<int> _need_score;
    public List<reward> _box_reward;
}
[Serializable]
public class reward
{
    public List<string> _item_name;
    public List<int> _item_count;
}

[Serializable]
public class effect
{
    public List<string> _name;
    public List<int> _lv;
    public List<Sprite> _sprite;
}