using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListController : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private GameObject prefab;

    private List<Character> availableCharacter;
    public List<Character> enemyLineup = new List<Character>();
    private List<GameObject> enemyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        availableCharacter = new List<Character>(PlayerInventory.availChara);
        randomizeEnemy();
    }
    public void randomizeEnemy(){
        enemyLineup = getRandomEnemy();
    }

    private void updateList(List<Character> enemy){
        if(enemyList.Count > 0){
            foreach(GameObject go in enemyList){
                Destroy(go);
            }
        }
        enemyList.Clear();
        foreach(Character e in enemy)
        {
            GameObject newEnemy = Instantiate(prefab, scrollViewContent);
            newEnemy.GetComponent<SelectedCharaUI>().setCharacter(e);
            enemyList.Add(newEnemy);
        }
    }

    public List<Character> getRandomEnemy(){
        List<Character> tempList = new List<Character>();
        for(int i = 0; i<4; i++){
            int index = Random.Range(0,(availableCharacter.Count-1));
            tempList.Add(availableCharacter[index]);
            availableCharacter.RemoveAt(index);
        }
        updateList(tempList);
        foreach(Character chara in tempList){
            availableCharacter.Add(chara);
        }
        return tempList;
    }
}
