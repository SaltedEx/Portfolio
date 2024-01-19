using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSave : MonoBehaviour
{
    float AutoTime = 14;
    public GameObject player;
    public Text alert;

    // Update is called once per frame
    void Update()
    {
        if (AutoTime > 0)
        {
            AutoTime -= Time.deltaTime;
        }
        else
        {
            PlayerPrefs.SetFloat("AutoX", player.transform.localPosition.x);
            PlayerPrefs.SetFloat("AutoY", player.transform.localPosition.y);
            PlayerPrefs.SetFloat("AutoZ", player.transform.localPosition.z);

            alert.text = "Location Autosaved!";
            AutoTime = 14;
            Debug.Log(("X Axis: " + PlayerPrefs.GetFloat("AutoX") + " Y Axis: " + PlayerPrefs.GetFloat("AutoY") + " Z Axis: " + PlayerPrefs.GetFloat("AutoZ")));
        }

        if (AutoTime < 11)
        {
            alert.text = "Load from Last AutoSave";
        }
    }
}
