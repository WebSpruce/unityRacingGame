using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class HistoryMethods : MonoBehaviour
{
    [SerializeField] private GameObject historyObject;
    [SerializeField] private GameObject ListViewItems;
    [SerializeField] private GameObject ListViewItemPrefab;
    List<ResultValues> results = new List<ResultValues>();
    private void OnEnable()
    {
        if (historyObject.activeSelf)
        {
            results = FileHandler.ReadFromJSON<ResultValues>(PlayerMovement.instance.filename);
            Vector3[] newPosition = new Vector3[results.Count];
            newPosition[0] = new Vector3(ListViewItemPrefab.transform.position.x - 493, ListViewItemPrefab.transform.position.y - 90, ListViewItemPrefab.transform.position.y);
            for (int i = 0; i < results.Count; i++)
            {
                if (i != 0)
                {
                    newPosition[i] = new Vector3(newPosition[i - 1].x, newPosition[i - 1].y - 45, newPosition[i - 1].y);
                }

                GameObject newBtn = Instantiate(ListViewItemPrefab, newPosition[i], transform.rotation) as GameObject;
                newBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = results[i].result;
                newBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = results[i].date;
                newBtn.transform.SetParent(ListViewItems.transform, false);
            }
        }
    }
    public void Back()
    {
        historyObject.SetActive(false);
    }
}
