using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class BlockProvider
{
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

  public static Block Create(KeyValuePair<string, char[,]> blueprint)
  {
    Block block = (new GameObject()).AddComponent<Block>() as Block;
    block.name = blueprint.Key;
    Piece.Type[,] blockData = ConvertBlueprintToBlock(blueprint.Value);
    Vector3 positionOffset = new Vector3((float)(blockData.GetLength(0) - 1) / 2.0f, (float)(blockData.GetLength(1) - 1) / 2.0f, 0);
    for (int i = 0; i < blockData.GetLength(0); ++i)
    {
      for (int j = 0; j < blockData.GetLength(1); ++j)
      {
        Piece.Type blockType = blockData[i, j];
        if (blockType == Piece.Type.EMPTY)
        {
          continue;
        }
        switch (blockType)
        {
          case Piece.Type.BLOCK:
            block.AddBlock(new Vector3(i, j, 0) - positionOffset);
            break;
          case Piece.Type.OUTLINE_CORNER:
            block.AddCorner(new Vector3(i, j, 0) - positionOffset);
            break;
          case Piece.Type.OUTLINE_SIDE:
            block.AddSide(new Vector3(i, j, 0) - positionOffset);
            break;
        }
      }
    }
    return block;
  }

  private static void Print(Piece.Type[,] block)
  {
    string log = "";
    for (int i = 0; i < block.GetLength(0); ++i)
    {
      for (int j = 0; j < block.GetLength(1); ++j)
      {
        if (block[i, j] == Piece.Type.BLOCK)
        {
          log += "B";
        }
        else if (block[i, j] == Piece.Type.EMPTY)
        {
          log += "E";
        }
        else if (block[i, j] == Piece.Type.OUTLINE_CORNER)
        {
          log += "C";
        }
        else if (block[i, j] == Piece.Type.OUTLINE_SIDE)
        {
          log += "S";
        }
      }
      log += " ";
    }
    Debug.Log(log);
  }

  private static Piece.Type[,] ConvertBlueprintToBlock(char[,] blueprint)
  {
    if (blueprint.Rank != 2)
    {
      throw new System.ArgumentException("Input structure is not 2-dimensions.");
    }
    if (blueprint.GetLength(0) == 0 || blueprint.GetLength(1) == 0)
    {
      throw new System.ArgumentException("Input structure is empty.");
    }

    Piece.Type[,] block = new Piece.Type[blueprint.GetLength(0) + 2, blueprint.GetLength(1) + 2];

    // Set the all the blueprints onto the block.
    for (int i = 0; i < blueprint.GetLength(0); ++i)
    {
      for (int j = 0; j < blueprint.GetLength(1); ++j)
      {
        if (blueprint[i, j] == O)
        {
          continue;
        }
        AssignCellType(ref block[i + 0, j + 0], Piece.Type.OUTLINE_CORNER);
        AssignCellType(ref block[i + 1, j + 0], Piece.Type.OUTLINE_SIDE);
        AssignCellType(ref block[i + 2, j + 0], Piece.Type.OUTLINE_CORNER);
        AssignCellType(ref block[i + 0, j + 1], Piece.Type.OUTLINE_SIDE);
        AssignCellType(ref block[i + 1, j + 1], Piece.Type.BLOCK);
        AssignCellType(ref block[i + 2, j + 1], Piece.Type.OUTLINE_SIDE);
        AssignCellType(ref block[i + 0, j + 2], Piece.Type.OUTLINE_CORNER);
        AssignCellType(ref block[i + 1, j + 2], Piece.Type.OUTLINE_SIDE);
        AssignCellType(ref block[i + 2, j + 2], Piece.Type.OUTLINE_CORNER);
      }
    }

    return block;
  }

  private static void AssignCellType(ref Piece.Type cell, Piece.Type target)
  {
    if (target > cell)
    {
      cell = target;
    }
  }
}