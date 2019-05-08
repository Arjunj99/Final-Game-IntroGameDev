using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM : MonoBehaviour {
    public Sprite black;
    public Sprite grey;
    public Sprite lightgrey;
    public Sprite wallTop;
    public RuntimeAnimatorController animator;
    public RuntimeAnimatorController walkingAnimation;

    void Awake() {
        God.SM = this;
    }
}
