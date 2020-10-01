using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject startingDomino;

    public GameObject lastPlayedRightDomino;
    public GameObject lastPlayedLeftDomino;

    public GameObject dominoToEnable;

    public int totalPlayedPieces;

    public int rightPositionValue;
    public int leftPositionValue;

    public bool hasBeenPlaced;
    private void Start()
    {
        totalPlayedPieces = 0;
    }

    public void PlayPiece(DominoPiece piece)
    {
        hasBeenPlaced = false;

        if (totalPlayedPieces == 0)
        {
            startingDomino.SetActive(true);
            startingDomino.GetComponent<DominoBoard>().EnableThis(piece);

            startingDomino.GetComponent<DominoBoard>().isRightPositionFree = true;
            startingDomino.GetComponent<DominoBoard>().isLeftPositionFree = true;

            rightPositionValue = startingDomino.GetComponent<DominoBoard>().dominoPiece.upValue;
            leftPositionValue = startingDomino.GetComponent<DominoBoard>().dominoPiece.downValue;

            lastPlayedRightDomino = startingDomino;
            lastPlayedLeftDomino = startingDomino;

            totalPlayedPieces++;
            hasBeenPlaced = true;
        }
        else if (totalPlayedPieces > 0 && totalPlayedPieces <= 14)
        {
            if (leftPositionValue == piece.downValue || rightPositionValue == piece.downValue || leftPositionValue == piece.upValue || leftPositionValue == piece.downValue)
            {
                if (leftPositionValue == piece.downValue && !hasBeenPlaced)
                {
                    lastPlayedLeftDomino.GetComponent<DominoBoard>().leftPosition.SetActive(true);
                    dominoToEnable = lastPlayedLeftDomino.GetComponent<DominoBoard>().leftPosition;
                    dominoToEnable.GetComponent<DominoBoard>().EnableThis(piece);
                    lastPlayedLeftDomino = dominoToEnable;
                    leftPositionValue = piece.upValue;
                    hasBeenPlaced = true;
                    Debug.Log("Matching leftSide with downValue");
                }
                if (leftPositionValue == piece.upValue && !hasBeenPlaced)
                {
                    lastPlayedLeftDomino.GetComponent<DominoBoard>().leftPosition.SetActive(true);
                    dominoToEnable = lastPlayedLeftDomino.GetComponent<DominoBoard>().leftPosition;
                    dominoToEnable.GetComponent<DominoBoard>().EnableThis(piece);
                    lastPlayedLeftDomino = dominoToEnable;
                    //lastPlayedLeftDomino.transform.Rotate(0, 0, -90);
                    leftPositionValue = piece.downValue;
                    hasBeenPlaced = true;

                    Debug.Log("Matching leftSide with upValue");
                }

                if (rightPositionValue == piece.downValue && !hasBeenPlaced)
                {
                    lastPlayedRightDomino.GetComponent<DominoBoard>().rightPosition.SetActive(true);
                    dominoToEnable = lastPlayedRightDomino.GetComponent<DominoBoard>().rightPosition;
                    dominoToEnable.GetComponent<DominoBoard>().EnableThis(piece);
                    lastPlayedRightDomino = dominoToEnable;
                    rightPositionValue = piece.upValue;
                    //lastPlayedRightDomino.transform.Rotate(0, 0, -90);
                    hasBeenPlaced = true;
                    Debug.Log("Matching right side with downValue");
                }
                if (rightPositionValue == piece.upValue && !hasBeenPlaced)
                {
                    lastPlayedRightDomino.GetComponent<DominoBoard>().rightPosition.SetActive(true);
                    dominoToEnable = lastPlayedRightDomino.GetComponent<DominoBoard>().rightPosition;
                    dominoToEnable.GetComponent<DominoBoard>().EnableThis(piece);
                    lastPlayedRightDomino = dominoToEnable;
                    leftPositionValue = piece.downValue;
                    //lastPlayedRightDomino.transform.Rotate(0, 0, -90);
                    
                    hasBeenPlaced = true;

                    Debug.Log("Matching right side with upValue");
                }
            }

        }
    }
}
