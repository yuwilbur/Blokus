using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Block
{

  public enum CellType
  {
    EMPTY, OUTLINE_CORNER, OUTLINE_SIDE, BLOCK
  }

  [Description("CellType")]
  private enum C
  {
    [Description("EMPTY")] O, [Description("FILLED")] X
  }

  private CellType[,] block;

  public void Print()
  {
    Dictionary<CellType, string> dictionary = new Dictionary<CellType, string>(){
      {CellType.EMPTY, "E"}, {CellType.BLOCK, "B"}, {CellType.OUTLINE_SIDE, "S"}, {CellType.OUTLINE_CORNER, "C"}
    };
    string block_string = "BLOCK: ";
    for (int i = 0; i < block.GetLength(0); ++i)
    {
      for (int j = 0; j < block.GetLength(1); ++j)
      {
        block_string += dictionary[block[i, j]];
      }
      block_string += ",";
    }
    Debug.Log(block_string);
  }

  private Block(C[,] block)
  {
    if (block.Rank != 2)
    {
      throw new System.ArgumentException("Input structure is not 2-dimensions.");
    }
    if (block.GetLength(0) == 0 || block.GetLength(1) == 0)
    {
      throw new System.ArgumentException("Input structure is empty.");
    }
    SetupBlock(block);
  }

  private void SetupBlock(C[,] bare_block)
  {
    block = new CellType[bare_block.GetLength(0) + 2, bare_block.GetLength(1) + 2];
    // Set all blocks to EMPTY.
    for (int i = 0; i < block.GetLength(0); ++i)
    {
      for (int j = 0; j < block.GetLength(1); ++j)
      {
        block[i, j] = CellType.EMPTY;
      }
    }

    // Set the bare blocks onto the blocks.
    for (int i = 0; i < bare_block.GetLength(0); ++i)
    {
      for (int j = 0; j < bare_block.GetLength(1); ++j)
      {
        block[i + 1, j + 1] = (bare_block[i, j] == C.X) ? CellType.BLOCK : CellType.EMPTY;
      }
    }

    // Determine the outlines.
    for (int i = 0; i < block.GetLength(0); ++i)
    {
      for (int j = 0; j < block.GetLength(1); ++j)
      {
        block[i, j] = GetBlockType(i, j);
      }
    }
  }

  private CellType GetBlockType(int i, int j)
  {
    if (block[i, j] == CellType.BLOCK)
    {
      return CellType.BLOCK;
    }

    if (
      (i > 0 && block[i - 1, j] == CellType.BLOCK) ||
      (j > 0 && block[i, j - 1] == CellType.BLOCK) ||
      (i + 1 < block.GetLength(0) && block[i + 1, j] == CellType.BLOCK) ||
      (j + 1 < block.GetLength(1) && block[i, j + 1] == CellType.BLOCK))
    {
      return CellType.OUTLINE_SIDE;
    }
    if (i > 0)
    {
      if (
        (j > 0 && block[i - 1, j - 1] == CellType.BLOCK) ||
        (j + 1 < block.GetLength(0) && block[i - 1, j + 1] == CellType.BLOCK))
      {
        return CellType.OUTLINE_CORNER;
      }
    }
    if (i + 1 < block.GetLength(0))
    {
      if (
        (j > 0 && block[i + 1, j - 1] == CellType.BLOCK) ||
        (j + 1 < block.GetLength(0) && block[i + 1, j + 1] == CellType.BLOCK))
      {
        return CellType.OUTLINE_CORNER;
      }
    }
    return CellType.EMPTY;
  }

  public static Block B1_1()
  {
    return new Block(new[,] { { C.X }
    });
  }

  public static Block B2_11()
  {
    return new Block(new[,] { { C.X, C.X }
    });
  }

  public static Block B3_11_01()
  {
    return new Block(new[,] { { C.X, C.X }, { C.O, C.X }
    });
  }

  public static Block B3_111()
  {
    return new Block(new[,] { { C.X, C.X, C.X }
    });
  }

  public static Block B4_1111()
  {
    return new Block(new[,] { { C.X, C.X, C.X, C.X }
    });
  }

  public static Block B4_010_111()
  {
    return new Block(new[,] { { C.O, C.X, C.O }, { C.X, C.X, C.X }
    });
  }

  public static Block B4_100_111()
  {
    return new Block(new[,] { { C.X, C.O, C.O }, { C.X, C.X, C.X }
    });
  }

  public static Block B4_110_011()
  {
    return new Block(new[,] { { C.X, C.X, C.O }, { C.O, C.X, C.X }
    });
  }

  public static Block B4_11_11()
  {
    return new Block(new[,] { { C.X, C.X }, { C.X, C.X }
    });
  }

  public static Block B5_11111()
  {
    return new Block(new[,] { { C.X, C.X, C.X, C.X, C.X }
    });
  }

  public static Block B5_1000_1111()
  {
    return new Block(new[,] { { C.X, C.O, C.O, C.O }, { C.X, C.X, C.X, C.X }
    });
  }

  public static Block B5_1100_0111()
  {
    return new Block(new[,] { { C.X, C.X, C.O, C.O }, { C.O, C.X, C.X, C.X }
    });
  }

  public static Block B5_0100_1111()
  {
    return new Block(new[,] { { C.O, C.X, C.O, C.O }, { C.X, C.X, C.X, C.X }
    });
  }

  public static Block B5_010_010_111()
  {
    return new Block(new[,] { { C.O, C.X, C.O }, { C.O, C.X, C.O }, { C.X, C.X, C.X }
    });
  }

  public static Block B5_100_100_111()
  {
    return new Block(new[,] { { C.X, C.O, C.O }, { C.X, C.O, C.O }, { C.X, C.X, C.X }
    });
  }

  public static Block B5_100_111_001()
  {
    return new Block(new[,] { { C.X, C.O, C.O }, { C.X, C.X, C.X }, { C.O, C.O, C.X }
    });
  }

  public static Block B5_110_011_001()
  {
    return new Block(new[,] { { C.X, C.X, C.O }, { C.O, C.X, C.X }, { C.O, C.O, C.X }
    });
  }

  public static Block B5_110_011_010()
  {
    return new Block(new[,] { { C.X, C.X, C.O }, { C.O, C.X, C.X }, { C.O, C.X, C.O }
    });
  }

  public static Block B5_010_111_010()
  {
    return new Block(new[,] { { C.O, C.X, C.O }, { C.X, C.X, C.X }, { C.O, C.X, C.O }
    });
  }

  public static Block B5_111_110()
  {
    return new Block(new[,] { { C.X, C.X, C.X }, { C.X, C.X, C.O }
    });
  }

  public static Block B5_111_101()
  {
    return new Block(new[,] { { C.X, C.X, C.X }, { C.X, C.O, C.X }
    });
  }
}