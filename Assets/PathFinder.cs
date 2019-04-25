//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class PathFinder {
//    public static Vector2Int Pathfinder(Vector2Int StartPos, Vector2Int EndPos) {
//        int safety = 999;
//        List<PathTile> open = new List<PathTile>() { new PathTile(StartPos, null, EndPos) };
//        Dictionary<Vector2Int, PathTile> closed = new Dictionary<Vector2Int, PathTile>();
//        PathTile current = null;
//        while (open.Count > 0 && safety > 0) {
//            safety--;
//            int best = 999;
//            PathTile bTile = null;
//            foreach (PathTile t in open)
//                if (t.Value < best) {
//                    best = t.Value;
//                    bTile = t;
//                }
//            open.Remove(bTile);
//            if (bTile.Tile == EndPos) {
//                current = bTile;
//                break;
//            }

//            foreach (Vector2Int nei in bTile.Tile.Neighbors()) {
//                if ()
//            }
//        }
//    }

//    public Vector2Int Neighbor(int x, int y) {
//        return GameManager.Instance.GetTile(X + x,Y + y);
//    }
    
//    //public List<TileThing> Neighbors()
//    //{
//    //    List<TileThing> r = new List<TileThing>();
//    //    List<Point> dirs = new List<Point>(){new Point(1,0),new Point(-1,0),new Point(0,1),new Point(0,-1)};
//    //    foreach (Point d in dirs)
//    //    {
//    //        TileThing t = Neighbor(d.X, d.Y);
//    //        if (t != null)
//    //            r.Add(t);
//    //    }
//    //    return r;
//    //}
//}
