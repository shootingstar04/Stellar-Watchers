using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPUI : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject currentSP;
    GameObject currentSPText;

    private int currentSPValue = 5;
    private int maxSPValue = 5;

    void Start()
    {
        currentSP = GameObject.Find("Current SP");
        currentSPText = GameObject.Find("Current SP Text");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) refresh_sp(-1);
        else if (Input.GetKeyDown(KeyCode.P)) refresh_sp(1);
    }

    bool refresh_sp(int dtSP) 
    {
        if (currentSPValue + dtSP >= 0 && currentSPValue + dtSP < maxSPValue + 1)
        {
            currentSPValue += dtSP;

            currentSPText.GetComponent<Text>().text = currentSPValue.ToString();


            StopCoroutine(smoth_change());
            StartCoroutine(smoth_change());

            return true;
        }
        else return false;
    }

    IEnumerator smoth_change()
    {
        if ((int) (currentSP.GetComponent<Image>().fillAmount * maxSPValue) != currentSPValue)
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
