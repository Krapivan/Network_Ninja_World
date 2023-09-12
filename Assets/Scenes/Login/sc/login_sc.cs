using UnityEngine;
using TMPro;
using Mirror;

public class login_sc : MonoBehaviour
{
    [SerializeField] inf_db_sc _inf_db;
    bool _action_is_login;


    private void Start()
    {
        _action_is_login = true;
    }


    [SerializeField] TMP_InputField _login_field, _password_field;
    [SerializeField] TextMeshProUGUI _action_logo, _connection_b_txt, _change_action_b_txt;
    public void Change_Action_B()
    {
        if (_action_is_login == true)
        {
            _action_is_login = false;
            _action_logo.text = "Regestration";
            _change_action_b_txt.text = "login";
            _connection_b_txt.text = "Regestration";
        }
        else if (_action_is_login == false)
        {
            _action_is_login = true;
            _action_logo.text = "Login";
            _change_action_b_txt.text = "regestration";
            _connection_b_txt.text = "Login";
        }
    }


    public void Connected_B()
    {
        if (_login_field.text.Length >= 5 && _password_field.text.Length >= 5)
        {
            user_login_setting user_login_setting = new()
            {
                _is_login = _action_is_login,
                _login = _login_field.text,
                _password = _password_field.text
            };
            _inf_db._user_login_setting = user_login_setting;

            NetworkManager.singleton.networkAddress = "26.111.73.141";

            if (_inf_db._user_login_setting._login == "server" && _inf_db._user_login_setting._password == "server")
            {
                NetworkManager.singleton.StartHost();
            }
            else
            {
                NetworkManager.singleton.StartClient();
            }
        }
    }
}
