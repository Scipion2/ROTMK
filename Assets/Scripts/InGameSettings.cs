using UnityEngine;

public class InGameSettings : MonoBehaviour
{
    
    public void Close()
    {

        Object.DestroyImmediate(this.gameObject);
        SettingsManager.instance.IsClosed();

    }//Close The Window

    public void ToMenu()
    {

        ScenesManager.instance.ToMenu();

    }//Go Back To The Game Menu

    public void ToSettings()
    {

        ScenesManager.instance.ToSettings();

    }//Go Back To Settings Scene

}
