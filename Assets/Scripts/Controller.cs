using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
    private Queue<GameAction> Actions = new Queue<GameAction>();
    
    void Awake() {
        God.C = this;

        //StartCoroutine(MainLoop());
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.F12)) {
            ModelManager.HP = 3;
            ModelManager.Score = 0;
            God.Round = 0;
            God.MonsterCount = 0;
            God.GemCount = 0;
            SceneManager.LoadScene("Game");
        }

        if (Input.GetKeyDown(KeyCode.Space) && God.GSM.tryAgain) {
            ModelManager.HP = 3;
            ModelManager.Score = 0;
            God.Round = 0;
            God.MonsterCount = 0;
            God.GemCount = 0;
            SceneManager.LoadScene("Game");
        }
        //ASK ABBY ABOUT GAMEFEEL
        ReadInputs();
        StartCoroutine(ResolveActions());
    }
    //ASK ABBY ABOUT GAMEFEEL
    //public IEnumerator MainLoop() {
        //while (true) {
        //    ReadInputs();
        //    yield return StartCoroutine(ResolveActions());
        //}
    //}
    
    void ReadInputs() {
        if (IM.Pressed(Inputs.Left)) {
            MsgAll(new EventMsg(EventType.MonsterMove, null, 0, Inputs.None));
            if (!God.GSM.hasKey) {
                MsgAll(new EventMsg(EventType.KeyMove, null, 0, Inputs.None));
            }
            MsgAll(new EventMsg(EventType.ScoreMove, null, 0, Inputs.None));
            MsgAll(new EventMsg(Inputs.Left));
        } else if (IM.Pressed(Inputs.Right)) {
            MsgAll(new EventMsg(EventType.MonsterMove, null, 0, Inputs.None));
            if (!God.GSM.hasKey) {
                MsgAll(new EventMsg(EventType.KeyMove, null, 0, Inputs.None));
            }
            MsgAll(new EventMsg(EventType.ScoreMove, null, 0, Inputs.None));
            MsgAll(new EventMsg(Inputs.Right));
        } else if (IM.Pressed(Inputs.Up)) {
            MsgAll(new EventMsg(EventType.MonsterMove, null, 0, Inputs.None));
            if (!God.GSM.hasKey) {
                MsgAll(new EventMsg(EventType.KeyMove, null, 0, Inputs.None));
            }
            MsgAll(new EventMsg(EventType.ScoreMove, null, 0, Inputs.None));
            MsgAll(new EventMsg(Inputs.Up));
        } else if (IM.Pressed(Inputs.Down)) {
            MsgAll(new EventMsg(EventType.MonsterMove, null, 0, Inputs.None));
            if (!God.GSM.hasKey) {
                MsgAll(new EventMsg(EventType.KeyMove, null, 0, Inputs.None));
            }
            MsgAll(new EventMsg(EventType.ScoreMove, null, 0, Inputs.None));
            MsgAll(new EventMsg(Inputs.Down));
        }

        //if (Input.GetKeyUp(KeyCode.S)) {
        //    ModelManager.SaveGame();
        //}
        //if (Input.GetKeyUp(KeyCode.L)) {
        //    ModelManager.PendingSave = ModelManager.LoadGame();
        //    if (ModelManager.PendingSave != null)
        //        SceneManager.LoadScene(0);
        //    else
        //        Debug.Log("NO FILE TO LOAD");
        //}
        //if (Input.GetKeyUp(KeyCode.D)) {
        //    ModelManager.DeleteSave();
        //}
        //if (Input.GetKeyUp(KeyCode.Space)) {
        //    StartCoroutine(ModelManager.CaptureScreenshot("X"));
        //}
    }

    public void MsgAll(EventMsg msg) {
        foreach (ActorModel am in ModelManager.GetActors()) {
            am.TakeMsg(msg);
        } 
    }

    public void AddAction(GameAction a) {
        Actions.Enqueue(a);
    }

    public IEnumerator ResolveActions() {
        while (Actions.Count > 0) {
            yield return StartCoroutine(Actions.Dequeue().Run());
        }
    }
}
