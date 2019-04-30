using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GSM : MonoBehaviour
{
    public Camera Cam;
    public TextMeshPro Text;
    public TextMeshPro ScoreTxt;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    //public static GSM instance;


    void Awake()
    {
        God.GSM = this;
        Debug.Log(Application.persistentDataPath);
    }

    void Start()
    {
        ModelManager.BuildModel();
        ModelManager.BuildView();
        UpdateText();
    }

    
    
    //I can update the big screen covering text with this
    public void SetText(string txt)
    {
        Text.text = txt;
    }

    
    public void UpdateText()
    {
        ScoreTxt.text = "Score: " + ModelManager.Score + "\n" + "HP: " + ModelManager.HP;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene("Game");
        }

        //TileModel t = new TileModel(GameSettings.playerX, GameSettings.playerY);
    }
}
