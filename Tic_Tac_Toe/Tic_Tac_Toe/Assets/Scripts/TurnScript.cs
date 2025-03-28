using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TurnScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images;
    private Image image;
    private bool _isPlayable = true;
    private GameManager gameManager;

    [SerializeField]
    private int col = -1;
    [SerializeField]
    private int row = -1;

    private int mySprite = -111;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        image = transform.GetChild(0).GetComponent<Image>();
        gameManager.a_ResetGame += resetBox;
    }

    public void changeBox(/*Image image*/){
        if (_isPlayable)
        {
            mySprite = gameManager.getCurrentTurn();
            if(gameManager.makeMove(row, col, mySprite))
            {
                image.sprite = images[Mathf.Abs(mySprite)];
                _isPlayable = false;
            }
        }
    }

    public void resetBox()
    {
        image.sprite = images[2];
        _isPlayable = true;
    }
}
