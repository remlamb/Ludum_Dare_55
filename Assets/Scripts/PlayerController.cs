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

        public inputSummon(bool down, bool left, bool right, bool up)
        {
            isSwipingDown = down;
            isSwipingLeft = left;
            isSwipingRight = right;
            isSwipingUp = up;
        }
        // Override == operator
        public static bool operator ==(inputSummon a, inputSummon b)
        {
            return a.isSwipingDown == b.isSwipingDown &&
                   a.isSwipingLeft == b.isSwipingLeft &&
                   a.isSwipingRight == b.isSwipingRight &&
                   a.isSwipingUp == b.isSwipingUp;
        }

        // Override != operator
        public static bool operator !=(inputSummon a, inputSummon b)
        {
            return !(a == b);
        }
    }

    private inputSummon inputPool = new inputSummon(true, false, false, false);
    private inputSummon inputPool2 = new inputSummon(false, true, false, false);
    private inputSummon inputPool3 = new inputSummon(false, false, true, false);
    private inputSummon inputPool4 = new inputSummon(false, false, false, true);
    [SerializeField] private List<inputSummon> InputsPool = new List<inputSummon>();


    private InputSwipeMobile input;
    [SerializeField] private List<inputSummon> inputsSummon = new List<inputSummon>();
    [SerializeField] private GameObject[] UI;
    [SerializeField] private Sprite[] arrowSprites;
    [SerializeField] private Sprite emptySprite;
    private GameState currentGameState;


    [SerializeField] private List<inputSummon> monsterInputs = new List<inputSummon>();
    [SerializeField] private List<inputSummon> monster2Inputs = new List<inputSummon>();
    [SerializeField] private List<inputSummon> monster3Inputs = new List<inputSummon>();
    private inputSummon monsterInput;

    [SerializeField] private GameObject[] Monster1UI;
    [SerializeField] private GameObject[] Monster2UI;
    [SerializeField] private GameObject[] Monster3UI;

    //Summoning
    private Vector3 choosenPos;
    private Vector3 targetPosition;
    [SerializeField] private GameObject[] deamons;
    private GameObject currentDeamon;

    // Start is called before the first frame update
    void Start()
    {
        input = gameObject.GetComponent<InputSwipeMobile>();
        currentGameState = GameState.Summoning;

        monsterInput = new inputSummon(true, false, false, false);

        currentDeamon = null;

        InputsPool.Add(inputPool);
        InputsPool.Add(inputPool2);
        InputsPool.Add(inputPool3);
        InputsPool.Add(inputPool4);

        RandomPool(ref monsterInputs);
        RandomPool(ref monster2Inputs);
        RandomPool(ref monster3Inputs);
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
        SpawnOnClick();
        DrawUIEnemy(monsterInputs, Monster1UI[0], Monster1UI[1], Monster1UI[2]);
        DrawUIEnemy(monster2Inputs, Monster2UI[0], Monster2UI[1], Monster2UI[2]);
        DrawUIEnemy(monster3Inputs, Monster3UI[0], Monster3UI[1], Monster3UI[2]);
    }

    void ManageInput()
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

            if (inputsSummon.Count >= 3)
            {
                if (inputsSummon[0] == monsterInputs[0] && inputsSummon[1] == monsterInputs[1] && inputsSummon[2] == monsterInputs[2])
                {
                    currentDeamon = deamons[0];
                    RandomPool(ref monsterInputs);
                    currentGameState = GameState.Placing;
                }
                else if (inputsSummon[0] == monster2Inputs[0] && inputsSummon[1] == monster2Inputs[1] &&
                         inputsSummon[2] == monster2Inputs[2])
                {
                    currentDeamon = deamons[1];
                    RandomPool(ref monster2Inputs);
                    currentGameState = GameState.Placing;
                }
                else if (inputsSummon[0] == monster3Inputs[0] && inputsSummon[1] == monster3Inputs[1] &&
                         inputsSummon[2] == monster3Inputs[2])
                {
                    currentDeamon = deamons[1];
                    RandomPool(ref monster3Inputs);
                    currentGameState = GameState.Placing;
                }
                else
                {
                    inputsSummon.Clear();
                    clearUI();
                }


            }
        }

        SpawnOnClick();
    }



    void clearUI()
    {
        foreach (var uiImage in UI)
        {
            uiImage.gameObject.GetComponent<Image>().sprite = emptySprite;
        }
    }


    //Summoning;
    void SpawnOnClick()
    {
        if (currentGameState == GameState.Placing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                choosenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                choosenPos.z = gameObject.transform.position.z;
                //Debug.Log("ClickPos:" + choosenPos.x);

                //TODO flip Deamon en fonction de sa position sur l ecran
                if (currentDeamon != null)
                {
                    Instantiate(currentDeamon, choosenPos, Quaternion.identity);
                    inputsSummon.Clear();
                    clearUI();
                    currentGameState = GameState.Summoning;

                }
                else
                {
                    //Debug.Log("Take A deamon");
                }
            }
        }
    }


    public void ChooseDeamon(int deamon)
    {
        currentDeamon = deamons[deamon];
    }


    void RandomPool(ref List<inputSummon> summonInput)
    {
        summonInput.Clear();
        summonInput.Add(InputsPool[Random.Range(0, InputsPool.Count)]);
        summonInput.Add(InputsPool[Random.Range(0, InputsPool.Count)]);
        summonInput.Add(InputsPool[Random.Range(0, InputsPool.Count)]);
    }

    void DrawUIEnemy(List<inputSummon> summonInput, GameObject image1, GameObject image2, GameObject image3)
    {
        image1.GetComponent<Image>().sprite = FindSpriteByInput(summonInput, 0);
        image2.GetComponent<Image>().sprite = FindSpriteByInput(summonInput, 1);
        image3.GetComponent<Image>().sprite = FindSpriteByInput(summonInput, 2);
    }

    Sprite FindSpriteByInput(List<inputSummon> summonInput, int iterator)
    {
        Sprite sprite = null;
        if (summonInput[iterator].isSwipingLeft)
        {
            sprite = arrowSprites[0];
        }
        if (summonInput[iterator].isSwipingDown)
        {
            sprite = arrowSprites[1];
        }
        if (summonInput[iterator].isSwipingRight)
        {
            sprite = arrowSprites[2];
        }
        if (summonInput[iterator].isSwipingUp)
        {
            sprite = arrowSprites[3];
        }
        return sprite;
    }

}