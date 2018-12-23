using static UnityEngine.Debug;
using static UnityEngine.Debug;

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Block {

    [Description ("CellType")]
    private enum C {
        [Description ("EMPTY")] O, [Description ("BLOCK")] X
    }

    private C[, ] block_structure;

    private int test = 0;

    private Block (C[, ] block_structure) {
        this.block_structure = block_structure;
    }

    public void setTest (int i) {
        test = i;
    }

    public int getTest () {
        return test;
    }

    public static Block B1_1 () {
        return new Block (new [, ] { { C.X }
        });
    }

    public static Block B2_11 () {
        return new Block (new [, ] { { C.X, C.X }
        });
    }

    public static Block B3_11_01 () {
        return new Block (new [, ] { { C.X, C.X }, { C.O, C.X }
        });
    }

    public static Block B3_111 () {
        return new Block (new [, ] { { C.X, C.X, C.X }
        });
    }

    public static Block B4_1111 () {
        return new Block (new [, ] { { C.X, C.X, C.X, C.X }
        });
    }

    public static Block B4_010_111 () {
        return new Block (new [, ] { { C.O, C.X, C.O }, { C.X, C.X, C.X }
        });
    }

    public static Block B4_100_111 () {
        return new Block (new [, ] { { C.X, C.O, C.O }, { C.X, C.X, C.X }
        });
    }

    public static Block B4_110_011 () {
        return new Block (new [, ] { { C.X, C.X, C.O }, { C.O, C.X, C.X }
        });
    }

    public static Block B4_11_11 () {
        return new Block (new [, ] { { C.X, C.X }, { C.X, C.X }
        });
    }

    public static Block B5_11111 () {
        return new Block (new [, ] { { C.X, C.X, C.X, C.X, C.X }
        });
    }

    public static Block B5_1000_1111 () {
        return new Block (new [, ] { { C.X, C.O, C.O, C.O }, { C.X, C.X, C.X, C.X }
        });
    }

    public static Block B5_1100_0111 () {
        return new Block (new [, ] { { C.X, C.X, C.O, C.O }, { C.O, C.X, C.X, C.X }
        });
    }

    public static Block B5_0100_1111 () {
        return new Block (new [, ] { { C.O, C.X, C.O, C.O }, { C.X, C.X, C.X, C.X }
        });
    }

    public static Block B5_010_010_111 () {
        return new Block (new [, ] { { C.O, C.X, C.O }, { C.O, C.X, C.O }, { C.X, C.X, C.X }
        });
    }

    public static Block B5_100_100_111 () {
        return new Block (new [, ] { { C.X, C.O, C.O }, { C.X, C.O, C.O }, { C.X, C.X, C.X }
        });
    }

    public static Block B5_100_111_001 () {
        return new Block (new [, ] { { C.X, C.O, C.O }, { C.X, C.X, C.X }, { C.O, C.O, C.X }
        });
    }

    public static Block B5_110_011_001 () {
        return new Block (new [, ] { { C.X, C.X, C.O }, { C.O, C.X, C.X }, { C.O, C.O, C.X }
        });
    }

    public static Block B5_110_011_010 () {
        return new Block (new [, ] { { C.X, C.X, C.O }, { C.O, C.X, C.X }, { C.O, C.X, C.O }
        });
    }

    public static Block B5_010_111_010 () {
        return new Block (new [, ] { { C.O, C.X, C.O }, { C.X, C.X, C.X }, { C.O, C.X, C.O }
        });
    }

    public static Block B5_111_110 () {
        return new Block (new [, ] { { C.X, C.X, C.X }, { C.X, C.X, C.O }
        });
    }

    public static Block B5_111_101 () {
        return new Block (new [, ] { { C.X, C.X, C.X }, { C.X, C.O, C.X }
        });
    }
}