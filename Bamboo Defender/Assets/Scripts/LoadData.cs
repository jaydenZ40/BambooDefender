using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    void Start()
    {
        PlayerData data = SaveAndLoad.Load();

        if (data != null)
        {
            GameManager.instance.level = data.level;
            GameManager.instance.playerName = data.playerName;
            GameManager.instance.leaderName = data.leaderName;
            GameManager.instance.leaderLevel = data.leaderLevel;
            GameManager.instance.leaderTime = data.leaderTime;
            //GameManager.instance.moveSpeed = data.moveSpeed;
            //GameManager.instance.dashSpeed = data.dashSpeed;
            //GameManager.instance.numOfWall = data.numOfWall;
            //GameManager.instance.numOfTrap = data.numOfTrap;
            //GameManager.instance.setWallCoolDown = data.setWallCoolDown;
            //GameManager.instance.setTrapCoolDown = data.setTrapCoolDown;
            //GameManager.instance.wallExistTime = data.wallExistTime;
            //GameManager.instance.trapExistTime = data.trapExistTime;
            //GameManager.instance.moveSpeedPenalty = data.moveSpeedPenalty;

            Debug.Log("Saved level is " + data.level);
            Debug.Log("Player's name is " + data.playerName);
        }
        
    }

}
