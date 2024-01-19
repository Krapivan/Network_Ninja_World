public static class sk_inf
{

    public static string Sk_Inf(string sk_type, string hero_name)
    {
        string inf = "";

        switch (hero_name)
        {
            case "Naruto [Genin]":
                if (sk_type == "aa")
                {
                    inf = "aa";
                }
                else if (sk_type == "sk")
                {
                    inf = "sk";
                }
                else if (sk_type == "ul")
                {
                    inf = "ul";
                }
                else if (sk_type == "ps")
                {
                    inf = "ps";
                }
                break;
        }


        return inf;
    }
}
