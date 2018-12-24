using static UnityEngine.Debug;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    int count = 0;
    foreach(KeyValuePair<string, char[,]> blueprint in BlockProvider.BLOCKS) {
        GameObject block =  BlockProvider.Create(blueprint);
        block.transform.position = new Vector3(count*6, 0, 0);
        count++;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}