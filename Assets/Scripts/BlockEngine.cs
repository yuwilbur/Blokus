using static UnityEngine.Debug;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {
        Block block = Block.B1_1();
        Log(block.getTest().ToString());
        block.setTest(1);
        Log(block.getTest().ToString());

        Block block2 = Block.B1_1();
        Log(block2.getTest().ToString());
    }

    // Update is called once per frame
    void Update () {

    }
}