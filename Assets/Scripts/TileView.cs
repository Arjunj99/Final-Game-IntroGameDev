using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour {
    public TileModel Model;
    //public bool PlayerWasHere = false;
    public int playerDuration = 0;
    private int playerTrail = 6;

    public void Setup(TileModel m) {
        Model = m;
        m.View = this;
        transform.position = new Vector3(m.X,m.Y,0);
    }

    public void Update() {
        DecreaseVision(this);
        CheckIfPlayer(this);
        //ControllerCheck(this);
    }

    public void DecreaseVision(TileView t) {
        if (gameObject.transform.Find("Player") != true) {
            t.GetComponentInChildren<SpriteRenderer>().sprite = God.SM.black;
        }
    }

    public void CheckForObjects(TileView t) {
        TileModel tm = ModelManager.GetTile(new Point(Model.X - 1, Model.Y - 1));
        Debug.Log(tm.View.name);
    }

    public void CheckIfPlayer(TileView t) {
        if(t.transform.Find("Player")) {
            t.playerDuration = playerTrail;
            t.GetComponentInChildren<Animator>().enabled = false;
        } else if (!t.transform.Find("Monster") || !t.transform.Find("RedKey") || !t.transform.Find("ScoreThing")) {
            t.GetComponentInChildren<Animator>().enabled = false;
        }

        if (t.playerDuration > 0 && !t.transform.Find("Player")) {
            t.GetComponentInChildren<SpriteRenderer>().sprite = God.SM.grey;
        }
    }

    public void ControllerCheck(TileView t) {
        //if (God.GSM.LightSubtraction) {
            if (t.playerDuration > 0) {
                Debug.Log(t.Model.X + "/" + t.Model.Y + " " + t.playerDuration);
                t.playerDuration--;
                God.GSM.LightSubtraction = false;
            //}
        }
    }
}
