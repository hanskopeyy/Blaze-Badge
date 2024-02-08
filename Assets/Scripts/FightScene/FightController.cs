using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightController : MonoBehaviour
{
    [SerializeField]
    private SceneInformation sceneInfo;

    private bool inFight = false;
    private Character ally, enemy;
    private GameObject allyObject, enemyObject;
    private Vector2 allyDefPos, enemyDefPos;
    private FightInfo allyInfo, enemyInfo;
    private bool isAllyTurn = true, isDoneAttack = false, calculate = false;
    private float step;

    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator Fighting(){
        while(inFight){
            if(isAllyTurn)
            {
                if(!isDoneAttack)
                {
                    allyObject.transform.position = Vector2.MoveTowards(allyObject.transform.position, enemyObject.transform.position, step);
                    if(Vector2.Distance(allyObject.transform.position, enemyObject.transform.position) < 1f && !calculate)
                    {
                        enemy.charaHP -= Fight(ally,enemy);
                        if(enemy.charaHP < 0)
                        {
                            enemy.charaHP = 0;
                        }
                        calculate = enemyInfo.calculateInfo();
                        yield return new WaitForSeconds(Time.deltaTime);
                    } else if(calculate) {
                        calculate = enemyInfo.calculateInfo();
                        if(!calculate){
                            isDoneAttack = true;
                            yield return new WaitForSeconds(0.5f);
                        } else {
                            yield return new WaitForSeconds(Time.deltaTime);
                        }
                    } else {
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                } else {
                    allyObject.transform.position = Vector2.MoveTowards(allyObject.transform.position, allyDefPos, step);
                    if(Vector2.Distance(allyObject.transform.position, allyDefPos) < 0.01f)
                    {
                        isDoneAttack = false;
                        isAllyTurn = false;
                        yield return new WaitForSeconds(1);
                    } else {
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
            } else if(enemy.charaHP > 0){
                if(!isDoneAttack)
                {
                    enemyObject.transform.position = Vector2.MoveTowards(enemyObject.transform.position, allyObject.transform.position, step);
                    if(Vector2.Distance(enemyObject.transform.position, allyObject.transform.position) < 1f && !calculate)
                    {
                        ally.charaHP -= Fight(enemy,ally);
                        if(ally.charaHP < 0)
                        {
                            ally.charaHP = 0;
                        }
                        calculate = allyInfo.calculateInfo();
                        yield return new WaitForSeconds(Time.deltaTime);
                    } else if(calculate) {
                        calculate = allyInfo.calculateInfo();
                        if(!calculate){
                            isDoneAttack = true;
                            yield return new WaitForSeconds(0.5f);
                        } else {
                            yield return new WaitForSeconds(Time.deltaTime);
                        }
                        yield return new WaitForSeconds(Time.deltaTime);
                    } else {
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                } else {
                    enemyObject.transform.position = Vector2.MoveTowards(enemyObject.transform.position, enemyDefPos, step);
                    if(Vector2.Distance(enemyObject.transform.position, enemyDefPos) < 0.01f)
                    {
                        isDoneAttack = false;
                        isAllyTurn = true;
                        inFight = false;
                        sceneInfo.isFirstLoad = false;
                        yield return new WaitForSeconds(2);
                        SceneManager.LoadScene("Game Screen");
                    } else {
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
            } else {
                inFight = false;
                sceneInfo.isFirstLoad = false;
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene("Game Screen");
            }
        }
    }

    public void doFight(GameObject aObject, FightInfo aInfo, GameObject eObject, FightInfo eInfo)
    {
        allyObject = aObject;
        enemyObject = eObject;
        allyDefPos = aObject.transform.position;
        enemyDefPos = eObject.transform.position;
        allyInfo = aInfo;
        enemyInfo = eInfo;
        var encounter = PlayerInventory.encounter.First();
        this.ally = encounter.ally;
        this.enemy = encounter.enemy;
        PlayerInventory.encounter.Remove(encounter);
        step = 10 * Time.deltaTime;
        Debug.Log("Fighting Start");
        inFight = true;
        StartCoroutine(Fighting());        
    }

    public int Fight(Character a, Character b)
    {
        int bReduceDMG = a.charaType == 1 ? b.charaDEF : b.charaRES;
        int aDMG = a.charaATK - bReduceDMG;

        return aDMG;
    }

}
