using UnityEngine;

[CreateAssetMenu(menuName = "db/user_resource_db")]
public class user_resource_db_sc : ScriptableObject
{
    public string _user_name;
    public string _last_login_date;
    public int _user_id;
    public int _money;
    public int _gold;
    public int _coin;
    public int _energy_now;
    public int _energy_mx;
}