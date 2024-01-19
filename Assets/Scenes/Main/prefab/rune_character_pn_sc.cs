using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rune_character_pn_sc : MonoBehaviour
{
    [SerializeField] character_pn_sc _character_pn_sc;

    [SerializeField] Rune _rune;


    public void Spawn_Setting(Rune rune, character_pn_sc character_pn_sc)
    {
        _character_pn_sc = character_pn_sc;
        _rune = rune;

        Art_Load();
        Star_Load();
        Lv_Slot_Load();
    }

    [SerializeField] Image _rune_cion;
    void Art_Load()
    {
        _rune_cion.sprite = _character_pn_sc._inf_db._managers._rune_manager.Get_Rune_Sprite(_rune._rare, _rune._set);
    }

    [SerializeField] List<Image> _stars;
    void Star_Load()
    {
        int star = _rune._star;

        for (int i = 0; i < star; i++)
        {
            _stars[i].gameObject.SetActive(true);
        }
    }

    [SerializeField] TextMeshProUGUI _lv_txt, _slot_txt;
    void Lv_Slot_Load()
    {
        _lv_txt.text = "+" + _rune._lv;
        _slot_txt.text = _rune._slot + "";
    }


    public void Rune_Character_Pn_B()
    {
        _character_pn_sc.Rune_Inf_Pn_Op(_rune._rune_id);
    }
}