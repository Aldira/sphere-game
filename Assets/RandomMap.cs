using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap : MonoBehaviour {


    int start = 1;
    int end = 49;
    int x = -60;
    int z = 60;
    List<GameObject> numbers = new List<GameObject>();

    // Use this for initialization
    void Start () {

        for (int i = start; i <= end; i++)
        {
            numbers.Add(GameObject.Find(i.ToString()));
        }

        while (numbers.Count > 0)
        {
            int index = Random.Range(0, numbers.Count);
            try
            {
                numbers[index].transform.position = new Vector3(x, 0, z);
            }
            catch
            {

            }
            x += 20;
            if (x > 60)
            {
                x = -60;
                z -= 20;
            }
            numbers.RemoveAt(index);
        }
        end = 7;
        for (int i = start; i <= end; i++)
        {
            numbers.Add(GameObject.Find("Item"+i.ToString()));
        }

        while (numbers.Count > 0)
        {
            int x = Random.Range(-70, 71);
            int z = Random.Range(-70, 71);
            int index = Random.Range(0, numbers.Count);

            numbers[index].transform.position = new Vector3(x, 50, z);

            numbers.RemoveAt(index);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
