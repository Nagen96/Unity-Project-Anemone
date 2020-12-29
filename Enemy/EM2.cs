using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM2 : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> el_bulletless = new List<GameObject>(); //el = EnemyList
    public IEnumerator active_enemy_routine;

    private void Start()
    {
        active_enemy_routine = ActiveEnemy();
        StartCoroutine(active_enemy_routine);
    }

    IEnumerator ActiveEnemy()
    {
        yield return new WaitForSeconds(2f);
        enemyList[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyList[1].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[5].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[8].SetActive(true);

        yield return new WaitForSeconds(3f);
        enemyList[4].SetActive(true);
        enemyList[2].SetActive(true);

        yield return new WaitForSeconds(3f);
        enemyList[20].SetActive(true);

        yield return new WaitForSeconds(2f);
        enemyList[12].SetActive(true);
        //el_bulletless[0].SetActive(true);

        
        enemyList[3].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[9].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[10].SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyList[25].SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyList[7].SetActive(true);

        yield return new WaitForSeconds(2f);
        enemyList[13].SetActive(true);
        
        enemyList[14].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[21].SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyList[16].SetActive(true);

        yield return new WaitForSeconds(3f);
        enemyList[17].SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyList[23].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[19].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[6].SetActive(true);

        yield return new WaitForSeconds(3f);
        enemyList[15].SetActive(true);
        yield return new WaitForSeconds(4f);
        enemyList[22].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[18].SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyList[24].SetActive(true);

        yield return new WaitForSeconds(3f);
        enemyList[11].SetActive(true);
        yield return new WaitForSeconds(3f);
        enemyList[26].SetActive(true);
        yield return new WaitForSeconds(2f);
        enemyList[27].SetActive(true);


        enemyList[28].SetActive(true);
        yield return new WaitForSeconds(6f);
        enemyList[29].SetActive(true);
        
        enemyList[30].SetActive(true);
        
        enemyList[31].SetActive(true);

        yield return new WaitForSeconds(10f);
        enemyList[32].SetActive(true);

    }
}
