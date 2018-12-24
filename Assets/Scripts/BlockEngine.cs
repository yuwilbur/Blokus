using static UnityEngine.Debug;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GameObject block = BlockProvider.Create(BlockProvider.B5_010_111_010);
    // block.Print();
  }

  // Update is called once per frame
  void Update()
  {

  }
}