using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //Just a manager
    public static BoardManager sharedInstance;
    //All the candies
    public List<Sprite> candyPrefabs = new List<Sprite>();
    //The candy that is being selected
    public GameObject currentCandy;
    //Board size
    public int xSize, ySize;
    public int xMargin, yMargin;

    private GameObject[,] candies;
    private bool isTheSame;
    public bool isShifting { get; set; }

    private CandyController selectedCandy;
    public const int MinCandiesToMatch = 2;

    void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
        else
            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 offset = currentCandy.GetComponent<BoxCollider2D>().size;
        InitBoard(offset);
    }

    void InitBoard(Vector2 offset)
    {
        candies = new GameObject[xSize, ySize];

        float startX = this.transform.position.x;
        float startY = this.transform.position.y;

        int idx = -1;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newCandy = Instantiate(
                currentCandy,
                    new Vector3(startX + (offset.x * x),
                                startY + (offset.y * y),
                                0),
                    currentCandy.transform.rotation
                 );
                newCandy.name = string.Format("Candy[{0}][{1}]", x, y);

                do
                {
                    idx = Random.Range(0, candyPrefabs.Count);
                } while ((x > 0 && idx == candies[x - 1, y].GetComponent<CandyController>().id) ||
                        (y > 0 && idx == candies[x, y - 1].GetComponent<CandyController>().id));



                Sprite sprite = candyPrefabs[idx];
                newCandy.GetComponent<SpriteRenderer>().sprite = sprite;
                newCandy.GetComponent<CandyController>().id = idx;

                newCandy.transform.parent = this.transform;
                candies[x, y] = newCandy;

            }
        }
    }

    public IEnumerator FindNullCandies()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (candies[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    yield return StartCoroutine(MakeCandiesFall(x, y));
                    break;
                }
            }
        }

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                candies[x, y].GetComponent<CandyController>().FindAllMatches();
            }
        }

    }

    public IEnumerator MakeCandiesFall(int x, int yStart, float shiftDelay = 0.25f)
    {
        isShifting = true;

        List<SpriteRenderer> renderes = new List<SpriteRenderer>();
        int nullCandies = 0;

        for (int y = yStart; y < ySize; y++)
        {
            SpriteRenderer spriteRenderer = candies[x, y].GetComponent<SpriteRenderer>();
            if (spriteRenderer.sprite == null)
            {
                nullCandies++;
            }
            renderes.Add(spriteRenderer);
        }

        for (int i = 0; i < nullCandies; i++)
        {
            yield return new WaitForSeconds(shiftDelay);
            for (int j = 0; j < renderes.Count - 1; j++)
            {
                renderes[j].sprite = renderes[j + 1].sprite;
                Sprite newCandy = GetNewCandy(x, ySize - 1);
                renderes[j + 1].sprite = newCandy;
            }
        }
        isShifting = false;
    }

    private Sprite GetNewCandy(int x, int y)
    {
        List<Sprite> posibleCandies = new List<Sprite>();
        posibleCandies.AddRange(candyPrefabs);
        if (x > 0)
        {
            posibleCandies.Remove(candies[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < xSize - 1)
        {
            posibleCandies.Remove(candies[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            posibleCandies.Remove(candies[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }
        return posibleCandies[Random.Range(0, posibleCandies.Count)];
    }

    private int GetId(Sprite candy)
    {
        int id = 0;
        for (int i = 0; i < candyPrefabs.Count; i++)
        {
            if (candy == candyPrefabs[i])
                id = i;
        }
        return id;
    }
}
