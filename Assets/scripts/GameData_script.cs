using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "scriptableObject")]
public class GameData_script : ScriptableObject
{
    [Tooltip("optianal")] //didnt impamented yet
    public int interval_xp;
    public Sprite [] sprites;
    

    public GameData[] levels;

    



    
}
[System.Serializable]
public class GameData
{ 
    //to do: get set
    
    public int xp;
    public string title;

}
