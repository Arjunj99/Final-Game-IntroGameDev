﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;

public static class ModelManager
{
    public static Dictionary<Guid,ActorModel> AllThings = new Dictionary<Guid, ActorModel>();
    public static Dictionary<Guid,TileModel> AllTiles = new Dictionary<Guid, TileModel>();
    [System.NonSerialized]public static Dictionary<int, Dictionary<int, TileModel>> Tiles = new Dictionary<int, Dictionary<int, TileModel>>();
    public static int Score;
    public static int HP = 4;
    public static SaveFile PendingSave;
    
    public static void OnLoad()
    {
        
        foreach(TileModel tm in GetTiles())
            tm.OnLoad();
        foreach(ActorModel am in GetThings())
            am.OnLoad();
    }
    
    public static void BuildModel()
    {
        AllThings.Clear();
        AllTiles.Clear();
        Tiles.Clear();
        //Is there a saved level stored on the hard drive already?
        //If so, just use that.
        if (PendingSave != null)
        {
            PendingSave.Imprint();
            OnLoad();
            PendingSave = null;
            return;
        }
        
        //Otherwise, build our own procedurally.
        for (int x = -GameSettings.MapSizeX /2; x <= GameSettings.MapSizeX /2; x++)
        {
            for (int y = -GameSettings.MapSizeY /2; y <= GameSettings.MapSizeY /2; y++)
            {
                TileModel t = new TileModel(x,y);
            }
        }

        //List<TileModel> openTiles = new List<TileModel>();
        //openTiles.AddRange(GetTiles());
        //foreach (ThingTypes t in GameSettings.MapContents)
        //{
        //    if (openTiles.Count == 0)
        //        break;
        //    if (t == ThingTypes.Wall) {
        //        TileModel WallPlacement = openTiles[4];
        //        openTiles.Remove(WallPlacement);
        //        ActorModel a = new ActorModel(WallPlacement, t);
        //    }
        //    else {
        //        TileModel rand = openTiles[Random.Range(0, openTiles.Count)];
        //        openTiles.Remove(rand);
        //        ActorModel a = new ActorModel(rand, t);
        //    }

        //}


        // SEARCH "Odium" TO FIND WALL SPAWN
        List<TileModel> openTiles = new List<TileModel>();
        openTiles.AddRange(GetTiles());

        int rando = Random.Range(0, 5);
        switch (rando) {
            case 0:
                for (int i = 0; i < God.GSM.wallPreset1.Count; i++) {
                    TileModel WallPlacement = openTiles[God.GSM.wallPreset1[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 1:
                for (int i = 0; i < God.GSM.wallPreset2.Count; i++)
                {
                    TileModel WallPlacement = openTiles[God.GSM.wallPreset2[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 2:
                for (int i = 0; i < God.GSM.wallPreset3.Count; i++)
                {
                    TileModel WallPlacement = openTiles[God.GSM.wallPreset3[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 3:
                for (int i = 0; i < God.GSM.wallPreset4.Count; i++)
                {
                    TileModel WallPlacement = openTiles[God.GSM.wallPreset4[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
            case 4:
                for (int i = 0; i < God.GSM.wallPreset5.Count; i++)
                {
                    TileModel WallPlacement = openTiles[God.GSM.wallPreset5[i]];
                    openTiles.Remove(WallPlacement);
                    ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);
                }
                break;
        }


        //Addd THis in case it doesn't work
        //TileModel WallPlacement = openTiles[10];
        //openTiles.Remove(WallPlacement);
        //ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);

        foreach (ThingTypes t in GameSettings.MapContents) {
            if (openTiles.Count == 0)
                break;
            TileModel rand = openTiles[Random.Range(0, openTiles.Count)];
            openTiles.Remove(rand);
            Debug.Log(rand.X + ":" + rand.Y);
            ActorModel a = new ActorModel(rand,t);
        }
        //TileModel WallPlacement = openTiles[4];
        //Debug.Log(WallPlacement.X + ":" + WallPlacement.Y);
        //openTiles.Remove(WallPlacement);
        //ActorModel actor = new ActorModel(WallPlacement, ThingTypes.Wall);

    }

    public static void BuildView()
    {
        foreach(TileModel tm in GetTiles())
            God.Library.SpawnTile(tm);
        foreach(ActorModel am in GetThings())
            God.Library.SpawnThing(am);
    }
    
    //Feed me coordinates and I'll tell you the tile that lives there
    public static TileModel GetTile(int x, int y)
    {
        if (!Tiles.ContainsKey(x) || !Tiles[x].ContainsKey(y))
            return null;
        return Tiles[x][y];
    }
    
    public static TileModel GetTile(Point loc)
    {
        if (loc == null)
            return null;
        return GetTile(loc.x, loc.y);
    }
    
    public static ActorModel GetActor(Guid id)
    {
        if (AllThings.ContainsKey(id))
            return AllThings[id];
        return null;
    }

    public static List<TileModel> GetTiles()
    {
        List<TileModel> r = new List<TileModel>();
        r.AddRange(AllTiles.Values);
        return r;
    }
    
    public static List<ActorModel> GetActors()
    {
        List<ActorModel> r = new List<ActorModel>();
        r.AddRange(AllThings.Values);
        return r;
    }

    
    
    //I can get a list of all the TYPE of a thing to exist.
    //If I don't specify a type it just gives me all world things that exist period
    //This doesn't work yet, since I don't have a way to populate AllThings
    public static List<ActorModel> GetThings(ThingTypes thingType = ThingTypes.None)
    {
        List<ActorModel> r = new List<ActorModel>();
        foreach(ActorModel wt in GetActors())
            if (thingType == ThingTypes.None || wt.Type == thingType)
                r.Add(wt);
        return r;
    }
    
    //This makes my points go up or down and also updates the text in the top left corner
    public static void ChangeScore(int amt)
    {
        Score += amt;
    }

    public static void TakeDamage(int amt)
    {
        if (God.GSM.monsterRound > 0) {
            God.GSM.monsterRound -= 1;
        }
        else {
            HP -= 10;
        }
        //Debug.Log("Monster Round rn is " + God.GSM.monsterRound);
        HP -= 1;

        //HP -= amt;
        if (HP <= 0)
        {
            HP = 0;
            God.C.AddAction(new DeathAction(GetThings(ThingTypes.Player)[0]));
        }
    }

    public static void SaveGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        SaveFile data = new SaveFile();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public static SaveFile LoadGame()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if(File.Exists(destination)) file = File.OpenRead(destination);
        else return null;
        
        BinaryFormatter bf = new BinaryFormatter();
        SaveFile data = (SaveFile) bf.Deserialize(file);
        file.Close();
        return data;
    }

    public static void DeleteSave()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        if (File.Exists(destination)) 
            File.Delete(destination);
    }
    
    public static IEnumerator CaptureScreenshot (string name)
    {
        int wid = Screen.width;
        int hei = Screen.height;
        Texture2D screenCap = new Texture2D (wid, hei, TextureFormat.RGB24, false);

        yield return new WaitForEndOfFrame ();
        screenCap.ReadPixels (new Rect (0, 0, wid, hei), 0, 0);
        screenCap.Apply ();

        // Encode texture into PNG
        byte[] bytes = screenCap.EncodeToPNG ();

        string x = string.Format (Application.persistentDataPath + "/SS" + name + ".png");
        File.WriteAllBytes (x, bytes);
    }
}

[System.Serializable]
public class SaveFile
{

    public Dictionary<Guid, ActorModel> AllThings;
    public Dictionary<Guid, TileModel> AllTiles;
    public int Score;
    public int HP;

    public SaveFile ()
    {
        AllThings = ModelManager.AllThings;
        AllTiles = ModelManager.AllTiles;
        Score = ModelManager.Score;
        HP = ModelManager.HP;
    }

    public void Imprint(){
        ModelManager.AllThings = AllThings;
        ModelManager.AllTiles = AllTiles;
        ModelManager.Score = Score;
        ModelManager.HP = HP;
    }
}