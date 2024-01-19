using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "db/fight_db")]
public class fight_db_sc : ScriptableObject
{
    public bool _is_pvp;

    public List<GameObject> _ally_team;
    public List<GameObject> _enemy_team;

    #region PVE
    public int _enemy_lv;
    public int _enemy_bonus;
    #endregion
}
