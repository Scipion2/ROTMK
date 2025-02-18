using UnityEngine;
using UnityEngine.UI;

public class TutoButton : MonoBehaviour
{

    [Header("Graphic Tutorial Variables")]
        public Image[] TutoImage;
        private int ActualImage=0;

    //Essentials
        public void Start()
        {

            for(int i=1;i<TutoImage.Length;++i)
            {

                TutoImage[i].gameObject.SetActive(false);

            }

        }//Initialize the Tutorial display

    //Navigation

        public void previous()
        {

            TutoImage[ActualImage].gameObject.SetActive(false);
            if(ActualImage==0)
                ActualImage=TutoImage.Length-1;
            else
                ActualImage--;

            TutoImage[ActualImage].gameObject.SetActive(true);

        }//Get the previous Tutorial slide displayed


        public void next()
        {

            TutoImage[ActualImage].gameObject.SetActive(false);

            if(ActualImage==TutoImage.Length-1)
                ActualImage=0;
            else
                ActualImage++;

            TutoImage[ActualImage].gameObject.SetActive(true);

        }//Get the next Tutorial slide displayed

    

}
