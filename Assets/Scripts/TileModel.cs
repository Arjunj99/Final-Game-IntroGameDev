using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileModel {
    public Guid ID;
    [NonSerialized] public TileView View;
    public Guid Contents;
    public int X;
    public int Y;
    
    public TileModel(int x, int y) {
        ID = Guid.NewGuid();
        X = x;
        Y = y;
        ModelManager.AllTiles.Add(ID,this);
        if (!ModelManager.Tiles.ContainsKey(X))
            ModelManager.Tiles.Add(X,new Dictionary<int, TileModel>());
        ModelManager.Tiles[X].Add(Y,this);
    }

    public ActorModel GetContents() {
        return ModelManager.GetActor(Contents);
    }

    public void OnLoad() {
        if (!ModelManager.Tiles.ContainsKey(X))
            ModelManager.Tiles.Add(X,new Dictionary<int, TileModel>());
        ModelManager.Tiles[X].Add(Y,this);
    }

    public TileModel Neighbor(int x, int y) {
        return ModelManager.GetTile(X + x,Y + y);
    }
}
