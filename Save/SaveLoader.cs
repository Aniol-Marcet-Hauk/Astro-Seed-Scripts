using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class SaveLoader : MonoBehaviour
{
    public int saveNumber;
    private int level;
    public Vector3 FirstPlayerPosition;
    public Vector3 playerPos;
    public string fileName;
    public bool dataBool { get; private set; } = false;
    private SaveFileData data;
    [Space]
    public TMP_InputField nameInput;
    public float fileCreated = .92f;
    public float timer = 0;
    void Start()
    {
        SetData();
    }
    private void SetData()
    {

        data = SaveSystem.LoadData(saveNumber);
        Image im = GetComponent<Image>();
        Color temp = im.color;

        if (data == null)
        {
            dataBool = false;
            timer = 0;
            temp.a = .5f;
            if (im != null)im.color = temp;
            return;
        }

        fileName = data.name;
        if(nameInput != null) nameInput.text = fileName;
        level = data.level;
        playerPos.x = data.position[0];
        playerPos.y = data.position[1];
        playerPos.z = data.position[2];
        dataBool = true;
        timer = data.timer;

        temp.a = fileCreated;
        if (im != null) im.color = temp;


    }
    // this doesnt seem to take into account chapters
    public void ChangeFileName()
    {
        fileName = nameInput.text;
        if(dataBool == false)
        {
            return;
        }
        SaveSystem.Save(playerPos, level, fileName,0, saveNumber,timer);
    }
    public void OnCreate()
    {
        if(fileName == "")
        {
            fileName = "New Save " + saveNumber;
            
        }
        dataBool = true;
        SaveSystem.Save(FirstPlayerPosition, 1, fileName, 0,saveNumber,0);
        SetData();
    }
    public void OnLoad()
    {

        //MAKE SOMETHING FOR IT TO KNOW WHICH FILE OPENED IT
        //MAKE SOMETHING FOR IT TO KNOW WHICH FILE OPENED IT
        //MAKE SOMETHING FOR IT TO KNOW WHICH FILE OPENED IT
        //MAKE SOMETHING FOR IT TO KNOW WHICH FILE OPENED IT
        
        if (dataBool == true)
        {
            GameManager.gameManager.saveData = data;
            SceneManager.LoadScene(level);
            return;
        }
        if (fileName == "")
        {
            fileName = "New Save " + saveNumber;
            if (nameInput != null) nameInput.text = fileName;
        }
        dataBool = true;
        SaveSystem.Save(FirstPlayerPosition, 1, fileName, 0,saveNumber,timer);
        SetData();
        GameManager.gameManager.saveData = data;
        SceneManager.LoadScene(level);
    }
    public void OnDelete()
    {
        dataBool = false;
        SaveSystem.Delete(saveNumber);
        fileName = "";
        if (nameInput != null) nameInput.text = fileName;
        GameManager.gameManager.timeHasPassed = 0;
        
        
        timer = 0;

        Image im = GetComponent<Image>();
        Color temp = im.color;
        temp.a = .5f;
        im.color = temp;
    }
}
