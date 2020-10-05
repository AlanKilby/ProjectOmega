using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public new string name;
    public string ID;

    // General Information
    public float damage;
    public float accuracy;
    public float fireRate;
    public int ammoCount;
    public GameObject ammoType;
    public List<GameObject> shootingPoints = new List<GameObject>();


    // Specific
    public float dropRate;
    public string rarity;

    // Sound
    public List<AudioSource> gunSounds = new List<AudioSource>();

}
