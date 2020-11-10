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
    public GameObject ammoType;
    public int ammoID;
    public float bulletVelocity;
    

    // Specific
    public string rarity;

    // Sound
    public List<AudioClip> gunSounds = new List<AudioClip>();

    //Graphics
    public Sprite[] weaponSprites;
    public GameObject gunIcon;


}
