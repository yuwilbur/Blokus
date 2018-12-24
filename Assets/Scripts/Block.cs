using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class BlockProvider
{
  public enum CellType
  {
    EMPTY, BLOCK, OUTLINE_CORNER, OUTLINE_SIDE
  }

  // Character denoting that this location is a block.
  private const char X = 'X';
  // Character denoting that this location is a empty.
  private const char O = 'O';

  public static readonly char[,] B1_1 = { { X } };
  public static readonly char[,] B2_11 = { { X, X } };
  public static readonly char[,] B3_111 = { { X, X, X } };
  public static readonly char[,] B3_11_10 = { { X, X }, { X, O } };
  public static readonly char[,] B4_1111 = { { X, X, X, X } };
  public static readonly char[,] B4_010_111 = { { O, X, O }, { X, X, X } };
  public static readonly char[,] B4_100_111 = { { X, O, O }, { X, X, X } };
  public static readonly char[,] B4_110_011 = { { X, X, O }, { O, X, X } };
  public static readonly char[,] B4_11_11 = { { X, X }, { X, X } };
  public static readonly char[,] B5_11111 = { { X, X, X, X, X } };
  public static readonly char[,] B5_1000_1111 = { { X, O, O, O }, { X, X, X, X } };
  public static readonly char[,] B5_1100_0111 = { { X, X, O, O }, { O, X, X, X } };
  public static readonly char[,] B5_0100_1111 = { { O, X, O, O }, { X, X, X, X } };
  public static readonly char[,] B5_111_110 = { { X, X, X }, { X, X, O } };
  public static readonly char[,] B5_111_101 = { { X, X, X }, { X, O, X } };
  public static readonly char[,] B5_010_010_111 = { { O, X, O }, { O, X, O }, { X, X, X } };
  public static readonly char[,] B5_100_100_111 = { { X, O, O }, { X, O, O }, { X, X, X } };
  public static readonly char[,] B5_100_111_001 = { { X, O, O }, { X, X, X }, { O, O, X } };
  public static readonly char[,] B5_110_011_001 = { { X, X, O }, { O, X, X }, { O, O, X } };
  public static readonly char[,] B5_110_011_010 = { { X, X, O }, { O, X, X }, { O, X, O } };
  public static readonly char[,] B5_010_111_010 = { { O, X, O }, { X, X, X }, { O, X, O } };

  public static GameObject Create(char[,] blueprint)
  {
    CheckBlueprint(blueprint);
    CellType[,] block = ConvertBlueprintToBlock(blueprint);
    Print(block);
    GameObject gameObject = new GameObject();
    // gameObject.AddComponent(this);
    return gameObject;
  }

  private static void Print(CellType[,] block) {
    string log = "";
    for(int i = 0; i < block.GetLength(0); ++i) {
      for (int j = 0; j < block.GetLength(1); ++j) {
        if (block[i,j] == CellType.BLOCK) {
          log += "B";
        } else if (block[i,j] == CellType.EMPTY) {
          log += "E";
        } else if (block[i,j] == CellType.OUTLINE_CORNER) {
          log += "C";
        } else if (block[i,j] == CellType.OUTLINE_SIDE) {
          log += "S";
        }
      }
      log += " ";
    }
    Debug.Log(log);
  }

  private static void CheckBlueprint(char[,] blueprint)
  {
    if (blueprint.Rank != 2)
    {
      throw new System.ArgumentException("Input structure is not 2-dimensions.");
    }
    if (blueprint.GetLength(0) == 0 || blueprint.GetLength(1) == 0)
    {
      throw new System.ArgumentException("Input structure is empty.");
    }
  }
  private static CellType[,] ConvertBlueprintToBlock(char[,] blueprint)
  {
    CellType[,] block = new CellType[blueprint.GetLength(0) + 2, blueprint.GetLength(1) + 2];

    // Set all blocks to EMPTY.
    for (int i = 0; i < block.GetLength(0); ++i)
    {
      for (int j = 0; j < block.GetLength(1); ++j)
      {
        block[i, j] = CellType.EMPTY;
      }
    }

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
    if (target == CellType.OUTLINE_SIDE && cell == CellType.BLOCK)
    {
      return;
    }
    if (target == CellType.OUTLINE_CORNER && (cell == CellType.OUTLINE_SIDE || cell == CellType.BLOCK))
    {
      return;
    }
    cell = target;
  }

  // private static CellType GetBlockType(CellType[,] block, int i, int j)
  // {
  //   if (block[i, j] == CellType.BLOCK)
  //   {
  //     return CellType.BLOCK;
  //   }

  //   if (
  //     (i > 0 && block[i - 1, j] == CellType.BLOCK) ||
  //     (j > 0 && block[i, j - 1] == CellType.BLOCK) ||
  //     (i + 1 < block.GetLength(0) && block[i + 1, j] == CellType.BLOCK) ||
  //     (j + 1 < block.GetLength(1) && block[i, j + 1] == CellType.BLOCK))
  //   {
  //     return CellType.OUTLINE_SIDE;
  //   }
  //   if (i > 0)
  //   {
  //     if (
  //       (j > 0 && block[i - 1, j - 1] == CellType.BLOCK) ||
  //       (j + 1 < block.GetLength(0) && block[i - 1, j + 1] == CellType.BLOCK))
  //     {
  //       return CellType.OUTLINE_CORNER;
  //     }
  //   }
  //   if (i + 1 < block.GetLength(0))
  //   {
  //     if (
  //       (j > 0 && block[i + 1, j - 1] == CellType.BLOCK) ||
  //       (j + 1 < block.GetLength(0) && block[i + 1, j + 1] == CellType.BLOCK))
  //     {
  //       return CellType.OUTLINE_CORNER;
  //     }
  //   }
  //   return CellType.EMPTY;
  // }
}