//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public static class Pathfinder {
//    public static List<TileModel> Pathfind(TileModel start, TileModel end) {
//        int safety = 999;
//        List<PathTile> open = new List<PathTile>() { new PathTile(start, null, end) };
//        Dictionary<TileModel, PathTile> closed = new Dictionary<TileModel, PathTile>();
//        PathTile current = null;
//        while (open.Count > 0 && safety > 0)
//        {
//            safety--;
//            int best = 999;
//            PathTile bTile = null;
//            foreach (PathTile t in open)
//                if (t.Value < best)
//                {
//                    best = t.Value;
//                    bTile = t;
//                }

//            open.Remove(bTile);
//            if (bTile.Tile == end)
//            {
//                current = bTile;
//                break;
//            }

//            //Just after you find your bTile
//            foreach (TileModel nei in bTile.Tile.Neighbors())
//            {
//                if (nei.CanEnter())
//                {
//                    if (!closed.ContainsKey(nei))
//                    {
//                        PathTile pt = new PathTile(nei, bTile, end);
//                        open.Add(pt);
//                        closed.Add(nei, pt);
//                        continue;
//                    }

//                    if (open.Contains(closed[nei]) && closed[nei].FromStart > bTile.FromStart + 1)
//                    {
//                        closed[nei].FromStart = bTile.FromStart + 1;
//                        closed[nei].CameFrom = bTile;
//                        closed[nei].Value = closed[nei].FindValue();
//                    }
//                }
//            }
//        }
//        List<TileThing> path = new List<TileThing>();
//        while (current != null && current.Tile != start)
//        {
//            //We add it to the start because we're tracing the path backwards
//            path.Insert(0, current.Tile);
//            current = current.CameFrom;
//        }
//        return path;
//    }
//}

//public class PathTile
//{
//    public TileThing Tile;
//    public PathTile CameFrom;
//    public int FromStart;
//    public int FromEnd;

//    public int Value;

//    public PathTile(TileThing t, PathTile cf, TileThing dest)
//    {
//        Tile = t; CameFrom = cf;
//        FromStart = CameFrom != null ? CameFrom.FromStart + 1 : 0;
//        FromEnd = Mathf.Abs(Tile.X - dest.X) + Mathf.Abs(Tile.Y - dest.Y);
//        Value = FindValue();
//    }

//    public int FindValue()
//    {
//        return FromStart + FromEnd;
//    }
//}

