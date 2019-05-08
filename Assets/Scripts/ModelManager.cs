using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ModelManager {
    public static Dictionary<Guid,ActorModel> AllActors = new Dictionary<Guid, ActorModel>();
    public static Dictionary<Guid,TileModel> AllTiles = new Dictionary<Guid, TileModel>();
    [System.NonSerialized]public static Dictionary<int, Dictionary<int, TileModel>> Tiles = new Dictionary<int, Dictionary<int, TileModel>>();
    public static int Score;
    public static int HP = 3;
    //public static SaveFile PendingSave;
    
    public static void OnLoad() {
        foreach(TileModel tm in GetTiles())
            tm.OnLoad();
        foreach(ActorModel am in GetThings())
            am.OnLoad();
    }
    
    public static void BuildModel() {
        AllActors.Clear();
        AllTiles.Clear();
        Tiles.Clear();
        ////Is there a saved level stored on the hard drive already?
        ////If so, just use that.
        //if (PendingSave != null)
        //{
        //    PendingSave.Imprint();
        //    OnLoad();
        //    PendingSave = null;
        //    return;
        //}

        for (int x = -GameSettings.MapSizeX /2; x <= GameSettings.MapSizeX /2; x++) {
            for (int y = -GameSettings.MapSizeY /2; y <= GameSettings.MapSizeY /2; y++) {
                TileModel t = new TileModel(x,y);
            }
        }

        List<TileModel> openTiles = new List<TileModel>();
        openTiles.AddRange(GetTiles());

        int rando = Random.Range(0, 5);
        switch (rando) {
            case 0:
                for (int i = 0; i < God.WSM.wallPreset1.Count; i++) {
                    TileModel WallPlacement = openTiles[God.WSM.wallPreset1[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 1:
                for (int i = 0; i < God.WSM.wallPreset2.Count; i++) {
                    TileModel WallPlacement = openTiles[God.WSM.wallPreset2[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 2:
                for (int i = 0; i < God.WSM.wallPreset3.Count; i++) {
                    TileModel WallPlacement = openTiles[God.WSM.wallPreset3[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 3:
                for (int i = 0; i < God.WSM.wallPreset4.Count; i++) {
                    TileModel WallPlacement = openTiles[God.WSM.wallPreset4[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 4:
                for (int i = 0; i < God.WSM.wallPreset5.Count; i++) {
                    TileModel WallPlacement = openTiles[God.WSM.wallPreset5[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
        }

        foreach (ThingTypes t in GameSettings.MapContents) {
            if (openTiles.Count == 0)
                break;
            TileModel rand = openTiles[Random.Range(0, openTiles.Count)];
            openTiles.Remove(rand);
            ActorModel a = new ActorModel(rand,t);
        }
    }

    public static void BuildView() {
        foreach(TileModel tm in GetTiles())
            God.Library.SpawnTile(tm);
        foreach(ActorModel am in GetThings())
            God.Library.SpawnThing(am);
    }
    
    public static TileModel GetTile(int x, int y) {
        if (!Tiles.ContainsKey(x) || !Tiles[x].ContainsKey(y))
            return null;
        return Tiles[x][y];
    }
    
    public static TileModel GetTile(Point loc) {
        if (loc == null)
            return null;
        return GetTile(loc.x, loc.y);
    }
    
    public static ActorModel GetActor(Guid id) {
        if (AllActors.ContainsKey(id))
            return AllActors[id];
        return null;
    }

    public static List<TileModel> GetTiles() {
        List<TileModel> r = new List<TileModel>();
        r.AddRange(AllTiles.Values);
        return r;
    }
    
    public static List<ActorModel> GetActors() {
        List<ActorModel> r = new List<ActorModel>();
        r.AddRange(AllActors.Values);
        return r;
    }

    public static List<ActorModel> GetThings(ThingTypes thingType = ThingTypes.None) {
        List<ActorModel> r = new List<ActorModel>();
        foreach(ActorModel wt in GetActors())
            if (thingType == ThingTypes.None || wt.Type == thingType)
                r.Add(wt);
        return r;
    }
    
    public static void ChangeScore(int amt) {
        Score += amt;
    }

    public static void TakeDamage() {
        if (God.GSM.monsterRound > 0) {
            God.GSM.monsterRound -= 1;
        }
        else { // Check this out
            HP -= 3;
        }
        HP -= 1;

        if (HP <= 0) {
            HP = 0;
            God.C.AddAction(new DeathAction(GetThings(ThingTypes.Player)[0]));
        }
    }

    //public static void SaveGame()
    //{
    //    string destination = Application.persistentDataPath + "/save.dat";
    //    FileStream file;

    //    if(File.Exists(destination)) file = File.OpenWrite(destination);
    //    else file = File.Create(destination);

    //    SaveFile data = new SaveFile();
    //    BinaryFormatter bf = new BinaryFormatter();
    //    bf.Serialize(file, data);
    //    file.Close();
    //}

    //public static SaveFile LoadGame()
    //{
    //    string destination = Application.persistentDataPath + "/save.dat";
    //    FileStream file;

    //    if(File.Exists(destination)) file = File.OpenRead(destination);
    //    else return null;
        
    //    BinaryFormatter bf = new BinaryFormatter();
    //    SaveFile data = (SaveFile) bf.Deserialize(file);
    //    file.Close();
    //    return data;
    //}

    //public static void DeleteSave()
    //{
    //    string destination = Application.persistentDataPath + "/save.dat";
    //    if (File.Exists(destination)) 
    //        File.Delete(destination);
    //}
    
    //public static IEnumerator CaptureScreenshot (string name)
    //{
    //    int wid = Screen.width;
    //    int hei = Screen.height;
    //    Texture2D screenCap = new Texture2D (wid, hei, TextureFormat.RGB24, false);

    //    yield return new WaitForEndOfFrame ();
    //    screenCap.ReadPixels (new Rect (0, 0, wid, hei), 0, 0);
    //    screenCap.Apply ();

    //    // Encode texture into PNG
    //    byte[] bytes = screenCap.EncodeToPNG ();

    //    string x = string.Format (Application.persistentDataPath + "/SS" + name + ".png");
    //    File.WriteAllBytes (x, bytes);
    //}
}

//[System.Serializable]
//public class SaveFile
//{

//    public Dictionary<Guid, ActorModel> AllThings;
//    public Dictionary<Guid, TileModel> AllTiles;
//    public int Score;
//    public int HP;

//    public SaveFile ()
//    {
//        AllThings = ModelManager.AllActors;
//        AllTiles = ModelManager.AllTiles;
//        Score = ModelManager.Score;
//        HP = ModelManager.HP;
//    }

//    public void Imprint(){
//        ModelManager.AllActors = AllThings;
//        ModelManager.AllTiles = AllTiles;
//        ModelManager.Score = Score;
//        ModelManager.HP = HP;
//    }
//}