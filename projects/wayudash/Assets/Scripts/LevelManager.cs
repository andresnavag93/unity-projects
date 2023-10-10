using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLevel {
    forest, lava, snow
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance2;    
    public GameLevel currentGameLevel;
    //Forest Levels
    private int levelCount;
    public List<LevelBlock> listBlocks;
    public List<LevelBlock> allTheForestBlocks, allTheForestPortalBlocks = new List<LevelBlock>();
    public List<LevelBlock> allTheLavaBlocks, allTheLavaPortalBlocks = new List<LevelBlock>();
    public List<LevelBlock> allTheSnowBlocks, allTheSnowPortalBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPosition;

    // Start is called before the first frame update
    
    private void Awake() {
        if (sharedInstance2 == null){
            sharedInstance2 = this;
        }
        //gameLevel = (GameLevel)Random.Range(0, 3);
        //currentGameLevel = (GameLevel)Random.Range(0, 3);
        //levelCount = 0;
    }
    void Start()
    {
        GenerateCustomInitialBlocks();
    }

    public void AddCustomLevelBlock(){
        ListGameLevelBlocks();

        int randomIdx = Random.Range(0, listBlocks.Count);
        LevelBlock block;
        Vector3 spawnPosition = Vector3.zero;

        if (currentLevelBlocks.Count == 0){
            block = Instantiate(listBlocks[0]);
            spawnPosition = levelStartPosition.position;
        } else {
            block = Instantiate(listBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks
                [currentLevelBlocks.Count-1]
                .endPoint.position;
        }
        block.transform.SetParent(this.transform, false);
        Vector3 correction = new Vector3(
            spawnPosition.x-block.startPoint.position.x,
            spawnPosition.y-block.startPoint.position.y,
            0
        );
        block.transform.position = correction;
        currentLevelBlocks.Add(block);
    }

    public void GenerateCustomInitialBlocks(){
        //currentGameLevel = GameLevel.forest;
        currentGameLevel = (GameLevel)Random.Range(0, 3);
        levelCount = 0;
        for (int i = 0; i < 2; i++)
        {
            AddCustomLevelBlock();
        }
    }

    public void RemoveLevelBlock(){
       LevelBlock oldBlock = currentLevelBlocks[0];
       currentLevelBlocks.Remove(oldBlock);
       Destroy(oldBlock.gameObject);
    }

    public void RemoveAllBlocks(){
        while (currentLevelBlocks.Count > 0){
            RemoveLevelBlock();
        }
    }

    public void ListGameLevelBlocks(){
        if (currentGameLevel == GameLevel.forest){
            if (levelCount < 3){
                levelCount++;
                listBlocks = allTheForestBlocks;
            } else {
                levelCount = 0;
                currentGameLevel = GameLevel.lava;
                listBlocks = allTheForestPortalBlocks;
            }
        } else if (currentGameLevel == GameLevel.lava){
            if (levelCount < 3){
                levelCount++;
                listBlocks = allTheLavaBlocks;
            } else {
                levelCount = 0;
                currentGameLevel = GameLevel.snow;
                listBlocks = allTheLavaPortalBlocks;
            }
        } else if (currentGameLevel == GameLevel.snow){
            if (levelCount < 3){
                levelCount++;
                listBlocks = allTheSnowBlocks;
            } else {
                levelCount = 0;
                currentGameLevel = GameLevel.forest;
                listBlocks = allTheSnowPortalBlocks;
            }
        }
    }

}
