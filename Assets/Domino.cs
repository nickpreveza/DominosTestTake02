using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domino : MonoBehaviour
{
    public DominoPiece dominoPiece;
    public GameObject availabilityLayer;
    public bool isInPlayer1Hand;
    public bool isInPlayer2Hand;
    public bool canBePlayedThisTurn;
    public bool rightOrientation;
    public void Start()
    {
        canBePlayedThisTurn = true;


        if (dominoPiece == null)
        {
            
            availabilityLayer.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.GetComponent<Image>().sprite = dominoPiece.sprite;
        }
        
    }

    public void UpdateGraphics()
    {

        if (dominoPiece == null)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.GetComponent<Image>().sprite = dominoPiece.sprite;
        }
    }

    public void UpdateAvailability()
    {
        canBePlayedThisTurn = !canBePlayedThisTurn;
        availabilityLayer.SetActive(!canBePlayedThisTurn);
    }

    public void EnableThis()
    {
        canBePlayedThisTurn = true;
        availabilityLayer.SetActive(false);
    }

    public void DisableThis()
    {
        canBePlayedThisTurn = false;
        availabilityLayer.SetActive(true);
    }

    public void Played()
    {

    }

}
