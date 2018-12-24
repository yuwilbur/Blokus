using static UnityEngine.Debug;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    int width_step = 4;
    int height_step = 6;
    int count = 0;
    int length = BlockProvider.BLOCKS.Count;
    foreach (KeyValuePair<string, char[,]> blueprint in BlockProvider.BLOCKS)
    {
      Block block = BlockProvider.Create(blueprint);
      block.color = Color.red;
      block.transform.position = new Vector3((count - length / 2) * width_step, 0, 0);
      count++;
    }
    count = 0;
    foreach (KeyValuePair<string, char[,]> blueprint in BlockProvider.BLOCKS)
    {
      Block block = BlockProvider.Create(blueprint);
      block.color = Color.green;
      block.transform.position = new Vector3((count - length / 2) * width_step, height_step, 0);
      count++;
    }
    count = 0;
    foreach (KeyValuePair<string, char[,]> blueprint in BlockProvider.BLOCKS)
    {
      Block block = BlockProvider.Create(blueprint);
      block.color = Color.blue;
      block.transform.position = new Vector3((count - length / 2) * width_step, -height_step, 0);
      count++;
    }
    count = 0;
    foreach (KeyValuePair<string, char[,]> blueprint in BlockProvider.BLOCKS)
    {
      Block block = BlockProvider.Create(blueprint);
      block.color = Color.yellow;
      block.transform.position = new Vector3((count - length / 2) * width_step, - 2 * height_step, 0);
      count++;
    }
    BoardProvider.Create(19, 19);
  }

  // Update is called once per frame
  void Update()
  {

  }
}