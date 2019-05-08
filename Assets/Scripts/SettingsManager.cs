using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour {
    public Vector2 MapSize;
    public List<ThingTypes> MapContents;
    public Vector2 PlayerCoords;

    void Awake() {
        GameSettings.MapSizeX = (int)MapSize.x;
        GameSettings.MapSizeY = (int)MapSize.y;
        GameSettings.MapContents = MapContents;
        GameSettings.playerX = (int)PlayerCoords.x;
        GameSettings.playerY = (int)PlayerCoords.y;
    }
}
