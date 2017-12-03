using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour 
{
  float speed = 0.01f;
  int leaks = 3;
  int pumps = 2;

  float leakTimer;

  public Transform[] partSpawns;
  public GameObject partPrefab;
  public GameObject[] leakGOs;
  public List<int> activeLeaks;

	void Start () 
	{
    
    int leaksAssigned = 0;
    int index;

    while (leaksAssigned < 3)
    {
      index = Random.Range(0, leakGOs.Length);
      if (!activeLeaks.Contains(index))
      {
        leakGOs[index].SetActive(true);
        activeLeaks.Add(index);
        leaksAssigned++;
      }
    }

    leakTimer = Random.Range(20, 30);

    int partsAssigned = 0;
    List<int> assignedIndeces = new List<int>();

    while (partsAssigned < 5)
    {
      index = Random.Range(0, partSpawns.Length);
      if (!assignedIndeces.Contains(index))
      {
        Instantiate(partPrefab, partSpawns[index].position, Quaternion.Euler(-90, 0, 0));
        assignedIndeces.Add(index);
        partsAssigned++;
      }
    }
    
	}
	
	void Update () 
	{
    transform.Translate(new Vector3(0, speed * Time.deltaTime * (leaks - pumps), 0));
    if (transform.position.y <= -10.1f)
    {
      transform.position = new Vector3(0, -10.1f, 0);
    }

    if (transform.position.y >= 6.25f)
    {
      Scenes.GameOver("Game Over. The sub has flooded");
    }

    leakTimer -= Time.deltaTime;

    if (leakTimer <= 0)
    {
      AddLeak();
    }
	}

  void AddLeak()
  {
    bool assigned = false;

    while (!assigned)
    {
      int index = Random.Range(0, leakGOs.Length);
      if (!activeLeaks.Contains(index))
      {
        leakGOs[index].SetActive(true);
        activeLeaks.Add(index);
        leaks++;
        assigned = true;
      }
    }

    leakTimer = Random.Range(20, 30);
  }

  public void RemoveLeak(int i)
  {
    leakGOs[i].SetActive(false);
    activeLeaks.Remove(i);
    leaks--;
  }
}
