using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;
    public Image marck1, marck2, marck3;

   
    public void SetFillValue(int max, float cur) 
    {
        float fillAmount = cur / (float)max;
        mask.fillAmount = fillAmount;
    }

    public void SetMarck(int i) 
    {
        if (i == 0)
        {
            marck1.enabled = true;
            marck2.enabled = false;
            marck3.enabled = false;

        } else if (i == 1)
        {
            marck1.enabled = false;
            marck2.enabled = true;
            marck3.enabled = false;

        } else if (i == 2) 
        {
            marck1.enabled = false;
            marck2.enabled = false;
            marck3.enabled = true;

        }
    }
}
