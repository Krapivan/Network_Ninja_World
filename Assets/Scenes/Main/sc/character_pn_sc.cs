using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class character_pn_sc : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    [SerializeField] user_resource_db_sc _user_resource_db;
    [SerializeField] hero_db_sc _hero_db;
    [SerializeField] user_hero_db_sñ _user_hero_db;


    [SerializeField] GameObject _character_pn;
    public void Character_Pn_Op_Cls(string v)
    {
        if (v == "op")
        {
            _character_pn.SetActive(true);
            Character_Pn_Load();
        }
        else if (v == "cls")
        {
            _character_pn.SetActive(false);
        }
    }


    [SerializeField] GameObject _har_pn, _rune_pn, _sk_pn;
    public void Inf_Pn_B(string pn_name)
    {
        _har_pn.SetActive(false);
        _rune_pn.SetActive(false);
        _sk_pn.SetActive(false);

        if (pn_name == "har")
        {
            _har_pn.SetActive(true);
            Har_Pn_Load(_user_hero_db._character_pn_ch_hero_name);
        }
        else if (pn_name == "rune")
        {
            _rune_pn.SetActive(true);
        }
        else if (pn_name == "sk")
        {
            _sk_pn.SetActive(true);
        }
    }


    void Character_Pn_Load()
    {
        _inf_db._user_command._hero_command_sc.Update_Db(_user_resource_db._user_id);

        if (_user_hero_db._character_pn_ch_hero_name == null)
        {
            _user_hero_db._character_pn_ch_hero_name = _user_hero_db._heroes[0]._name;
        }

        _har_pn.SetActive(false);
        _rune_pn.SetActive(false);
        _sk_pn.SetActive(false);
        _har_pn.SetActive(true);

        Har_Pn_Load(_user_hero_db._character_pn_ch_hero_name);
        Load_Hero(_user_hero_db._character_pn_ch_hero_name);
    }
    [SerializeField] Image _hero_art, _hero_rare;
    [SerializeField] TextMeshProUGUI _hero_name;
    void Load_Hero(string hero_name)
    {
        Hero_Sprite hero_sprite = _hero_db.Get_Hero_Sprite(hero_name);
        Hero_Stat hero_stat = _hero_db.Get_Hero_Stat(hero_name);

        _hero_art.sprite = hero_sprite._art;
        _hero_rare.sprite = _hero_db.Get_Rare_Art(hero_stat._rare);
        _hero_name.text = hero_stat._name;
    }


    [SerializeField] TextMeshProUGUI _hero_lv, _hero_exp, _har_1, _har_2;
    void Har_Pn_Load(string hero_name)
    {
        User_Hero user_hero = _user_hero_db.Get_User_Hero(hero_name);
        Hero_Stat hero_stat = _hero_db.Get_Hero_Stat(hero_name);

        _hero_lv.text = user_hero._lv + "";
        _hero_exp.text = user_hero._exp + " | " + user_hero._exp_need;
        _har_1.text = "hp : " + hero_stat._health + "\n" +
            "atk : " + hero_stat._attack + "\n" +
            "def : " + hero_stat._defense + "\n" +
            "spd : " + hero_stat._speed;
        _har_2.text = "crr" + hero_stat._crit_chance + "\n" +
            "crd : " + hero_stat._crit_damage + "\n" +
            "acc : " + hero_stat._accuracy + "\n" +
            "res : " + hero_stat._resistance;
    }
    void Rune_Pn_Load()
    {

    }
}
