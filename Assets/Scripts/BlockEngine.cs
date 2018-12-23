using static UnityEngine.Debug;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    Block block = Block.B5_100_111_001();
    block.Print();
  }

  // Update is called once per frame
  void Update()
  {

  }
}