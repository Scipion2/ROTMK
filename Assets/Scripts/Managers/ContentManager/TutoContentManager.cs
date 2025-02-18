using UnityEngine;

public class TutoContentManager : MonoBehaviour
{
    
    public static TutoContentManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }//Enable to call instance of this class from everywhere

}
