using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DominoBoard : MonoBehaviour
{
    public DominoPiece dominoPiece;
    public bool hasThisBeenPlayed;

    public GameObject rightPosition;
    public GameObject leftPosition;

    public bool isRightPositionFree;
    public bool isLeftPositionFree;
    public void Start()
    {
        this.GetComponent<Image>().enabled = false;
        isRightPositionFree = true;
        isLeftPositionFree = true;
    }

    public void EnableThis(DominoPiece piece)
    {
        dominoPiece = piece;
        this.GetComponent<Image>().sprite = dominoPiece.sprite;
        this.GetComponent<Image>().enabled = true;
    }

}
