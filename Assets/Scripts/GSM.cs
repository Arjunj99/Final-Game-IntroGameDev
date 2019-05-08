using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GSM : MonoBehaviour {
    public Camera Cam;
    public TextMeshPro Text;
    public TextMeshPro ScoreTxt;
    public SettingsManager SM;
    //public int monsterMovement = 0;
    //public int keyMovement = 0;
    //public int scoreMovement = 0;
    public int monsterRound = 2;
    public int keyRound = 2;
    public int scoreRound = 2;
    public int wallLimit = 5;
    public int currentMoves = 0;
    public bool hasKey = false;
    //public bool keyIsVisible = false;
    //public bool scoreIsVisible = false;
    public bool tryAgain = false;
    public bool LightSubtraction = false;

    void Awake() {
        God.GSM = this;
        //for (int i = 0; i < SM.MapContents.Count; i++)
        //{
        //    if (SM.MapContents[i] == ThingTypes.ScoreThing)
        //        scoreRound++;
        //}
        //scoreRound *= 2;


        if (God.Round % 2 == 0) {
            Debug.Log("SpawnMonster");
            God.MonsterCount++;
        } else {
            Debug.Log("SpawnGem");
            God.GemCount++;
        }

        for (int i = 0; i < God.MonsterCount; i++) {
            SM.MapContents.Add(ThingTypes.Monster);
        } 
        for (int i = 0; i < God.GemCount; i++) {
            SM.MapContents.Add(ThingTypes.ScoreThing);
        }
    }

    void Start() {
        ModelManager.BuildModel();
        ModelManager.BuildView();
        UpdateText();

        //for (int i = 0; i < SM.MapContents.Count; i++) {
        //    if (SM.MapContents[i] == ThingTypes.Monster) {
        //        monsterRound++;
        //    }
        //}
        //Debug.Log("There are " + monsterRound + " monsters");
        //monsterRound *= 2;
    }

    void Update() {
        if (currentMoves < 12) {
            monsterRound = 2;
            scoreRound = 2;
            keyRound = 2;
        } else if (currentMoves < 20) {
            monsterRound = 1;
            scoreRound = 1;
            keyRound = 1;
        } else {
            monsterRound = 0;
            scoreRound = 0;
            keyRound = 0;
        }
    }

    //I can update the big screen covering text with this
    public void SetText(string txt) {
        Text.text = txt;
    }
    
    public void UpdateText() {
        ScoreTxt.text = "Score: " + ModelManager.Score + "\n" + "HP: " + ModelManager.HP;
    }

    //public void Update() {
    //    if (Input.GetKeyDown(KeyCode.F12)) {
    //        ModelManager.HP = 3;
    //        ModelManager.Score = 0;
    //        SceneManager.LoadScene("Game");
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space) && tryAgain) {
    //        ModelManager.HP = 3;
    //        ModelManager.Score = 0;
    //        SceneManager.LoadScene("Game");
    //    }
    //}
}
