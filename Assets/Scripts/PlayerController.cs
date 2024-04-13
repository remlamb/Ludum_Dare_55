using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class PlayerController : MonoBehaviour
{
    public enum GameState
    {
        Summoning,
        Placing
    }

    struct inputSummon
    {
        public bool isSwipingDown;
        public bool isSwipingLeft;
        public bool isSwipingRight;
        public bool isSwipingUp;
    }

    private InputSwipeMobile input;
    [SerializeField] private List<inputSummon> inputsSummon = new List<inputSummon>();
    [SerializeField] private GameObject[] UI;
    [SerializeField] private Sprite[] arrowSprites;
    [SerializeField] private Sprite emptySprite;
    private GameState currentGameState;


    // Start is called before the first frame update
    void Start()
    {
        input = gameObject.GetComponent<InputSwipeMobile>();
        currentGameState = GameState.Summoning;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == GameState.Summoning)
        {
            inputSummon newInput = new inputSummon();
            if (input.isSwipingDown || Input.GetKeyUp(KeyCode.DownArrow))
            {
                newInput.isSwipingLeft = false;
                newInput.isSwipingRight = false;
                newInput.isSwipingUp = false;
                newInput.isSwipingDown = true;
                inputsSummon.Add(newInput);
            }
            if (input.isSwipingLeft || Input.GetKeyUp(KeyCode.LeftArrow))
            {

                newInput.isSwipingLeft = true;
                newInput.isSwipingRight = false;
                newInput.isSwipingUp = false;
                newInput.isSwipingDown = false;
                inputsSummon.Add(newInput);
            }
            if (input.isSwipingRight || Input.GetKeyUp(KeyCode.RightArrow))
            {

                newInput.isSwipingLeft = false;
                newInput.isSwipingRight = true;
                newInput.isSwipingUp = false;
                newInput.isSwipingDown = false;
                inputsSummon.Add(newInput);
            }
            if (input.isSwipingUp || Input.GetKeyUp(KeyCode.UpArrow))
            {

                newInput.isSwipingLeft = false;
                newInput.isSwipingRight = false;
                newInput.isSwipingUp = true;
                newInput.isSwipingDown = false;
                inputsSummon.Add(newInput);
            }



            if (inputsSummon != null)
            {
                for (int i = 0; i < inputsSummon.Count; i++)
                {
                    Sprite currentArowSprite = null;
                    if (inputsSummon[i].isSwipingLeft)
                    {
                        currentArowSprite = arrowSprites[0];
                    }
                    if (inputsSummon[i].isSwipingDown)
                    {
                        currentArowSprite = arrowSprites[1];
                    }
                    if (inputsSummon[i].isSwipingRight)
                    {
                        currentArowSprite = arrowSprites[2];
                    }
                    if (inputsSummon[i].isSwipingUp)
                    {
                        currentArowSprite = arrowSprites[3];
                    }
                    UI[i].gameObject.GetComponent<Image>().sprite = currentArowSprite;
                }
            }

            if (inputsSummon.Count > 3)
            {
                inputsSummon.Clear();
                clearUI();
            }
        }
    }


    void clearUI()
    {
        foreach (var uiImage in UI)
        {
            uiImage.gameObject.GetComponent<Image>().sprite = emptySprite;
        }
    }
}
