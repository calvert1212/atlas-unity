using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkOpen : MonoBehaviour
{
    public void openLinkedin()
    {
        Application.OpenURL("https://www.linkedin.com/in/slash9chadcalvert/");
    }

    public void openGithub()
    {
        Application.OpenURL("https://github.com/calvert1212/");
    }
    
    public void openFacebook()
    {
        Application.OpenURL("https://www.facebook.com/100083535186197/");
    }

    public void openGmail()
    {
        Application.OpenURL("mailto:calvert1212@gmail.com");
    }
}
