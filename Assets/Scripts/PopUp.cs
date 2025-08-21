using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public GameObject popUp;

    public void ShowPopUp()
    {
        popUp.SetActive(true);
    }

    public void HidePopUp()
    {
        popUp.SetActive(false);
    }
}

