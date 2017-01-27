using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MadWonder
{

    public class CheshireCreate : EditorWindow
    {

        private Cheshire _targetCat;
        private TextAsset _targetData;
        private AudioClip _targetAudio;

        private AnimationCurve[] processCurves;

        private float modelAnimStrength;
        private float modelAnimLeadTime;
        private int modelAnimType;
        private string[] modelAnimTypeLabels;

        public void CheshireCreateInit(Cheshire freshCat)
        {
            _targetCat = freshCat;
            modelAnimStrength = 80.0f;
            modelAnimType = 0;
            modelAnimTypeLabels = new string[2];
            modelAnimTypeLabels[0] = "Mecanim Generic";
            modelAnimTypeLabels[1] = "Legacy";
            this.minSize = new Vector2(320.0f, 104.0f);
            this.maxSize = new Vector2(480.0f, 104.0f);
        }

        void OnGUI()
        {
            _targetData = (TextAsset)EditorGUILayout.ObjectField("Timing Data:", _targetData, typeof(TextAsset), true);
            _targetAudio = (AudioClip)EditorGUILayout.ObjectField("Audio Clip:", _targetAudio, typeof(AudioClip), true);

            if (_targetCat.syncType == 2)
            {
                modelAnimStrength = EditorGUILayout.Slider("Anim Strength:", modelAnimStrength, 5.0f, 100.0f);
            }

            modelAnimType = EditorGUILayout.Popup("Animation Type:", modelAnimType, modelAnimTypeLabels);

            if (_targetData == null)
            {
                GUI.enabled = false;
            }

            if (GUILayout.Button("Create Animation"))
            {
                string animObjectPath = string.Empty;
                if (_targetCat.targetChild != null)
                {
                    animObjectPath = _targetCat.GetTargetPath();
                }

                string animSavePath = EditorUtility.SaveFilePanelInProject("Save Animation", _targetData.name, "anim", "Choose a name and location for your animation");
                if (animSavePath.Length != 0)
                {
                    string[] freshPhonemes = ExtractPhonemes(_targetData);
                    float[] freshTimings = ExtractTimings(_targetData);
                    if (_targetCat.syncType == 1)
                    {
                        CreateSpriteAnim(animObjectPath, animSavePath, freshPhonemes, freshTimings);
                    }
                    else if (_targetCat.syncType == 2)
                    {
                        CreateModelAnim(animObjectPath, animSavePath, freshPhonemes, freshTimings);
                    }
                }
            }
            GUI.enabled = true;
        }

        private void CreateModelAnim(string freshObjectPath, string freshSavePath, string[] freshSymbols, float[] freshTimings)
        {
            AnimationClip processClip = new AnimationClip();
            processClip.name = _targetData.name;
            processClip.wrapMode = WrapMode.Once;
            if (modelAnimType == 0)
            {
#if UNITY_5_5_OR_NEWER
                //nothing
#else
                AnimationUtility.SetAnimationType(processClip, ModelImporterAnimationType.Generic);
#endif
            }
            else if (modelAnimType == 1)
            {

#if UNITY_5_5_OR_NEWER
                //nothing
#else
                AnimationUtility.SetAnimationType(processClip, ModelImporterAnimationType.Legacy);
#endif
            }

            AnimationEvent[] modelEvents = new AnimationEvent[1];
            if (_targetAudio != null)
            {
                modelEvents[0] = new AnimationEvent();
                modelEvents[0].functionName = "PlaySyncAudio";
                modelEvents[0].objectReferenceParameter = _targetAudio;
                modelEvents[0].time = 0.0f;
                AnimationUtility.SetAnimationEvents(processClip, modelEvents);
            }
            //For whatever reason, it seems to be better to assign Animation Events after the Curve assignment, not sure why

            processCurves = new AnimationCurve[_targetCat.GetBlendList().Count];
            for (int k = 0; k < processCurves.Length; k++)
            {
                processCurves[k] = new AnimationCurve(); //AnimationCurve.EaseInOut(0.0f, 0.0f, freshTimings[freshTimings.Length - 1], 0.0f);
            }

            string previousSymbol = string.Empty;
            float leadTime = 0.1f;
            //float speechVolume = 100.0f;

            for (int i = 0; i < freshSymbols.Length; i++)
            {
                if (previousSymbol == string.Empty)
                {
                    //This is where we handle the very first key assignment
                    if (freshSymbols[i] == "rest")
                    {
                        continue;
                    }
                    else
                    {
                        if (freshTimings[i] == 0.0f)
                        {
                            Keyframe freshFrame = new Keyframe(0.0f, modelAnimStrength, 0.0f, 0.0f);
                            freshFrame.tangentMode = 0;
                            processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i])].AddKey(freshFrame);
                            //processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i])].AddKey(
                        }
                        if (freshTimings[i] - leadTime <= 0.0f)
                        {
                            Keyframe freshFrame = new Keyframe(0.0f, modelAnimStrength, 0.0f, 0.0f);
                            freshFrame.tangentMode = 0;
                            processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i])].AddKey(freshFrame);
                        }
                        else
                        {
                            Keyframe freshFrame = new Keyframe(freshTimings[i] - leadTime, modelAnimStrength, 0.0f, 0.0f);
                            freshFrame.tangentMode = 0;
                            processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i])].AddKey(freshFrame);
                        }
                    }
                }
                else
                {
                    if (freshSymbols[i] == "rest")
                    {
                        if (freshTimings[i] - leadTime > freshTimings[i - 1])
                        {
                            Keyframe freshFrame = new Keyframe(freshTimings[i] - leadTime, modelAnimStrength, 0.0f, 0.0f);
                            freshFrame.tangentMode = 0;
                            processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i - 1])].AddKey(freshFrame);
                        }
                        Keyframe anotherFrame = new Keyframe(freshTimings[i], 0.0f, 0.0f, 0.0f);
                        anotherFrame.tangentMode = 0;
                        processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i - 1])].AddKey(anotherFrame);
                    }
                    else
                    {
                        if (previousSymbol != "rest")
                        {
                            if (freshTimings[i] - leadTime > freshTimings[i - 1])
                            {
                                Keyframe freshFrame = new Keyframe(freshTimings[i] - leadTime, modelAnimStrength, 0.0f, 0.0f);
                                freshFrame.tangentMode = 0;
                                processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i - 1])].AddKey(freshFrame);
                            }
                            Keyframe anotherFrame = new Keyframe(freshTimings[i], 0.0f, 0.0f, 0.0f);
                            anotherFrame.tangentMode = 0;
                            processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i - 1])].AddKey(anotherFrame);
                        }

                        if (freshTimings[i] - leadTime > freshTimings[i - 1])
                        {
                            Keyframe freshFrame = new Keyframe(freshTimings[i] - leadTime, 0.0f, 0.0f, 0.0f);
                            freshFrame.tangentMode = 0;
                            processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i])].AddKey(freshFrame);
                        }
                        else
                        {
                            Keyframe freshFrame = new Keyframe(freshTimings[i - 1], 0.0f, 0.0f, 0.0f);
                            freshFrame.tangentMode = 0;
                            processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i])].AddKey(freshFrame);
                        }
                        Keyframe yetAnotherFrame = new Keyframe(freshTimings[i], modelAnimStrength, 0.0f, 0.0f);
                        yetAnotherFrame.tangentMode = 0;
                        processCurves[_targetCat.GetAdjustedBlend(freshSymbols[i])].AddKey(yetAnotherFrame);
                    }
                }

                previousSymbol = freshSymbols[i];
            }

            for (int j = 0; j < processCurves.Length; j++)
            {
                if (_targetCat.targetChild == null)
                {
                    AnimationUtility.SetEditorCurve(processClip, EditorCurveBinding.FloatCurve(freshObjectPath, typeof(SkinnedMeshRenderer), "blendShape." + _targetCat.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeName(_targetCat.GetBlendList()[j])), processCurves[j]);
                }
                else
                {
                    AnimationUtility.SetEditorCurve(processClip, EditorCurveBinding.FloatCurve(freshObjectPath, typeof(SkinnedMeshRenderer), "blendShape." + _targetCat.targetChild.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeName(_targetCat.GetBlendList()[j])), processCurves[j]);
                }
            }

            AssetDatabase.CreateAsset(processClip, freshSavePath);
            AssetDatabase.SaveAssets();
            this.Close();
        }

        private void CreateSpriteAnim(string freshObjectPath, string freshSavePath, string[] freshSymbols, float[] freshTimings)
        {
            AnimationClip processClip = new AnimationClip();
            processClip.name = _targetData.name;
            if (modelAnimType == 0)
            {
#if UNITY_5_5_OR_NEWER
                //donothing
#else
                AnimationUtility.SetAnimationType(processClip, ModelImporterAnimationType.Generic);
#endif
            }
            else if (modelAnimType == 1)
            {
#if UNITY_5_5_OR_NEWER
#else
                AnimationUtility.SetAnimationType(processClip, ModelImporterAnimationType.Legacy);
#endif
            }

            EditorCurveBinding spriteBinding = new EditorCurveBinding();
            spriteBinding.type = typeof(SpriteRenderer);
            spriteBinding.path = freshObjectPath;
            spriteBinding.propertyName = "m_Sprite";

            AnimationEvent[] spriteEvents = new AnimationEvent[1];

            if (_targetAudio != null)
            {
                spriteEvents[0] = new AnimationEvent();
                spriteEvents[0].functionName = "PlaySyncAudio";
                spriteEvents[0].objectReferenceParameter = _targetAudio;
                spriteEvents[0].time = 0.0f;

                AnimationUtility.SetAnimationEvents(processClip, spriteEvents);
            }

            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[freshSymbols.Length];
            for (int i = 0; i < freshSymbols.Length; i++)
            {
                spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                spriteKeyFrames[i].time = freshTimings[i];
                spriteKeyFrames[i].value = _targetCat.GetSprite(freshSymbols[i]);
            }

            AnimationUtility.SetObjectReferenceCurve(processClip, spriteBinding, spriteKeyFrames);

            AssetDatabase.CreateAsset(processClip, freshSavePath);
            AssetDatabase.SaveAssets();

            this.Close();
        }

        private string[] ExtractPhonemes(TextAsset freshData)
        {
            string[] retrievePhonemes = freshData.text.Split('\n');
            List<string> pStrings = new List<string>();
            for (int i = 1; i < retrievePhonemes.Length; i++)
            {
                string[] processLine = retrievePhonemes[i].Split(',');
                if (processLine[0] == "P")
                {
                    pStrings.Add(retrievePhonemes[i]);
                }
            }
            string[] finalPhonemes = new string[pStrings.Count];

            for (int j = 1; j < pStrings.Count; j++)
            {
                string[] processLine = pStrings[j].Split(',');
                finalPhonemes[j] = processLine[1].Trim();
            }

            return finalPhonemes;
        }
        private float[] ExtractTimings(TextAsset freshData)
        {
            string[] retrievePhonemes = freshData.text.Split('\n');
            List<string> pFloats = new List<string>();
            for (int i = 1; i < retrievePhonemes.Length; i++)
            {
                string[] processLine = retrievePhonemes[i].Split(',');
                if (processLine[0] == "P")
                {
                    pFloats.Add(retrievePhonemes[i]);
                }
            }
            float[] finalTimings = new float[pFloats.Count];

            for (int j = 1; j < pFloats.Count; j++)
            {
                string[] processLine = pFloats[j].Split(',');
                finalTimings[j] = float.Parse(processLine[2].Trim());
            }

            return finalTimings;
        }
    }

}
