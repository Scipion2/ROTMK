using UnityEngine;

public class Settings : MonoBehaviour
{
   
    public void OnFullScreenToggled()
    {

        SettingsManager.instance.SwitchFullScreen();

    }

    public void Return()
    {

        ScenesManager.instance.ToMenu();

    }

}
