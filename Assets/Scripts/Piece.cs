﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
  public enum Type
  {
    EMPTY = 0, OUTLINE_CORNER = 1, OUTLINE_SIDE = 2, BLOCK = 3
  }

  public Type m_type;
}
