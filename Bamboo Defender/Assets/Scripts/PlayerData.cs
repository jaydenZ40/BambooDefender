using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public float moveSpeed;
    public float dashSpeed;
    public int numOfWall, numOfTrap;
    public float setWallCoolDown, setTrapCoolDown;
    public float wallExistTime;
    public float trapExistTime;
    public float moveSpeedPenalty;
    public string playerName;
    public string[] leaderName = new string[10];
    public int[] leaderLevel = new int[10];
    public float[] leaderTime = new float[10];

    public PlayerData(PlayerController player)
    {
        if (level < player.level)
        {
            level = player.level;
            moveSpeed = player.moveSpeed;
            dashSpeed = player.dashSpeed;
            numOfWall = player.numOfWall;
            numOfTrap = player.numOfTrap;
            setWallCoolDown = player.setWallCoolDown;
            setTrapCoolDown = player.setTrapCoolDown;
            wallExistTime = player.wallExistTime;
            trapExistTime = player.trapExistTime;
            moveSpeedPenalty = player.moveSpeedPenalty;
            playerName = player.playerName;
            leaderName = player.leaderName;
            leaderLevel = player.leaderLevel;
            leaderTime = player.leaderTime;
        }
    }
}
