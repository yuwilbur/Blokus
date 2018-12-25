using static UnityEngine.Debug;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEngine : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    BoardProvider.Create(19, 19);
  }

  // Update is called once per frame
  void Update()
  {

  }
}