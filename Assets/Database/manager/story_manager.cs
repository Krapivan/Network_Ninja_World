using System.Collections.Generic;
using UnityEngine;

public class story_manager : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;

    public bool Is_Open_Part(List<User_Chapter_Parts> chapter_parts, int chapter_num, int part_num)
    {
        if (_inf_db._database._user_story_db._chapter_parts[chapter_num - 1]._part_comp[part_num - 1] == true)
        {
            return true;
        }
        else if (_inf_db._database._user_story_db._chapter_parts[chapter_num - 1]._part_comp[part_num - 1] == false)
        {
            if (part_num == 1 && chapter_num == 1)
            {
                return true;
            }
            else if (part_num == 1 && chapter_num != 1)
            {
                if (_inf_db._database._user_story_db._chapter_parts[chapter_num - 2]._part_comp[_inf_db._database._user_story_db._chapter_parts[chapter_num - 2]._part_name.Count - 1] == true)
                {
                    return true;
                }
            }
            else if (part_num != 1)
            {
                if (_inf_db._database._user_story_db._chapter_parts[chapter_num - 1]._part_comp[part_num - 2] == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Sprite Part_Point_Art(int chapter_num, int part_num)
    {
        return _inf_db._database._story_db._chapter_parts[chapter_num - 1]._part_point_art[part_num - 1];
    }
}
