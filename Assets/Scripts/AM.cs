using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AM : MonoBehaviour {
    public AudioClip monsterSnarl;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip StompClip;
    public AudioSource AS;

    void Awake() {
        God.AM = this;
    }
}
