using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class BlockProvider
{
  // Values of the enum dictates their priority during creation.
  public enum CellType
  {
    EMPTY = 0, OUTLINE_CORNER = 1, OUTLINE_SIDE = 2, BLOCK = 3
  }

  // Character denoting that this location is a block.
  private const char X = 'X';
  // Character denoting that this location is a empty.
  private const char O = 'O';
  public static readonly Dictionary<string, char[,]> BLOCKS = new Dictionary<string, char[,]>() {
    {"B1_1", new char[,] { { X } }},
    {"B2_11", new char[,] { { X, X } }},
    {"B3_111", new char[,] { { X, X, X } }},
    {"B3_11_10", new char[,] { { X, X }, { X, O } }},
    {"B4_1111", new char[,] { { X, X, X, X } }},
    {"B4_010_111", new char[,] { { O, X, O }, { X, X, X } }},
    {"B4_100_111", new char[,] { { X, O, O }, { X, X, X } }},
    {"B4_110_011", new char[,] { { X, X, O }, { O, X, X } }},
    {"B4_11_11", new char[,] { { X, X }, { X, X } }},
    {"B5_11111", new char[,] { { X, X, X, X, X } }},
    {"B5_1000_1111", new char[,] { { X, O, O, O }, { X, X, X, X } }},
    {"B5_1100_0111", new char[,] { { X, X, O, O }, { O, X, X, X } }},
    {"B5_0100_1111", new char[,] { { O, X, O, O }, { X, X, X, X } }},
    {"B5_111_110", new char[,] { { X, X, X }, { X, X, O } }},
    {"B5_111_101", new char[,] { { X, X, X }, { X, O, X } }},
    {"B5_010_010_111", new char[,] { { O, X, O }, { O, X, O }, { X, X, X } }},
    {"B5_100_100_111", new char[,] { { X, O, O }, { X, O, O }, { X, X, X } }},
    {"B5_100_111_001", new char[,] { { X, O, O }, { X, X, X }, { O, O, X } }},
    {"B5_110_011_001", new char[,] { { X, X, O }, { O, X, X }, { O, O, X } }},
    {"B5_110_011_010", new char[,] { { X, X, O }, { O, X, X }, { O, X, O } }},
    {"B5_010_111_010", new char[,] { { O, X, O }, { X, X, X }, { O, X, O } }}
  };

  public static GameObject Create(KeyValuePair<string, char[,]> blueprint)
  {
    CellType[,] block = ConvertBlueprintToBlock(blueprint.Value);
    GameObject gameObject = new GameObject();
    Vector3 position_offset = new Vector3((float)(block.GetLength(0) - 1) / 2.0f, (float)(block.GetLength(1) - 1) / 2.0f, 0);
    for (int i = 0; i < block.GetLength(0); ++i)
    {
      for (int j = 0; j < block.GetLength(1); ++j)
      {
        CellType blockType = block[i, j];
        Color color = Color.white;
        switch (blockType)
        {
          case CellType.BLOCK:
            color = Color.white;
            break;
          case CellType.OUTLINE_CORNER:
            color = Color.red;
            break;
          case CellType.OUTLINE_SIDE:
            color = Color.gray;
            break;
          case CellType.EMPTY:
          default:
            continue;
        }
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.GetComponent<Renderer>().material.color = color;
        piece.transform.localPosition = new Vector3(i, j, 0) - position_offset;
        piece.transform.parent = gameObject.transform;
      }
    }
    gameObject.name = blueprint.Key;
    return gameObject;
  }

  private static void Print(CellType[,] block)
  {
    string log = "";
    for (int i = 0; i < block.GetLength(0); ++i)
    {
      for (int j = 0; j < block.GetLength(1); ++j)
      {
        if (block[i, j] == CellType.BLOCK)
        {
          log += "B";
        }
        else if (block[i, j] == CellType.EMPTY)
        {
          log += "E";
        }
        else if (block[i, j] == CellType.OUTLINE_CORNER)
        {
          log += "C";
        }
        else if (block[i, j] == CellType.OUTLINE_SIDE)
        {
          log += "S";
        }
      }
      log += " ";
    }
    Debug.Log(log);
  }

  private static CellType[,] ConvertBlueprintToBlock(char[,] blueprint)
  {
    if (blueprint.Rank != 2)
    {
      throw new System.ArgumentException("Input structure is not 2-dimensions.");
    }
    if (blueprint.GetLength(0) == 0 || blueprint.GetLength(1) == 0)
    {
      throw new System.ArgumentException("Input structure is empty.");
    }

    CellType[,] block = new CellType[blueprint.GetLength(0) + 2, blueprint.GetLength(1) + 2];

    // Set the all the blueprints onto the block.
    for (int i = 0; i < blueprint.GetLength(0); ++i)
    {
      for (int j = 0; j < blueprint.GetLength(1); ++j)
      {
        if (blueprint[i, j] == O)
        {
          continue;
        }
        AssignCellType(ref block[i + 0, j + 0], CellType.OUTLINE_CORNER);
        AssignCellType(ref block[i + 1, j + 0], CellType.OUTLINE_SIDE);
        AssignCellType(ref block[i + 2, j + 0], CellType.OUTLINE_CORNER);
        AssignCellType(ref block[i + 0, j + 1], CellType.OUTLINE_SIDE);
        AssignCellType(ref block[i + 1, j + 1], CellType.BLOCK);
        AssignCellType(ref block[i + 2, j + 1], CellType.OUTLINE_SIDE);
        AssignCellType(ref block[i + 0, j + 2], CellType.OUTLINE_CORNER);
        AssignCellType(ref block[i + 1, j + 2], CellType.OUTLINE_SIDE);
        AssignCellType(ref block[i + 2, j + 2], CellType.OUTLINE_CORNER);
      }
    }

    return block;
  }

  private static void AssignCellType(ref CellType cell, CellType target)
  {
    if (target > cell) {
      cell = target;
    }
  }
}