//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PathTile {
//    public Vector2Int Tile;
//    public PathTile CameFrom;
//    public int FromStart;
//    public int FromEnd;
//    public int Value;

//    public PathTile(Vector2Int tile, PathTile cameFrom, Vector2Int dest) {
//        Tile = tile;
//        CameFrom = cameFrom;
//        FromStart = CameFrom != null ? CameFrom.FromStart + 1 : 0;
//        FromEnd = Mathf.Abs(Tile.x - dest.x) + Mathf.Abs(Tile.y - dest.y);
//        Value = FindValue();
//    }

//    public int FindValue() {
//        return FromStart + FromEnd;
//    }

//    public List<Vector2Int> Neighbors() {
//        List<Vector2Int> r = new List<Vector2Int>();
//        List<Poi>
//    }
//}
