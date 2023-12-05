using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShifitngPlatform : MonoBehaviour
{
    private Vector3 startpos;
    private Vector3 endpos;
    [SerializeField] float speed;
    [SerializeField] Transform platform;
    [SerializeField] Transform transformB;
    private Vector3 nextpos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        endpos = transformB.localPosition;
        startpos = platform.localPosition;
        nextpos = endpos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(platform.localPosition, nextpos) <= 0.1) {
            if (nextpos != startpos) {
                nextpos = startpos;
            } else {
                nextpos = endpos;
            }
        }
        platform.localPosition = Vector3.MoveTowards(platform.localPosition, nextpos, speed * Time.deltaTime);

    }


}
