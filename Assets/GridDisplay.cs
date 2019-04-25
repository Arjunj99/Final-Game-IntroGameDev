using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour {
    public GameObject mainCamera;
    public Vector2Int currentPosition;
    public Vector2Int enemyPosition;
    public GameObject testTile;
    public GameObject[,] tiles;
    public GameObject[,] displayTiles;
    private GameObject gridHolder;
    public Grid grid;
    private GameObject player;

    void Start() {
        grid = new Grid(currentPosition, enemyPosition, tiles);
        grid.Tiles = new GameObject[GameManager.WIDTH, GameManager.HEIGHT];
        displayTiles = new GameObject[GameManager.WIDTH, GameManager.HEIGHT];
        gridHolder = new GameObject();


        grid.RandomStart();
        Debug.Log(grid.CurrentPosition.x);


        for (int i = 0; i < GameManager.WIDTH; i++) {
            for (int j = 0; j < GameManager.HEIGHT; j++) {
                Debug.Log("Instant");
                grid.Tiles[i,j] = null;
                GameObject newTile = Instantiate(testTile);
                newTile.transform.parent = gridHolder.transform;
                newTile.transform.localPosition = new Vector2
                    (GameManager.WIDTH - i * 3.5f - 0,
                     GameManager.HEIGHT - j * 3.5f - 0);
                displayTiles[i, j] = newTile;
            }
        }
        player = Instantiate(GameManager.Instance.player);
        mainCamera.transform.parent = player.transform;
        //GameObject player = Instantiate(GameManager.Instance.player);
        grid.Tiles[grid.CurrentPosition.x, grid.CurrentPosition.y] = GameManager.Instance.player;
        Debug.Log("instant");
        player.transform.localPosition = displayTiles[grid.CurrentPosition.x, grid.CurrentPosition.y].transform.localPosition;
    }


    // Update is called once per frame
    void Update() {
        //if (grid.Tiles[grid.CurrentPosition.x, grid.CurrentPosition.y] != player) {
        //    //Debug.Log("fuckin error");
        //    //Destroy(grid.Tiles[grid.CurrentPosition.x, grid.CurrentPosition.y]);
        //    grid.Tiles[grid.CurrentPosition.x, grid.CurrentPosition.y] = player;
        //}

        for (int i = 0; i < GameManager.WIDTH; i++) {
            for (int j = 0; j < GameManager.HEIGHT; j++) {
                if (i == grid.CurrentPosition.x && j == grid.CurrentPosition.y) {
                    grid.Tiles[i, j] = player;
                    continue;
                }
                if (grid.Tiles[i, j] == GameManager.Instance.player) {
                    Destroy(grid.Tiles[i, j]);
                    grid.Tiles[i, j] = null;
                }
            }
        }
        //Debug.Log(grid.CurrentPosition.x + ":" + grid.CurrentPosition.y);
        grid.Movement();
        player.transform.localPosition = displayTiles[grid.CurrentPosition.x, grid.CurrentPosition.y].transform.localPosition;
    }
}
