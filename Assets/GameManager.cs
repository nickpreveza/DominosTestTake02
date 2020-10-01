using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum State
    {
        Setup,
        Player1Turn,
        Player2Turn,
        Win,
        Loss,
        Tie
    }

    public State currentState;
    public GameObject selectedPiece;
    public DominoHolder holder;
    public Board board;
    public CanvasInteractions interactions;

    private static GameObject player1, player2;

    public static bool gameOver = false;

    public GameObject[] player1hand;
    public GameObject[] player2hand;
    public int startingHand;

    public int highestDouble;
    public int indexOfBiggestPiece;
    public int playerWithBiggestPiece;

    public int playableCardsforP1;
    public int playerableCardsforP2;

    public GameObject bestPlayer2CandidatePiece;

    void Start()
    {
        highestDouble = 0;
        holder = FindObjectOfType<DominoHolder>();
        board = FindObjectOfType<Board>();

        currentState = State.Setup;
        StartCoroutine(SetUp());
    }

    IEnumerator SetUp()
    {
       for (int i = 0; i < player1hand.Length; i++)
       {
            int random = UnityEngine.Random.Range(0, holder.dominoPieces.Length);
            DominoPiece dominoPiece = holder.dominoPieces[random];
            RemoveAt<DominoPiece>(ref holder.dominoPieces, random);
            player1hand[i].GetComponent<Domino>().dominoPiece = dominoPiece;
            player1hand[i].SetActive(true);
            player1hand[i].GetComponent<Domino>().isInPlayer1Hand = true;
            player1hand[i].GetComponent<Domino>().UpdateGraphics();
            player1hand[i].GetComponent<Domino>().UpdateAvailability();
            if (player1hand[i].GetComponent<Domino>().dominoPiece.isDoublePiece)
            {
                player1hand[i].GetComponent<Domino>().UpdateAvailability();
                if (player1hand[i].GetComponent<Domino>().dominoPiece.totalValue > highestDouble)
                {
                    highestDouble = player1hand[i].GetComponent<Domino>().dominoPiece.totalValue;
                    indexOfBiggestPiece = i;
                    playerWithBiggestPiece = 1;
                }
            }
        }

        yield return new WaitForSeconds[1];

        for (int i = 0; i < player2hand.Length; i++)
        {
            int random = UnityEngine.Random.Range(0, holder.dominoPieces.Length);
            DominoPiece dominoPiece = holder.dominoPieces[random];
            RemoveAt<DominoPiece>(ref holder.dominoPieces, random);
            player2hand[i].GetComponent<Domino>().dominoPiece = dominoPiece;
            player2hand[i].SetActive(true);
            player2hand[i].GetComponent<Domino>().isInPlayer2Hand = true;
            player2hand[i].GetComponent<Domino>().UpdateGraphics();
            player2hand[i].GetComponent<Domino>().UpdateAvailability();
            if (player2hand[i].GetComponent<Domino>().dominoPiece.isDoublePiece)
            {
                player2hand[i].GetComponent<Domino>().UpdateAvailability();
                if (player2hand[i].GetComponent<Domino>().dominoPiece.totalValue > highestDouble)
                {
                    highestDouble = player2hand[i].GetComponent<Domino>().dominoPiece.totalValue;
                    indexOfBiggestPiece = i;
                    playerWithBiggestPiece = 2;
                }
            }
        }

        yield return new WaitForSeconds[1];


        if (playerWithBiggestPiece == 1)
        {
            currentState = State.Player1Turn;
            Debug.Log(currentState);
        }

        if (playerWithBiggestPiece == 2)
        {
            currentState = State.Player2Turn;
            StartCoroutine(Player2Turn());
            Debug.Log(currentState);
        }
        
    }

    IEnumerator Player2Turn()
    {
        Debug.Log("AI is now Playing");

        yield return new WaitForSeconds(1);


        //Calculate Best Choice

        List<int> possibleMoves = new List<int>();

        int highestValue = 0;
        int savedIndex = 0;

        for (int i = 0; i < player2hand.Length; i++)
        {
            if (player2hand[i].GetComponent<Domino>().canBePlayedThisTurn)
            {
                if (player2hand[i].GetComponent<Domino>().dominoPiece.totalValue > highestValue)
                {
                    highestValue = player2hand[i].GetComponent<Domino>().dominoPiece.totalValue;
                    savedIndex = i;
                }
            }
        }
        // Play
        Debug.Log("I think I should play" + player2hand[savedIndex].GetComponent<Domino>().dominoPiece.name);

        yield return new WaitForSeconds[1];
        //DominoPiece dominoPiece = player2hand[savedIndex].GetComponent<DominoPiece>();
        board.PlayPiece(player2hand[savedIndex].GetComponent<Domino>().dominoPiece);
        //player2hand[savedIndex].GetComponent<Domino>().Played();
        player2hand[savedIndex].SetActive(false);
        RemoveAt<GameObject>(ref player2hand, savedIndex);

        CalculateAvailability(player2hand);
        currentState = State.Player1Turn;
        CalculateAvailability(player1hand);
        if (playableCardsforP1 == 1)
        {
            currentState = State.Player2Turn;
            StartCoroutine(Player2Turn());
        }
    }

    public void CalculateAvailability(GameObject[] array)
    {
        playerableCardsforP2 = 0;
        playableCardsforP1 = 0;
        for (int i = 0; i < array.Length; i++)
        {
            
            array[i].GetComponent<Domino>().DisableThis();

            if (board.leftPositionValue == array[i].GetComponent<Domino>().dominoPiece.downValue)
            {
                array[i].GetComponent<Domino>().EnableThis();
                playerableCardsforP2++;
                playableCardsforP1++;
            }

            if (board.leftPositionValue == array[i].GetComponent<Domino>().dominoPiece.upValue)
            {
                array[i].GetComponent<Domino>().EnableThis();
                playerableCardsforP2++;
                playableCardsforP1++;
            }

            if (board.rightPositionValue == array[i].GetComponent<Domino>().dominoPiece.downValue)
            {
                array[i].GetComponent<Domino>().EnableThis();
                playerableCardsforP2++;
                playableCardsforP1++;
            }

            if (board.rightPositionValue == array[i].GetComponent<Domino>().dominoPiece.upValue)
            {
                array[i].GetComponent<Domino>().EnableThis();
                playerableCardsforP2++;
                playableCardsforP1++;
            }
        }  
    }

    
    public static void RemoveAt<T>(ref T[] array, int index)
    {
        array[index] = array[array.Length - 1];
        Array.Resize(ref array, array.Length - 1);
    }



    void Update()
    {
        if (currentState == State.Player1Turn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (interactions.interacted != null)
                {
                    selectedPiece = interactions.interacted;

                    for (int i = 0; i < player1hand.Length; i++)
                    {
                        if (selectedPiece == player1hand[i])
                        {
                            if (!selectedPiece.GetComponent<Domino>().availabilityLayer.activeSelf)
                            {
                                board.PlayPiece(selectedPiece.GetComponent<Domino>().dominoPiece);
                                player1hand[i].SetActive(false);
                                RemoveAt<GameObject>(ref player1hand, i);
                                CalculateAvailability(player2hand);
                                if (playerableCardsforP2 > 0)
                                {
                                    currentState = State.Player2Turn;
                                    StartCoroutine(Player2Turn());
                                }
                                else
                                {
                                    currentState = State.Player1Turn;
                                }
                                
                                Debug.Log("Play piece by the name: " + selectedPiece.GetComponent<Domino>().dominoPiece.name);
                            }
                            else if (selectedPiece == player1hand[i])
                            {
                                Debug.Log("This cannot be played this turn");
                            }
                            //add this later on 
                        }
                    }
                }
                //if is player turn and block can be player

            }
        }
      
    }

}
