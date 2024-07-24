using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPUI : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject currentSP;
    GameObject maxSP;
    GameObject currentSPText;

    private int currentSPValue = 5;
    private int maxSPValue = 5;

    public static SPUI Instance;
    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(this);
        }

        currentSP = GameObject.Find("Current SP");
        maxSP = GameObject.Find("Max SP");
        currentSPText = GameObject.Find("Current SP Text");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool refresh_sp(int SP)
    {
        if (SP >= 0 && SP < maxSPValue + 1)
        { 
            currentSPValue = SP;

            currentSPText.GetComponent<Text>().text = currentSPValue.ToString();


            StopCoroutine(smoth_change());
            StartCoroutine(smoth_change());

            return true;
        }
        else return false;
    }
    public void add_max_SP(int MaxSP)
    {
        if (MaxSP < 8)
        {
            maxSPValue = MaxSP;

            refresh_sp(MaxSP);

            maxSP.GetComponent<ChangeImage>().set_image(MaxSP - 5);
            currentSP.GetComponent<ChangeImage>().set_image(MaxSP - 5);
        }
    }

    IEnumerator smoth_change()
    {
        if ((currentSP.GetComponent<Image>().fillAmount * maxSPValue) != currentSPValue)
        {
            float changeValue = (currentSPValue / (float)maxSPValue - currentSP.GetComponent<Image>().fillAmount) / 10;

            while (Mathf.Abs((currentSP.GetComponent<Image>().fillAmount * maxSPValue) - currentSPValue) > 0.1)
            {
                currentSP.GetComponent<Image>().fillAmount += changeValue;
                
                if (Mathf.Abs((currentSP.GetComponent<Image>().fillAmount * maxSPValue) - currentSPValue) < 0.1)
                {
                    currentSP.GetComponent<Image>().fillAmount = currentSPValue / (float) maxSPValue;
                }

                yield return new WaitForSeconds(1/60.0f);
            }
        }
    }

}
