using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="MonsterTypes" ,menuName="Monster Type")]
public class MonsterType : ScriptableObject {
    public Types Type;
    public Sprite S;
    public int Vibration;
    
    public enum Types {
        None=0,
        Tybault=1,
        KeyMimic=2
    }
}
