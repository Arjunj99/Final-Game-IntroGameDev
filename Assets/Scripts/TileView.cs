using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    public TileModel Model;

    public void Setup(TileModel m)
    {
        Model = m;
        m.View = this;
        transform.position = new Vector3(m.X,m.Y,0);
//        God.GSM.AllTiles.Add(this);
//        if (!God.GSM.Tiles.ContainsKey(m.X))
//            God.GSM.Tiles.Add(m.X,new Dictionary<int, TileView>());
//        God.GSM.Tiles[m.X].Add(m.Y,this);
    }

    public void Start()
    {
        //CheckForObjects(this);
    }

    public void Update()
    {
        IncreaseVision(this);
    }

    public void IncreaseVision(TileView t) {
        if (gameObject.transform.Find("Player") != true) {
            //Debug.Log("KILL");
            t.GetComponentInChildren<SpriteRenderer>().sprite = God.GSM.invert;
            //Debug.Log(gameObject.transform.localPosition);
        }
        //if(t.Model.Neighbor(1,1).View.transform.Find(t.Model.Neighbor(1,1).View.name) == true) {
        //    Debug.Log("Monster Nearby");
        //}
    }

    public void CheckForObjects(TileView t) {
        TileModel tm = ModelManager.GetTile(new Point(Model.X - 1, Model.Y - 1));
        Debug.Log(tm.View.name);
    }



    
    
}
