using UnityEngine;
using TMPro;

public class main_pn_sc : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_resource_db_sc _user_resource_db;


    private void FixedUpdate()
    {
        Resource_Pn_Load();
    }


    [SerializeField] TextMeshProUGUI _money_txt, _gold_txt, _coin_txt, _energy_txt;
    void Resource_Pn_Load()
    {
        _money_txt.text = _user_resource_db._money + "";
        _gold_txt.text = _user_resource_db._gold + "";
        _coin_txt.text = _user_resource_db._coin + "";
        _energy_txt.text = _user_resource_db._energy_now + " | " + _user_resource_db._energy_mx;
    }


    [SerializeField] character_pn_sc _character_pn_sc;
    public void Main_B(string v)
    {
        if (v == "character")
        {
            _character_pn_sc.Character_Pn_Op_Cls("op");
        }
    }
}
