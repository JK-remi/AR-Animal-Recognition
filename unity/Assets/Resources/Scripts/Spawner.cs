using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Spawner : MonoBehaviour
{
    Dictionary<string, int> prefabIndex;
    public List<GameObject> prefab;
    private GameObject spawnObj;
    public float tempRot = 225f;

    public ParticleSystem particlePrefab;
    private ParticleSystem particleObj;
    public float particleTime = 2f;

    public ARRaycastManager rayManager;

    private void Start()
    {
        if(rayManager == null)
            rayManager=FindObjectOfType<ARRaycastManager>();

        prefabIndex = new Dictionary<string, int>();
        prefabIndex.Add("zebra", 0);
        prefabIndex.Add("nizer", 1);
        prefabIndex.Add("kumi", 2);
        prefabIndex.Add("chungcho", 3);

        prefabIndex.Add("ostrich", 4);
        prefabIndex.Add("elephant", 5);
        prefabIndex.Add("bear", 6);

        prefabIndex.Add("tiger", 7);
        prefabIndex.Add("TaeHo", 8);
        prefabIndex.Add("GunGon", 9);
        prefabIndex.Add("TaeBum", 10);
        prefabIndex.Add("MooGung", 11);

        prefabIndex.Add("lion", 12);
        prefabIndex.Add("hippo", 13);
        prefabIndex.Add("panda", 14);
        prefabIndex.Add("FuBao", 15);
        prefabIndex.Add("LeBao", 16);
        prefabIndex.Add("AiBao", 17);
        prefabIndex.Add("rhinocero", 18);
        prefabIndex.Add("giraffe", 19);
    }

    private void Awake()
    {
        if(particlePrefab != null)
        {
            particleObj = Instantiate<ParticleSystem>(particlePrefab, this.transform);
            StopParticle();
        }
    }

    private void Update()
    {
        List<ARRaycastHit> hitpoint = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width * 0.5f, Screen.height * 0.15f), hitpoint, TrackableType.AllTypes);
        if(hitpoint.Count > 0)
        {
            transform.position = hitpoint[0].pose.position;
            transform.rotation = new Quaternion(0f, hitpoint[0].pose.rotation.y, 0f, hitpoint[0].pose.rotation.w);
            transform.Rotate(Vector3.up, tempRot);
        }
    }

    public void Spawn(string name)
    {
        spawnObj = Instantiate(prefab[prefabIndex[name]], transform.position, transform.rotation);
        PlayParticle();
        Invoke("StopParticle", particleTime);
    }

    private void PlayParticle()
    {
        if(particleObj != null)
        {
            particleObj.gameObject.SetActive(true);
            particleObj.Play(true);
        }
    }

    private void StopParticle()
    {
        if(particleObj != null)
        {
            particleObj.Stop();
            particleObj.gameObject.SetActive(false);
        }
    }

    public void Eliminate()
    {
        if (spawnObj != null) DestroyImmediate(spawnObj);
    }
}
