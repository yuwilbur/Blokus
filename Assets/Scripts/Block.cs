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

  private GameObject m_renderable;
  private HashSet<Piece> m_blocks = new HashSet<Piece>();
  private HashSet<Piece> m_sides = new HashSet<Piece>();
  private HashSet<Piece> m_corners = new HashSet<Piece>();

  public void AddPiece(Vector3 position, Piece.Type type)
  {
    Piece piece = CreatePiece(position);
    piece.m_type = type;
    switch (type)
    {
      case Piece.Type.BLOCK:
        m_blocks.Add(piece);
        break;
      case Piece.Type.OUTLINE_SIDE:
        m_sides.Add(piece);
        break;
      case Piece.Type.OUTLINE_CORNER:
        m_corners.Add(piece);
        break;
      case Piece.Type.EMPTY:
      default:
        break;
    }
  }

  public void SetCollider(Vector3 size)
  {
    if (this.gameObject.GetComponent<BoxCollider>() == null)
    {
      this.gameObject.AddComponent<BoxCollider>();
    }
    this.gameObject.GetComponent<BoxCollider>().size = size;
  }

  public void InitRenderables()
  {
    m_renderable.transform.parent = this.transform;
    m_renderable.transform.localPosition = Vector3.zero;
    foreach (Piece block in m_blocks)
    {
      Piece piece = CreatePiece(block.transform.localPosition);
      piece.transform.parent = m_renderable.transform;
      piece.GetComponent<Collider>().enabled = false;
    }
  }

  private Piece CreatePiece(Vector3 position)
  {
    GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
    piece.transform.parent = transform;
    piece.transform.localPosition = position;
    piece.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
    piece.AddComponent<Piece>();
    return piece.GetComponent<Piece>();
  }

  private void UpdateColors()
  {
    foreach (Transform child in m_renderable.transform)
    {
      child.GetComponent<Renderer>().material.color = m_color;
    }

    foreach (Piece piece in m_blocks)
    {
      piece.GetComponent<Renderer>().material.color = Color.Lerp(m_color, Color.black, 0.0f); ;
      Color color = piece.GetComponent<Renderer>().material.color;
      color.a = 0.5f;
      piece.GetComponent<Renderer>().material.color = color;
      piece.GetComponent<Renderer>().enabled = false;
    }
    foreach (Piece piece in m_sides)
    {
      piece.GetComponent<Renderer>().material.color = Color.Lerp(m_color, Color.black, 0.5f);
      Color color = piece.GetComponent<Renderer>().material.color;
      color.a = 0.5f;
      piece.GetComponent<Renderer>().material.color = color;
      piece.GetComponent<Renderer>().enabled = false;
    }
    foreach (Piece piece in m_corners)
    {
      piece.GetComponent<Renderer>().material.color = Color.Lerp(m_color, Color.black, 0.9f);
      Color color = piece.GetComponent<Renderer>().material.color;
      color.a = 0.5f;
      piece.GetComponent<Renderer>().material.color = color;
      piece.GetComponent<Renderer>().enabled = false;
    }
  }

  void Awake()
  {
    m_renderable = new GameObject();
    m_renderable.name = "Renderables";
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
