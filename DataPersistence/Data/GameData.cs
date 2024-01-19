using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public long lastUpdated;
    public int whatLevel;
    public Vector3 PlayerPostion;
    public string[] ItemNames;
    public int triggerTimes;

    //this dictionary is all for the collectibles, checkpoints and such, this is just a sample
    //Idk how the demo work ngl so yeah
    /*
     * public SerializableDictionary<string, bool> hospitalTriggers;
     */


    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load

    // Note: Please fill out ALL the necessary data in the demo and I really
    // cannot stress enough the importance of the video
    // If there's any bugs in the script, let me know so I can find a solution hopefully before April 30
    public GameData()
    {
        this.whatLevel = 0;
        PlayerPostion = Vector3.zero; //modifiable
        ItemNames = new string[12];
        this.triggerTimes = 0;

        //the rest of the video from 18:11 mostly requires to mess with the Demo Codes, so please watch it
        //as I don't want to mess with the Demo Codes
        /*
         * hospitalTriggers = new SerializableDictionary<string, bool>();
         */
    }

    public int GetPercentageComplete()
    {
        //sample code for percentage getting; in our game it's based on flag checks and events per area; here's a sample code for vine destruction if there were any vines that were flagged
        /* int totalDestroyed = 0;
           foreach (bool destroyed in vinesDestroyed.Values)
               {
                    if (destroyed)
                    {
                        totalCollected++;
                    }
                }
        */

        // ensuring we don't divide by 0 when calculating percentage; sample code again
        int percentageCompleted = -1; // can use float or int for this one; using int as an example
        /* if (vinesDestroyed.Count != 0)
            {
                percentageCompleted = (totalDestroyed * 100 / vinesDestroyed.Count);
            }
            return percentageCompleted;
        */
        return percentageCompleted;
    }
}
