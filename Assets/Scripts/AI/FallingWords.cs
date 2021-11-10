using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWords : MonoBehaviour
{
    public string path= "Words";
    public List<string> sentences;
    public Transform player;

    public float atktime1 = 0.5f;
    public float atktime2 = 2;
    public float atktime3 = 30;


    int currentindex = 0;
    /// <summary>
    /// 将整个地图划分为大九宫格
    /// </summary>
    int[,] map = new int[9, 9];
    /// <summary>
    /// 以大格中心或玩家位置为中心的小九宫格
    /// </summary>
    List<int> box = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    /// <summary>
    /// 大字的坐标偏移
    /// </summary>
    float[,] posoffset = { { -5.0f, -5.0f }, { 0, -5.0f },{ 5.0f,-5.0f},
                                   { -5.0f,0},{ 0.0f,0.0f},{ 5.0f,0},
                                    { -5.0f,5.0f},{ 0,5.0f},{ 5.0f,5.0f} };
    /// <summary>
    /// 小字的坐标偏移
    /// </summary>
    float[,] posoffsetsmall={ { -0.5f, -0.5f }, { 0, -0.5f },{ 0.5f,-0.5f},
                                   { 0.0f,0.0f},{ 0.0f,0.0f},{ 0.0f,0.0f},
                                    { -0.5f,0.5f},{ 0,0.5f},{ 0.5f,0.5f} };


    private FallingWords instance;
    public FallingWords GetInstance() => instance;


    public List<T> RandomSortList<T>(List<T> ListT)
    {
        System.Random random = new System.Random();
        List<T> newList = new List<T>();
        foreach (T item in ListT)
        {
            newList.Insert(random.Next(newList.Count + 1), item);
        }
        return newList;
    }

    public void FallBigWords()
    {
        //9*9的方格 根据坐标判断所在格子
        int row = Mathf.Clamp((int)(player.position.x / 10 + Mathf.Sign(player.position.x) * 0.5f), -4, 4);
        int col = Mathf.Clamp((int)(player.position.z / 10 + Mathf.Sign(player.position.z) * 0.5f), -4, 4);

        //格子中心坐标
        float posx = row * 10;
        float posz = col * 10;

        //转换为map下标
        row += 4;
        col += 4;

        map[4, 4] = 1;//最中心不落

        //0 大小都能落 1不能落 2 只能落小字
        if (map[row, col] == 0)
        {
            for(int i=-1;i<=1;i++)
            {
                for(int j=-1;j<=1;j++)
                {
                    map[Mathf.Clamp(row + i,0,8), Mathf.Clamp(col + j,0,8)] = 2;
                }
            }
            map[row, col] = 1;


            box = RandomSortList<int>(box);
            for (int i = 0; i < sentences[currentindex].Length; i++)
            {
                PoolMgr.GetInstance().GetObj(path + "/" + sentences[currentindex][i], new Vector3(posx, 20, posz) + new Vector3(posoffset[box[i], 0], 0, posoffset[box[i], 1]), Quaternion.identity, (o) => {
                    
                });
            }
            currentindex++;
            if (currentindex >= sentences.Count) currentindex = 0;
        }
    }

    public void FallSmallWords()
    {
        //9*9的方格 根据坐标判断所在格子
        int row = Mathf.Clamp((int)(player.position.x / 10 + Mathf.Sign(player.position.x) * 0.5f), -4, 4);
        int col = Mathf.Clamp((int)(player.position.z / 10 + Mathf.Sign(player.position.z) * 0.5f), -4, 4);

        //格子中心坐标
        float posx = row * 10;
        float posz = col * 10;

        //转换为map下标
        row += 4;
        col += 4;

        if (map[row, col] != 1)
        {
            int index = UnityEngine.Random.Range(0, sentences[currentindex].Length - 1);
            box = RandomSortList<int>(box);
            PoolMgr.GetInstance().GetObj(path + "/" + sentences[currentindex][index] + " 1", new Vector3(player.transform.position.x, 20, player.transform.position.z) + new Vector3(posoffsetsmall[box[0], 0], 0, posoffsetsmall[box[0], 1]), Quaternion.identity, (o) =>
            {

            });
        }
    }

    public void FullScreenAtk()
    {
        GlobalFxManager.GetInstance().PlayfxOnceAtPoint(new Vector3(0,0.2f,0), Quaternion.LookRotation(Vector3.up), VFXName.magicfield);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //StartCoroutine("fall");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>0&&Time.time % atktime1 <= 0.01f)
        {
            FallSmallWords();
        }
        if (Time.time > 0 && Time.time % atktime2 <= 0.01f)
        {
            FullScreenAtk();
        }
        if (Time.time > 0 && Time.time % atktime3 <= 0.01f)
        {
            FallBigWords();
        }
    }

    

    IEnumerator fall()
    {
        while(true)
        {
            //FallBigWords();
            //FallSmallWords();
            //FullScreenAtk();
            yield return new WaitForSeconds(5);
        }
    }
}
