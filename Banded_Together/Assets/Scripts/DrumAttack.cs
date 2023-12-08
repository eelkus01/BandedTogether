using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumAttack : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // objectTransform = transform;
        // StartCoroutine(AnimateScale());
        anim = GetComponent<Animator>();

        StartCoroutine(ScheduleDestroy());
        anim.SetTrigger("StartAttack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ScheduleDestroy()
    {
        yield return new WaitForSeconds(1f);

        // Destroy the object
        Destroy(gameObject);
    }
}
