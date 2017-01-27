using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MadWonder {

	[CustomEditor(typeof(Cheshire))]
	public class CheshireInspector : Editor {

		private SerializedObject syncTarget;

		private SerializedProperty childIndex;
		private SerializedProperty syncTypeLink;

		private SerializedProperty childOptionList;
		private SerializedProperty modelShapeList;
		
		private SerializedProperty spriteAITarget;
		private SerializedProperty spriteETarget;
		private SerializedProperty spriteEtcTarget;
		private SerializedProperty spriteFVTarget;
		private SerializedProperty spriteLTarget;
		private SerializedProperty spriteMBPTarget;
		private SerializedProperty spriteOTarget;
		private SerializedProperty spriteUTarget;
		private SerializedProperty spriteWQTarget;
		private SerializedProperty spriteRestTarget;
		
		private SerializedProperty modelAITarget;
		private SerializedProperty modelETarget;
		private SerializedProperty modelEtcTarget;
		private SerializedProperty modelFVTarget;
		private SerializedProperty modelLTarget;
		private SerializedProperty modelMBPTarget;
		private SerializedProperty modelOTarget;
		private SerializedProperty modelUTarget;
		private SerializedProperty modelWQTarget;
		
		private SerializedProperty mouthToggle;
		private SerializedProperty spritePrepared;
		private SerializedProperty modelPrepared;

		void OnEnable() {
			syncTarget = new SerializedObject(target);

			childIndex = syncTarget.FindProperty("selectedChild");
			syncTypeLink = syncTarget.FindProperty("syncType");

			childOptionList = syncTarget.FindProperty("optionNames");
			modelShapeList = syncTarget.FindProperty("modelShapeNames");
			
			spriteAITarget = syncTarget.FindProperty("mouthSpriteAI");
			spriteETarget = syncTarget.FindProperty("mouthSpriteE");
			spriteEtcTarget = syncTarget.FindProperty("mouthSpriteEtc");
			spriteFVTarget = syncTarget.FindProperty("mouthSpriteFV");
			spriteLTarget = syncTarget.FindProperty("mouthSpriteL");
			spriteMBPTarget = syncTarget.FindProperty("mouthSpriteMBP");
			spriteOTarget = syncTarget.FindProperty("mouthSpriteO");
			spriteUTarget = syncTarget.FindProperty("mouthSpriteU");
			spriteWQTarget = syncTarget.FindProperty("mouthSpriteWQ");
			spriteRestTarget = syncTarget.FindProperty("mouthSpriteRest");

			modelAITarget = syncTarget.FindProperty("mouthModelAI");
			modelETarget = syncTarget.FindProperty("mouthModelE");
			modelEtcTarget = syncTarget.FindProperty("mouthModelEtc");
			modelFVTarget = syncTarget.FindProperty("mouthModelFV");
			modelLTarget = syncTarget.FindProperty("mouthModelL");
			modelMBPTarget = syncTarget.FindProperty("mouthModelMBP");
			modelOTarget = syncTarget.FindProperty("mouthModelO");
			modelUTarget = syncTarget.FindProperty("mouthModelU");
			modelWQTarget = syncTarget.FindProperty("mouthModelWQ");
			
			mouthToggle = syncTarget.FindProperty("mouthShapeToggle");
			spritePrepared = syncTarget.FindProperty("spriteReady");
			modelPrepared = syncTarget.FindProperty("modelReady");
		}

		public override void OnInspectorGUI() {
			syncTarget.Update();

			string[] selectableOptions = new string[childOptionList.arraySize];
			for(int i = 0; i < childOptionList.arraySize; i++) {
				selectableOptions[i] = childOptionList.GetArrayElementAtIndex(i).stringValue;
			}

			string[] modelShapeOptions = new string[modelShapeList.arraySize];
			for(int j = 0; j < modelShapeList.arraySize; j++) {
				modelShapeOptions[j] = modelShapeList.GetArrayElementAtIndex(j).stringValue;
			}

			childIndex.intValue = EditorGUILayout.Popup("Select Child:", childIndex.intValue, selectableOptions);

			mouthToggle.boolValue = EditorGUILayout.Foldout(mouthToggle.boolValue, "Mouth Shapes");
			if(mouthToggle.boolValue) {
				switch(syncTypeLink.intValue) {
				case 0:
					EditorGUILayout.LabelField("AI Shape");
					EditorGUILayout.LabelField("E Shape");
					EditorGUILayout.LabelField("Etc Shape");
					EditorGUILayout.LabelField("FV Shape");
					EditorGUILayout.LabelField("L Shape");
					EditorGUILayout.LabelField("MBP Shape");
					EditorGUILayout.LabelField("O Shape");
					EditorGUILayout.LabelField("U Shape");
					EditorGUILayout.LabelField("WQ Shape");
					EditorGUILayout.LabelField("Rest Shape");
					break;
				case 1:
					spriteAITarget.objectReferenceValue = EditorGUILayout.ObjectField("AI Shape:", spriteAITarget.objectReferenceValue, typeof(Sprite), true);
					spriteETarget.objectReferenceValue = EditorGUILayout.ObjectField("E Shape:", spriteETarget.objectReferenceValue, typeof(Sprite), true);
					spriteEtcTarget.objectReferenceValue = EditorGUILayout.ObjectField("Etc Shape:", spriteEtcTarget.objectReferenceValue, typeof(Sprite), true);
					spriteFVTarget.objectReferenceValue = EditorGUILayout.ObjectField("FV Shape:", spriteFVTarget.objectReferenceValue, typeof(Sprite), true);
					spriteLTarget.objectReferenceValue = EditorGUILayout.ObjectField("L Shape:", spriteLTarget.objectReferenceValue, typeof(Sprite), true);
					spriteMBPTarget.objectReferenceValue = EditorGUILayout.ObjectField("MBP Shape:", spriteMBPTarget.objectReferenceValue, typeof(Sprite), true);
					spriteOTarget.objectReferenceValue = EditorGUILayout.ObjectField("O Shape:", spriteOTarget.objectReferenceValue, typeof(Sprite), true);
					spriteUTarget.objectReferenceValue = EditorGUILayout.ObjectField("U Shape:", spriteUTarget.objectReferenceValue, typeof(Sprite), true);
					spriteWQTarget.objectReferenceValue = EditorGUILayout.ObjectField("WQ Shape:", spriteWQTarget.objectReferenceValue, typeof(Sprite), true);
					spriteRestTarget.objectReferenceValue = EditorGUILayout.ObjectField("Rest Shape:", spriteRestTarget.objectReferenceValue, typeof(Sprite), true);
					break;
				case 2:
					modelAITarget.intValue = EditorGUILayout.Popup("AI Shape:", modelAITarget.intValue, modelShapeOptions);
					modelETarget.intValue = EditorGUILayout.Popup("E Shape:", modelETarget.intValue, modelShapeOptions);
					modelEtcTarget.intValue = EditorGUILayout.Popup("Etc Shape:", modelEtcTarget.intValue, modelShapeOptions);
					modelFVTarget.intValue = EditorGUILayout.Popup("FV Shape:", modelFVTarget.intValue, modelShapeOptions);
					modelLTarget.intValue = EditorGUILayout.Popup("L Shape:", modelLTarget.intValue, modelShapeOptions);
					modelMBPTarget.intValue = EditorGUILayout.Popup("MBP Shape:", modelMBPTarget.intValue, modelShapeOptions);
					modelOTarget.intValue = EditorGUILayout.Popup("O Shape:", modelOTarget.intValue, modelShapeOptions);
					modelUTarget.intValue = EditorGUILayout.Popup("U Shape:", modelUTarget.intValue, modelShapeOptions);
					modelWQTarget.intValue = EditorGUILayout.Popup("WQ Shape:", modelWQTarget.intValue, modelShapeOptions);
					EditorGUILayout.LabelField("Rest Shape: - None -");
					break;
				default:
					EditorGUILayout.LabelField("AI Shape");
					EditorGUILayout.LabelField("E Shape");
					EditorGUILayout.LabelField("Etc Shape");
					EditorGUILayout.LabelField("FV Shape");
					EditorGUILayout.LabelField("L Shape");
					EditorGUILayout.LabelField("MBP Shape");
					EditorGUILayout.LabelField("O Shape");
					EditorGUILayout.LabelField("U Shape");
					EditorGUILayout.LabelField("WQ Shape");
					EditorGUILayout.LabelField("Rest Shape");
					break;
				}
			}

			if(syncTypeLink.intValue == 0) {
				GUI.enabled = false;
			}
			if(syncTypeLink.intValue == 1 && !spritePrepared.boolValue) {
				GUI.enabled = false;
			}
			if(syncTypeLink.intValue == 2 && !modelPrepared.boolValue) {
				GUI.enabled = false;
			}
			if(GUILayout.Button("Create Animation")) {
				//Time to try creating a sub-object animation
				CheshireCreate freshMaker = EditorWindow.GetWindow<CheshireCreate>(true, "Create Lip Sync Animation", true);
				freshMaker.CheshireCreateInit((target as Cheshire));
			}
			if(syncTypeLink.intValue == 1 || !spritePrepared.boolValue) {
				GUI.enabled = true;
			}
			if(syncTypeLink.intValue == 2 || !modelPrepared.boolValue) {
				GUI.enabled = true;
			}

			syncTarget.ApplyModifiedProperties();
		}

	}

}
