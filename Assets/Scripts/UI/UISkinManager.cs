using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISkinManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public MeshRenderer playerMeshRenderer;
    public MeshFilter playerMeshFilter;

    private List<MeshFilter> materials = new List<MeshFilter>();
    private List<string> namesOfSkins = new List<string> { "Apple Green", "Apple Red", "Blueberry Blue", "Blueberry Cyan", "Eggplant", "Grape", "Lemon", "Lime", "Orange", "Peach", "Pear", "Pineapple", "Plum Blue", "Plum Purple", "Pomegranate", "Strawberry" };
    private int skinCount = 0;
    private GameObject[] btnsSkin;
    private TextMeshProUGUI skinName;
  
    void Start()
    {
        playerMeshRenderer = player.GetComponent<MeshRenderer>();
        playerMeshFilter = player.GetComponent<MeshFilter>();
        playerMeshRenderer.material = (Material)Resources.Load("fc_unlit", typeof(Material));
        playerMeshFilter.mesh = (Mesh)Resources.Load($"Meshes/Mesh0", typeof(Mesh));

        GetAllMesh();

        btnsSkin = GameObject.FindGameObjectsWithTag("btnsSkin");
        skinName = GameObject.FindWithTag("txtSkin").GetComponent<TextMeshProUGUI>();
        skinName.text = namesOfSkins[0];
    }
    private void Update()
    {
        if (skinCount <= 15)
        {
            btnsSkin[0].SetActive(true);
        }
        else
        {
            btnsSkin[0].SetActive(false);
        }

        if (skinCount >= 0)
        {
            btnsSkin[1].SetActive(true);
        }
        else
        {
            btnsSkin[1].SetActive(false);
        }
    }
    public void NextSkin()
    {
        if (skinCount < 15)
        {
            skinCount++;
            playerMeshFilter.mesh = (Mesh)Resources.Load($"Meshes/Mesh{skinCount}", typeof(Mesh));
            PlayerPrefs.SetString("MeshFilter",$"Meshes/Mesh{skinCount}");
            skinName.text = namesOfSkins[skinCount];
        }
    }
    public void BackSkin()
    {
        if (skinCount > 0)
        {
            skinCount--;
            playerMeshFilter.mesh = (Mesh)Resources.Load($"Meshes/Mesh{skinCount}", typeof(Mesh));
            PlayerPrefs.SetString("MeshFilter", $"Meshes/Mesh{skinCount}");
            skinName.text = namesOfSkins[skinCount];
        }

    }
    private void GetAllMesh()
    {
        for (int i = 0; i < 15; i++)
        {
            materials.Add((MeshFilter)Resources.Load($"Meshes/Mesh{i}", typeof(MeshFilter)));
        }
    }
    public void Back()
    {
        Time.timeScale = 1;
        if(MainMenuMethods.instance.previousSceneName == "SampleScene")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (MainMenuMethods.instance.previousSceneName == "Level2")
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
