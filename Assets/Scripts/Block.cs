using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  private Color m_color;
  public Color color
  {
    get { return m_color; }
    set
    {
      m_color = value;
      UpdateColors();
    }
  }

  private HashSet<GameObject> m_blocks = new HashSet<GameObject>();
  private HashSet<GameObject> m_sides = new HashSet<GameObject>();
  private HashSet<GameObject> m_corners = new HashSet<GameObject>();

  public void AddBlock(Vector3 position)
  {
    m_blocks.Add(CreatePiece(position));
  }

  public void AddSide(Vector3 position)
  {
    m_sides.Add(CreatePiece(position));
  }

  public void AddCorner(Vector3 position)
  {
    m_corners.Add(CreatePiece(position));
  }

  public Piece.Type GetPieceType(GameObject piece)
  {
    return Piece.Type.BLOCK;
  }

  private GameObject CreatePiece(Vector3 position)
  {
    GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
    piece.transform.localPosition = position;
    piece.transform.parent = transform;
    piece.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
    return piece;
  }

  private void UpdateColors()
  {
    foreach (GameObject block in m_blocks)
    {
      block.GetComponent<Renderer>().material.color = m_color;
    }
    foreach (GameObject side in m_sides)
    {
      side.GetComponent<Renderer>().material.color = Color.Lerp(m_color, Color.black, 0.5f);
      Color color = side.GetComponent<Renderer>().material.color;
      color.a = 0.0f;
      side.GetComponent<Renderer>().material.color = color;
    }
    foreach (GameObject corner in m_corners)
    {
      corner.GetComponent<Renderer>().material.color = Color.Lerp(m_color, Color.black, 0.9f);
      Color color = corner.GetComponent<Renderer>().material.color;
      color.a = 0.0f;
      corner.GetComponent<Renderer>().material.color = color;
    }
  }

  private void UpdateCollider()
  {

  }

  // Start is called before the first frame update
  void Start()
  {
    UpdateColors();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
