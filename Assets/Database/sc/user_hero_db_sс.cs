using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "db/user_hero_db")]
public class user_hero_db_sñ : ScriptableObject
{
    public List<User_Hero> _heroes;

    public string _character_pn_ch_hero_name;

    public User_Hero Get_User_Hero(string name)
    {
        for (int i = 0; i < _heroes.Count; i++)
        {
            if (_heroes[i]._name == name)
            {
                return _heroes[i];
            }
        }

        return null;
    }
}
