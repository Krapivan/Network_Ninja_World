using System;
using System.Collections.Generic;

[Serializable]
public class User_Story
{
    public string _user_id;

    public List<string> _chapter_name;
    public List<User_Chapter_Parts> _chapter_parts;
}
[Serializable]
public class User_Chapter_Parts
{
    public List<string> _part_name;
    public List<bool> _part_comp;
}

