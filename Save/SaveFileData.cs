using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public struct Chapter2
{

    public bool bossFight;
    public float[] bossPosition;
    public int cutSceneNum; // for the falling rocks

    public Chapter2(bool _bossFight, Vector3 _bossPosition, int _cutSceneNum)
    {
        bossFight = _bossFight;
        bossPosition = new float[3];
        bossPosition[0] = _bossPosition.x;
        bossPosition[0] = _bossPosition.y;
        bossPosition[0] = _bossPosition.z;
        cutSceneNum = _cutSceneNum;
    }

}
[System.Serializable]
public struct Chapter3Boss
{

    
    public int fase;

    public Chapter3Boss(int _fase)
    {
        fase = _fase;
    }
}
[System.Serializable]
public struct Chapter4
{


    public float[] gravity;
    public int cutSceneNum; //for the state of gravity and liquid


    public Chapter4(Vector3 _gravity, int _cutSceneNum)
    {
        gravity = new float[3];
        gravity[0] = _gravity.x;
        gravity[1] = _gravity.y;
        gravity[2] = _gravity.z;
        cutSceneNum = _cutSceneNum;
    }
}
 

[Serializable]
public class SaveFileData
    {
    public int saveFile;
    public int[] saveFileDateCreated;
    public string name;
    public int level;
    public float[] position;
    public int cutScene;
    public float timer;

    //
    public Chapter2 chp2;
    public Chapter3Boss chp3Boss;
    public Chapter4 chp4;
    //ALL OF LEVEL SPECIFIC STUFF LIKE GRAVITY
    public SaveFileData(Vector3 player, int _level, string _name, int _cutScene, int _saveNum,float _timer)
    {
        name = _name;
        level = _level;
        position = new float[3];
        position[0] = player.x;
        position[1] = player.y;
        position[2] = player.z;
        cutScene = _cutScene;
        saveFile = _saveNum;
        timer = _timer;
    }
    public SaveFileData(Vector3 player, int _level, string _name, int _cutScene, int _saveNum, float _timer, Chapter2 _chp2)
    {
        name = _name;
        level = _level;
        position = new float[3];
        position[0] = player.x;
        position[1] = player.y;
        position[2] = player.z;
        cutScene = _cutScene;
        saveFile = _saveNum;
        timer = _timer;
        chp2 = _chp2;
    }
    public SaveFileData(Vector3 player, int _level, string _name, int _cutScene, int _saveNum, float _timer, Chapter3Boss _chp3Boss)
    {
        name = _name;
        level = _level;
        position = new float[3];
        position[0] = player.x;
        position[1] = player.y;
        position[2] = player.z;
        cutScene = _cutScene;
        saveFile = _saveNum;
        timer = _timer;
        chp3Boss = _chp3Boss;
    }
    public SaveFileData(Vector3 player, int _level, string _name, int _cutScene, int _saveNum, float _timer, Chapter4 _chp4)
    {
        name = _name;
        level = _level;
        position = new float[3];
        position[0] = player.x;
        position[1] = player.y;
        position[2] = player.z;
        cutScene = _cutScene;
        saveFile = _saveNum;
        timer = _timer;
        chp4 = _chp4;
    }



}


