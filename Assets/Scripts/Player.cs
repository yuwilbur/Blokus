using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public Color m_color;

  void Start()
  {
    int width_step = 4;
    int count = 0;
    int length = BlockProvider.BLOCKS.Count;
    foreach (KeyValuePair<string, char[,]> blueprint in BlockProvider.BLOCKS)
    {
      Block block = BlockProvider.Create(blueprint);
      block.color = m_color;
      block.transform.parent = this.transform;
      block.transform.localPosition = new Vector3((count - length / 2) * width_step, 0, 0);
      count++;
    }
  }
}
