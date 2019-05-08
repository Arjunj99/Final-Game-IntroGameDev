using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSM : MonoBehaviour {
    public List<int> wallPreset1 = new List<int>();
    public List<int> wallPreset2 = new List<int>();
    public List<int> wallPreset3 = new List<int>();
    public List<int> wallPreset4 = new List<int>();
    public List<int> wallPreset5 = new List<int>();

    void Awake() {
        God.WSM = this;
    }
}
