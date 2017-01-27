using UMA.PoseTools;
using UnityEngine;

public class GuopinExpressionPlayer : ExpressionPlayer
{

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Player");
        //Animator animator = GetComponent<Animator>();
        //Debug.Log(animator);
        //Transform jawbone = animator.GetBoneTransform(HumanBodyBones.Jaw);
        //Debug.Log(jawbone);
        Debug.Log("Entrance");
        Animation animation = GetComponent<Animation>();
        Animator animator = animation.GetComponent<Animator>();
        Avatar avatar = animation.GetComponent<Avatar>();

        Transform jawboneTransform = animator.GetBoneTransform(HumanBodyBones.Jaw);
        Debug.Log(jawboneTransform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
