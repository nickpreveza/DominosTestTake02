using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DominoPiece : ScriptableObject
{
    public bool isDoublePiece;
    public string pieceName;
    public Sprite sprite;
    public int upValue;
    public int downValue;
    public int totalValue;

}
