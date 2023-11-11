using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    #region inspector var
    [SerializeField] [Tooltip("Speed of the boomerang, like how fast the thing flys.")]
    private float speed = 5;
    [SerializeField] [Tooltip("how far it flies befor it comes back.")]
    private float maxDistance = 35;
    [SerializeField] [Tooltip("How much damage the boomerang does when it hits enemies.")]
    private float damage = 1f;
    [SerializeField] [Tooltip("Damage multiplier for when the boomerang is on the way back to the player.")]
    private float returnDmgMult = 2.0f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        
    }
}
