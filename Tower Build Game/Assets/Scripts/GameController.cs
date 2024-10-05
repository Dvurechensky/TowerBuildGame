using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    private Color toCameraColor;
    private Transform mainCam;
    private float camMoveToYPosition, camMoveSpeed = 2f;
    private int prevCountMaxHorizontal = 0;
    private Coroutine showCubePlace;
    private bool isLoos, firstCube;
    private Rigidbody allCubesRb;
    private CubePos nowCube = new CubePos(0, 1, 0);
    private List<GameObject> posibleCubesToCreate = new List<GameObject>();
    private List<Vector3> allCubesPostions = new List<Vector3>()
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(-1, 0, 1),
        new Vector3(1, 0, -1),
    };

    public GameObject[] cubesToCreate;
    public Text scoreTxt;
    public Color[] bgColors;
    public GameObject[] canvasStartPage;
    public Transform cubeToPlace;
    public float cubeChangePlaceSpeed = 0.5f;
    public GameObject allCubes, vfx;

    private void Start()
    {
        if (PlayerPrefs.GetInt("score") < 5)
            AddPossibleCubes(1);
        else if(PlayerPrefs.GetInt("score") < 10)
            AddPossibleCubes(2);
        else if (PlayerPrefs.GetInt("score") < 15)
            AddPossibleCubes(3);
        else if (PlayerPrefs.GetInt("score") < 20)
            AddPossibleCubes(4);
        else if (PlayerPrefs.GetInt("score") < 25)
            AddPossibleCubes(5);
        else if (PlayerPrefs.GetInt("score") < 30)
            AddPossibleCubes(6);
        else if (PlayerPrefs.GetInt("score") < 35)
            AddPossibleCubes(7);
        else if (PlayerPrefs.GetInt("score") < 40)
            AddPossibleCubes(8);
        else if (PlayerPrefs.GetInt("score") < 45)
            AddPossibleCubes(9);
        else
            AddPossibleCubes(10);


        scoreTxt.text = "<size=50><color=#E13885>BEST:</color><color=#65151>" + PlayerPrefs.GetInt("score") + "</color></size><size=50>\nnow:<color=#66151>" + 0 + "</color></size>";
        toCameraColor = Camera.main.backgroundColor;
        mainCam = Camera.main.transform;
        camMoveToYPosition = 5.9f + nowCube.y - 1f;

        showCubePlace = StartCoroutine(ShowCubePlace());
        allCubesRb = allCubes.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }
        }

        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && cubeToPlace != null && allCubes != null && !EventSystem.current.IsPointerOverGameObject())
        {
#if !UNITY_EDITOR
            if(Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            if(!firstCube)
            {
                firstCube = true;
                foreach(var obj in canvasStartPage)
                    Destroy(obj);
            }

            GameObject createCube = null;
            if (posibleCubesToCreate.Count == 1)
                createCube = posibleCubesToCreate[0];
            else
                createCube = posibleCubesToCreate[UnityEngine.Random.Range(0, posibleCubesToCreate.Count)];

            GameObject newCube = Instantiate(createCube,
                        cubeToPlace.position,
                        Quaternion.identity);

            newCube.transform.SetParent(allCubes.transform);
            nowCube.setVector(cubeToPlace.position);
            allCubesPostions.Add(nowCube.getVector());

            if (PlayerPrefs.GetString("music") != "No")
                GetComponent<AudioSource>().Play();

            GameObject newVfx = Instantiate(vfx, cubeToPlace.position, Quaternion.identity);
            Destroy(newVfx, 2f);

            allCubesRb.isKinematic = true;
            allCubesRb.isKinematic = false;

            SpawnPositions();
            MoveCameraChangeBg();
        }

        if(!isLoos && allCubesRb.velocity.magnitude > 0.1f)
        {
            Destroy(cubeToPlace.gameObject);
            isLoos = true;
            StopCoroutine(showCubePlace);
        }
        
        mainCam.localPosition = Vector3.MoveTowards(mainCam.localPosition,
                new Vector3(mainCam.localPosition.x, camMoveToYPosition, mainCam.localPosition.z),
                camMoveSpeed * Time.deltaTime);

        if (Camera.main.backgroundColor != toCameraColor)
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, toCameraColor, Time.deltaTime);
    }

    private IEnumerator ShowCubePlace()
    {
        while(true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(cubeChangePlaceSpeed);
        }
    }

    private void SpawnPositions()
    {
        var positions = new List<Vector3>();
        if(IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z))
            && nowCube.x + 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
        if(IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z))
            && nowCube.x - 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z))
            && nowCube.y + 1 != cubeToPlace.position.y)
            positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z))
            && nowCube.y - 1 != cubeToPlace.position.y)
            positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1))
            && nowCube.z + 1 != cubeToPlace.position.z)
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
        if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1))
            && nowCube.z - 1 != cubeToPlace.position.z)
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));

        if (positions.Count > 1)
            cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
        else if (positions.Count == 0)
            isLoos = true;
        else
            cubeToPlace.position = positions[0];
    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if(targetPos.y == 0)
            return false;

        foreach(Vector3 pos in allCubesPostions)
        {
            if(pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
                return false;
        }

        return true;
    }

    private void MoveCameraChangeBg()
    {
        int maxX = Mathf.Abs(Convert.ToInt32(allCubesPostions.Max(element => element.x)));
        int maxY = Convert.ToInt32(allCubesPostions.Max(element => element.y));
        int maxZ = Mathf.Abs(Convert.ToInt32(allCubesPostions.Max(element => element.z)));
        int maxHorizontal;

        maxY--;

        if (PlayerPrefs.GetInt("score") < maxY)
            PlayerPrefs.SetInt("score", maxY);

        scoreTxt.text = "<size=50><color=#E13885>BEST:</color><color=#65151>" + PlayerPrefs.GetInt("score") + "</color></size><size=50>\nnow:<color=#66151>" + maxY + "</color></size>";

        camMoveToYPosition = 5.9f + nowCube.y - 1f;
        maxHorizontal = maxX > maxZ ? maxX : maxZ;

        if (maxHorizontal % 3 == 0 && prevCountMaxHorizontal != maxHorizontal)
        {
            mainCam.localPosition -= new Vector3(0, 0, 2.5f);
            prevCountMaxHorizontal = maxHorizontal;
        }

        if (maxY >= 7)
            toCameraColor = bgColors[2];
        else if(maxY >= 5)
            toCameraColor = bgColors[1];
        else if (maxY >= 2)
            toCameraColor = bgColors[0];
    }

    private void AddPossibleCubes(int till)
    {
        for(int i = 0; i < till; i++)
            posibleCubesToCreate.Add(cubesToCreate[i]);
    }
}

struct CubePos
{
    public int x, y, z;

    public CubePos(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 getVector()
    {
        return new Vector3(x, y, z);
    }

    public void setVector(Vector3 pos)
    {
        x = Convert.ToInt32(pos.x);
        y = Convert.ToInt32(pos.y);
        z = Convert.ToInt32(pos.z);
    }
}
