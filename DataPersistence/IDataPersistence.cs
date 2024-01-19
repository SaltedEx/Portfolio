using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);

    void SaveData(GameData data);

    // put these beside monobehavior like this
    // : MonoBehaviour, IDataPersistence

    // then add the method the interface defines; i.e.
    // public void LoadData(GameData data)
    //{
    //  this.variable = data.variable
    //}
    //
    //  OR/AND
    //
    //  public void SaveData(GameData data)
    //{
    //  data.variable = this.variable
    //}
    //
    // This is to ensure that we can transfer data easier
}
