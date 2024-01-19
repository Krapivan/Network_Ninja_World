using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "db/user_story_db")]
public class user_story_db_sc : ScriptableObject
{
    public int _chosen_chapter_num;
    public int _chosen_part_num;

    public List<string> _chapter_name;
    public List<User_Chapter_Parts> _chapter_parts;
}
