using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    // player's property
    public int level;
    public float moveSpeed;
    public float dashSpeed;
    public int numOfWall, numOfTrap;
    public float setWallCoolDown, setTrapCoolDown;
    public float wallExistTime;
    public float trapExistTime;
    public float moveSpeedPenalty;

    public string playerName = "***";
    public string[] leaderName = new string[10];
    public int[] leaderLevel = new int[10];
    public float[] leaderTime = new float[10];

    void Awake()
    {
        if (null == instance)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void SavePlayerProperty()
    {
        PlayerController P = PlayerController.instance;
        level = P.level;
        //moveSpeed = P.moveSpeed;
        //dashSpeed = P.dashSpeed;
        //numOfWall = P.numOfWall;
        //numOfTrap = P.numOfTrap;
        //setWallCoolDown = P.setWallCoolDown;
        //setTrapCoolDown = P.setTrapCoolDown;
        //wallExistTime = P.wallExistTime;
        //trapExistTime = P.trapExistTime;
        //moveSpeedPenalty = P.moveSpeedPenalty;
        Debug.Log("Property saved");
    }

    public void LoadPlayerProperty()
    {
        PlayerController.instance.level = level;
        PlayerController.instance.leaderName = leaderName;
        PlayerController.instance.leaderLevel = leaderLevel;
        PlayerController.instance.leaderTime = leaderTime;
        //PlayerController.instance.moveSpeed = moveSpeed;
        //PlayerController.instance.dashSpeed = dashSpeed;
        //PlayerController.instance.numOfWall = numOfWall;
        //PlayerController.instance.numOfTrap = numOfTrap;
        //PlayerController.instance.setWallCoolDown = setWallCoolDown;
        //PlayerController.instance.setTrapCoolDown = setTrapCoolDown;
        //PlayerController.instance.wallExistTime = wallExistTime;
        //PlayerController.instance.trapExistTime = trapExistTime;
        //PlayerController.instance.moveSpeedPenalty = moveSpeedPenalty;
        Debug.Log("Property loaded");
    }
}
