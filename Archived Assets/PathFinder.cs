using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Pathfinder {
    public static List<Vector2Int> Pathfind(Vector2Int start, Vector2Int end) {
        int safety = 999;
        List<PathTile> open = new List<PathTile>() { new PathTile(start, null, end) };
        Dictionary<Vector2Int, PathTile> closed = new Dictionary<Vector2Int, PathTile>();
        PathTile current = null;
        while (open.Count > 0 && safety > 0) {
            safety--;
            int best = 999;
            PathTile bTile = null;
            foreach (PathTile t in open)
                if (t.Value < best) {
                    best = t.Value;
                    bTile = t;
                }

            open.Remove(bTile);
            if (bTile.Tile == end) {
                current = bTile;
                break;
            }

            //Just after you find your bTile
            foreach (TileThing nei in bTile.Tile.Neighbors())
            {
                if (nei.CanEnter())
                {
                    if (!closed.ContainsKey(nei))
                    {
                        PathTile pt = new PathTile(nei, bTile, end);
                        open.Add(pt);
                        closed.Add(nei, pt);
                        continue;
                    }

                    if (open.Contains(closed[nei]) && closed[nei].FromStart > bTile.FromStart + 1)
                    {
                        closed[nei].FromStart = bTile.FromStart + 1;
                        closed[nei].CameFrom = bTile;
                        closed[nei].Value = closed[nei].FindValue();
                    }
                }
            }
        }
        List<Vector2Int> path = new List<Vector2Int>();
        while (current != null && current.Tile != start)
        {
            //We add it to the start because we're tracing the path backwards
            path.Insert(0, current.Tile);
            current = current.CameFrom;
        }
        return path;
    }
}

public class PathTile
{
    public Vector2Int Tile;
    public PathTile CameFrom;
    public int FromStart;
    public int FromEnd;

    public int Value;

    public PathTile(Vector2Int t, PathTile cf, Vector2Int dest)
    {
        Tile = t; CameFrom = cf;
        FromStart = CameFrom != null ? CameFrom.FromStart + 1 : 0;
        FromEnd = Mathf.Abs(Tile.x - dest.x) + Mathf.Abs(Tile.y - dest.y);
        Value = FindValue();
    }

    public int FindValue()
    {
        return FromStart + FromEnd;
    }
}





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
