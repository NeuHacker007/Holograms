using UMA;
using UMA.PoseTools;
using UnityEngine;

public class UMAExpression : MonoBehaviour
{
    public UMAExpressionPlayer ExpressionPlayer;
    public bool ready = false;

    // Use this for initialization
    void Start()
    {
        UMADynamicAvatar avatar = GetComponent<UMADynamicAvatar>();
        if (avatar.umaData == null)
        {
            avatar.Initialize();
        }

        avatar.umaData.OnCharacterUpdated += AddExpressions;


    }

    void AddExpressions(UMAData umaData)
    {
        UMAExpressionSet expressionSet = umaData.umaRecipe.raceData.expressionSet;
        ExpressionPlayer = umaData.gameObject.AddComponent<UMAExpressionPlayer>();
        ExpressionPlayer.expressionSet = expressionSet;
        ExpressionPlayer.umaData = umaData;
        ExpressionPlayer.Initialize();

        ready = true;

    }


}
