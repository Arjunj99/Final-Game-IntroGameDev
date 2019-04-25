using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    public Vector2Int CurrentPosition;
    public Vector2Int EnemyPosition;
    public GameObject[,] Tiles;

    public Grid(Vector2Int currentPosition, Vector2Int enemyPosition, GameObject[,] tiles) {
        CurrentPosition = currentPosition;
        EnemyPosition = enemyPosition;
        Tiles = tiles;
    }

    public void RandomStart() {
        int rand = (int)Random.Range(0f, GameManager.Instance.possibleSpawns.Count);
        CurrentPosition = GameManager.Instance.possibleSpawns[rand];
    }

    public void Movement() {
        if (Input.GetKeyDown(GameManager.Instance.upButton) && CurrentPosition.y > 0) {
            CurrentPosition.y--;
            //Debug.Log("up");
        }
        if (Input.GetKeyDown(GameManager.Instance.downButton) && CurrentPosition.y < GameManager.HEIGHT - 1) {
            CurrentPosition.y++;
            //Debug.Log("down");
        }
        if (Input.GetKeyDown(GameManager.Instance.rightButton) && CurrentPosition.x > 0) {
            CurrentPosition.x--;
            //Debug.Log("right");
        }
        if (Input.GetKeyDown(GameManager.Instance.leftButton) && CurrentPosition.x < GameManager.WIDTH - 1) {
            CurrentPosition.x++;
            //Debug.Log(CurrentPosition.x);
        }
    }
}
