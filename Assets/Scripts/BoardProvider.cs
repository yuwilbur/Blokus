using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardProvider
{
  public static GameObject Create(int row, int col, float dividerThickness = 0.005f)
  {
    GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
    bottom.transform.localScale = new Vector3(row, col, 1);
    for (int i = 1; i < row; ++i)
    {
      GameObject divider = GameObject.CreatePrimitive(PrimitiveType.Cube);
      divider.GetComponent<Renderer>().material.color = Color.gray;
      divider.transform.parent = bottom.transform;
      divider.transform.localScale = new Vector3(dividerThickness, 1, 1.25f);
      divider.transform.localPosition = new Vector3((float)(i) / row - 0.5f, 0, 0);
    }
    for (int i = 1; i < col; ++i)
    {
      GameObject divider = GameObject.CreatePrimitive(PrimitiveType.Cube);
      divider.GetComponent<Renderer>().material.color = Color.gray;
      divider.transform.parent = bottom.transform;
      divider.transform.localScale = new Vector3(1, dividerThickness, 1.25f);
      divider.transform.localPosition = new Vector3(0, (float)(i) / col - 0.5f, 0);
    }

    GameObject board = new GameObject();
    bottom.transform.parent = board.transform;
    board.transform.position = new Vector3(0, 0, 1);
    return board;
  }
}
