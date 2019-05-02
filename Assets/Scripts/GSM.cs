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
    public int monsterMovement = 0;
    public int keyMovement = 0;
    public int scoreMovement = 0;
    public int monsterRound = 3;
    public Sprite black;
    public Sprite grey;
    public Sprite lightgrey;
    public int wallLimit = 5;
    public RuntimeAnimatorController animator;
    public RuntimeAnimatorController walkingAnimation;
    public AudioClip StompClip;
    public AudioSource AS;
    public bool hasKey = false;
    public bool keyIsVisible = false;
    public bool scoreIsVisible = false;
    public List<int> wallPreset1 = new List<int>();
    public List<int> wallPreset2 = new List<int>();
    public List<int> wallPreset3 = new List<int>();
    public List<int> wallPreset4 = new List<int>();
    public List<int> wallPreset5 = new List<int>();
    public bool tryAgain = false;
    //public bool keyIsVisible = false;
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
            ModelManager.HP = 4;
            SceneManager.LoadScene("Game");
        }

        if (Input.GetKeyDown(KeyCode.Space) && tryAgain)
        {
            ModelManager.HP = 4;
            SceneManager.LoadScene("Game");
        }

        //TileModel t = new TileModel(GameSettings.playerX, GameSettings.playerY);
    }
}
