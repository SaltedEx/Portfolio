using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public GameObject player;
    public void SavePosition()
    {
        PlayerPrefs.SetFloat("Y", player.transform.localPosition.y);
        PlayerPrefs.SetFloat("X", player.transform.localPosition.x);
        PlayerPrefs.SetFloat("Z", player.transform.localPosition.z);
        Debug.Log(("X Axis: " + PlayerPrefs.GetFloat("X") + " Y Axis: " + PlayerPrefs.GetFloat("Y") + " Z Axis: " + PlayerPrefs.GetFloat("Z")));
    }

    public void LoadPosition()
    {
        player.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
    }

    public void LoadAutoPos()
    {
        player.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("AutoX"), PlayerPrefs.GetFloat("AutoY"), PlayerPrefs.GetFloat("AutoZ"));
    }
}
