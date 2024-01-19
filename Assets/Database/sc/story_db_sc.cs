using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "db/story_db")]
public class story_db_sc : ScriptableObject
{
    public List<string> _chapter_name;
    public List<chapter_parts> _chapter_parts;
}
[Serializable]
public class chapter_parts
{
    public List<string> _part_name;

    public List<int> _part_energy_cost;

    public List<Sprite> _part_point_art;

    public List<part_enemy> _part_enemy;
    public List<int> _part_enemy_lv;
    public List<int> _part_enemy_bonus;

    public List<reward> _part_reward;
}
[Serializable]
public class part_enemy
{
    public List<string> _enemy_name;
}
