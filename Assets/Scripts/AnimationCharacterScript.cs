using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCharacterScript : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") !=0 || Input.GetAxis("Horizontal") !=0)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }
}
