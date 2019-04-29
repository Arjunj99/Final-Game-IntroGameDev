using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<Vector2Int> possibleSpawns = new List<Vector2Int>();
    public KeyCode upButton;
    public KeyCode downButton;
    public KeyCode leftButton;
    public KeyCode rightButton;
    public GameObject gridHolder;
    public GameObject player;

    public Dictionary<int, Dictionary<int, Vector2Int>> Tiles = new Dictionary<int, Dictionary<int, Vector2Int>>();


    public const int HEIGHT = 10;
    public const int WIDTH = 16;



    private static GameManager instance = null;
    public static GameManager Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //public Vector2Int GetTile(int x, int y) {
    //    if (!Tiles.ContainsKey(x) || !Tiles[x].ContainsKey(y))
    //        return new Vector2Int(0, 0);
    //    return Tiles[x][y];
    //}
}
