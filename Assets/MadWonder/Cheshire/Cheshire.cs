using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MadWonder {

	[ExecuteInEditMode]
	public class Cheshire : MonoBehaviour {

		public Transform targetChild;
		public int selectedChild;
		public int syncType;
		public string[] optionNames;
		public string targetPath;
		public string[] modelShapeNames;
		
		public Sprite mouthSpriteAI;
		public Sprite mouthSpriteE;
		public Sprite mouthSpriteEtc;
		public Sprite mouthSpriteFV;
		public Sprite mouthSpriteL;
		public Sprite mouthSpriteMBP;
		public Sprite mouthSpriteO;
		public Sprite mouthSpriteU;
		public Sprite mouthSpriteWQ;
		public Sprite mouthSpriteRest;

		public int mouthModelAI;
		public int mouthModelE;
		public int mouthModelEtc;
		public int mouthModelFV;
		public int mouthModelL;
		public int mouthModelMBP;
		public int mouthModelO;
		public int mouthModelU;
		public int mouthModelWQ;
		
		public bool mouthShapeToggle;

		public bool spriteReady;
		public bool modelReady;
		
		// Use this for initialization
		void Start () {
		}
		
		void Awake () {
			//This function is executed first in Editor Mode when the script is added to an object, use it for editor-related initialization
			syncType = SyncStateCheck(this.gameObject);
			UpdateOptionList();
			modelShapeNames = GetPossibleShapes();
			targetChild = null;
			targetPath = string.Empty;
		}
		
		// Update is called once per frame
		void Update () {
			if(Application.isEditor) {
				UpdateCheshireEditor();
			}
			if(this.GetComponent<AudioSource>() != null) {
				if(this.GetComponent<AudioSource>().isPlaying && this.GetComponent<AudioSource>().time > 0.0f) {
					if(this.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length > 0) {
					}
				}
			}
		}
		
		public void UpdateCheshireEditor() {
			UpdateOptionList();
			if(selectedChild >= optionNames.Length) {
				selectedChild = 0;
			}
			UpdateSyncType();
			if(syncType == 1) {
				spriteReady = SpritePrepared();
			}
			if(syncType == 2) {
				modelShapeNames = GetPossibleShapes();
				modelReady = ModelPrepared();
			}
		}
		
		public void UpdateSyncType() {
			//This function updates the current Sync Type based on the selected child
			if(selectedChild == 0) {
				syncType = SyncStateCheck(this.gameObject);
				targetChild = null;
				return; //If the selected child is zero, than no child has been selected, and nothing needs to be changed
			}
			else if(selectedChild > 0 && selectedChild <= this.PossibleSprites.Length) {
				syncType = SyncStateCheck(this.PossibleSprites[selectedChild - 1].gameObject);
				targetChild = this.PossibleSprites[selectedChild - 1].gameObject.transform;
			}
			else if(selectedChild > this.PossibleSprites.Length) {
				syncType = SyncStateCheck(this.PossibleModels[selectedChild - PossibleSprites.Length - 1].gameObject);
				targetChild = this.PossibleModels[selectedChild - PossibleSprites.Length - 1].gameObject.transform;
			}
		}
		
		public void UpdateOptionList() {
			optionNames = new string[this.PossibleSprites.Length + this.PossibleModels.Length + 1];
			optionNames[0] = "- none -";
			for(int i = 1; i < this.PossibleSprites.Length + 1; i++) {
				optionNames[i] = this.PossibleSprites[i - 1].gameObject.name;
			}
			for(int j = this.PossibleSprites.Length + 1; j < this.PossibleSprites.Length + this.PossibleModels.Length + 1; j++) {
				optionNames[j] = this.PossibleModels[j - this.PossibleSprites.Length - 1].gameObject.name;
			}
		}
		
		public string GetTargetPath() {
			//For some of the Animation creation functions, we will require a relative path to the child objects
			string processPath = string.Empty;
			if(targetChild == null) {
				return processPath;
			}
			string rootName = this.gameObject.name;
			Transform testTrans = targetChild.gameObject.transform;
			string testName = targetChild.gameObject.name;
			
			processPath = processPath.Insert(0, testName);
			
			testTrans = testTrans.parent.gameObject.transform;
			testName = testTrans.gameObject.name;
			
			while(testName != rootName) {
				processPath = processPath.Insert(0, testName + "/");
				testTrans = testTrans.parent.gameObject.transform;
				testName = testTrans.gameObject.name;
			}
			
			return processPath;
			//This function uses a while loop to retrieve the relative path of child objects and return it as a string
		}
		
		public string[] GetPossibleShapes() {
			string[] blendShapeOptions;
			
			if(targetChild != null) {
				if(targetChild.gameObject.GetComponent<SkinnedMeshRenderer>() == null) {
					blendShapeOptions = new string[1];
					blendShapeOptions[0] = "- none -";
				}
				else {
					blendShapeOptions = new string[targetChild.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.blendShapeCount + 1];
					blendShapeOptions[0] = "- none -";
					for(int i = 1; i < blendShapeOptions.Length; i++) {
						blendShapeOptions[i] = targetChild.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeName(i - 1);
					}
				}
			}
			else {
				if(gameObject.GetComponent<SkinnedMeshRenderer>() == null) {
					blendShapeOptions = new string[1];
					blendShapeOptions[0] = "- none -";
				}
				else {
					blendShapeOptions = new string[GetComponent<SkinnedMeshRenderer>().sharedMesh.blendShapeCount + 1];
					blendShapeOptions[0] = "- none -";
					for(int i = 1; i < blendShapeOptions.Length; i++) {
						blendShapeOptions[i] = GetComponent<SkinnedMeshRenderer>().sharedMesh.GetBlendShapeName(i - 1);
					}
				}
			}
			
			return blendShapeOptions;
		}
		
		public int SyncStateCheck(GameObject freshTest) {
			int stateTest = 0;
			if(freshTest.GetComponent<SpriteRenderer>() != null && freshTest.GetComponent<SkinnedMeshRenderer>() == null) {
				stateTest = 1;
			}
			else if(freshTest.GetComponent<SkinnedMeshRenderer>() != null && freshTest.GetComponent<SpriteRenderer>() == null) {
				stateTest = 2;
			}
			return stateTest;
		}
		
		public SpriteRenderer[] PossibleSprites {
			get {
				SpriteRenderer[] freshRenderers;
				int trialCounter = 0;
				foreach(SpriteRenderer trialRender in GetComponentsInChildren<SpriteRenderer>()) {
					if(trialRender.gameObject != this.gameObject) {
						trialCounter++;
					}
				}
				freshRenderers = new SpriteRenderer[trialCounter];
				int updateCount = 0;
				for(int i = 0; i < GetComponentsInChildren<SpriteRenderer>().Length; i++) {
					if(GetComponentsInChildren<SpriteRenderer>()[i].gameObject != this.gameObject) {
						freshRenderers[updateCount] = GetComponentsInChildren<SpriteRenderer>()[i];
						updateCount++;
					}
				}
				return freshRenderers;
			}
		}
		
		public SkinnedMeshRenderer[] PossibleModels {
			get {
				SkinnedMeshRenderer[] freshMeshRenderers;
				int trialCounter = 0;
				foreach(SkinnedMeshRenderer trialRender in GetComponentsInChildren<SkinnedMeshRenderer>()) {
					if(trialRender.gameObject != this.gameObject) {
						trialCounter++;
					}
				}
				freshMeshRenderers = new SkinnedMeshRenderer[trialCounter];
				int updateCount = 0;
				for(int i = 0; i < GetComponentsInChildren<SkinnedMeshRenderer>().Length; i++) {
					if(GetComponentsInChildren<SkinnedMeshRenderer>()[i].gameObject != this.gameObject) {
						freshMeshRenderers[updateCount] = GetComponentsInChildren<SkinnedMeshRenderer>()[i];
						updateCount++;
					}
				}
				return freshMeshRenderers;
			}
		}

		public bool SpritePrepared() {
			bool spriteReady = true;

			if(mouthSpriteAI == null) {
				spriteReady = false;
			}
			if(mouthSpriteE == null) {
				spriteReady = false;
			}
			if(mouthSpriteEtc == null) {
				spriteReady = false;
			}
			if(mouthSpriteFV == null) {
				spriteReady = false;
			}
			if(mouthSpriteL == null) {
				spriteReady = false;
			}
			if(mouthSpriteMBP == null) {
				spriteReady = false;
			}
			if(mouthSpriteO == null) {
				spriteReady = false;
			}
			if(mouthSpriteRest == null) {
				spriteReady = false;
			}
			if(mouthSpriteU == null) {
				spriteReady = false;
			}
			if(mouthSpriteWQ == null) {
				spriteReady = false;
			}

			return spriteReady;
		}

		public bool ModelPrepared() {
			bool modelReady = true;
			if(mouthModelAI == 0) {
				modelReady = false;
			}
			if(mouthModelE == 0) {
				modelReady = false;
			}
			if(mouthModelEtc == 0) {
				modelReady = false;
			}
			if(mouthModelFV == 0) {
				modelReady = false;
			}
			if(mouthModelL == 0) {
				modelReady = false;
			}
			if(mouthModelMBP == 0) {
				modelReady = false;
			}
			if(mouthModelO == 0) {
				modelReady = false;
			}
			if(mouthModelU == 0) {
				modelReady = false;
			}
			if(mouthModelWQ == 0) {
				modelReady = false;
			}
			return modelReady;
		}

		public Sprite GetSprite(string spriteName) {
			Sprite retrieveSprite = new Sprite();
			switch(spriteName) {
			case "AI":
				retrieveSprite = mouthSpriteAI;
				break;
			case "E":
				retrieveSprite = mouthSpriteE;
				break;
			case "etc":
				retrieveSprite = mouthSpriteEtc;
				break;
			case "FV":
				retrieveSprite = mouthSpriteFV;
				break;
			case "L":
				retrieveSprite = mouthSpriteL;
				break;
			case "MBP":
				retrieveSprite = mouthSpriteMBP;
				break;
			case "O":
				retrieveSprite = mouthSpriteO;
				break;
			case "U":
				retrieveSprite = mouthSpriteU;
				break;
			case "WQ":
				retrieveSprite = mouthSpriteWQ;
				break;
			case "rest":
				retrieveSprite = mouthSpriteRest;
				break;
			default:
				retrieveSprite = mouthSpriteRest;
				break;
			}
			return retrieveSprite;
		}

		public int GetAdjustedBlend(string blendName) {
			return GetBlendList().IndexOf(GetMappedBlend(blendName));
		}
		public List<int> GetBlendList() {
			List<int> retrieveBlends = new List<int>();
			if(!retrieveBlends.Contains(GetMappedBlend("AI"))) {
				retrieveBlends.Add(GetMappedBlend("AI"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("E"))) {
				retrieveBlends.Add(GetMappedBlend("E"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("etc"))) {
				retrieveBlends.Add(GetMappedBlend("etc"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("FV"))) {
				retrieveBlends.Add(GetMappedBlend("FV"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("L"))) {
				retrieveBlends.Add(GetMappedBlend("L"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("MBP"))) {
				retrieveBlends.Add(GetMappedBlend("MBP"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("O"))) {
				retrieveBlends.Add(GetMappedBlend("O"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("U"))) {
				retrieveBlends.Add(GetMappedBlend("U"));
			}
			if(!retrieveBlends.Contains(GetMappedBlend("WQ"))) {
				retrieveBlends.Add(GetMappedBlend("WQ"));
			}
			return retrieveBlends;
		}
		public int GetMappedBlend(string blendName) {
			int retrieveBlend = 0;
			switch(blendName) {
			case "AI":
				retrieveBlend = mouthModelAI - 1;
				break;
			case "E":
				retrieveBlend = mouthModelE - 1;
				break;
			case "etc":
				retrieveBlend = mouthModelEtc - 1;
				break;
			case "FV":
				retrieveBlend = mouthModelFV - 1;
				break;
			case "L":
				retrieveBlend = mouthModelL - 1;
				break;
			case "MBP":
				retrieveBlend = mouthModelMBP - 1;
				break;
			case "O":
				retrieveBlend = mouthModelO - 1;
				break;
			case "U":
				retrieveBlend = mouthModelU - 1;
				break;
			case "WQ":
				retrieveBlend = mouthModelWQ - 1;
				break;
			default:
				break;
			}
			return retrieveBlend;
		}

		public void PlaySyncAudio(AudioClip freshAudio) {
			if(this.targetChild != null) {
				if(this.targetChild.gameObject.GetComponent<AudioSource>() == null) {
					this.targetChild.gameObject.AddComponent<AudioSource>();
				}
				this.targetChild.gameObject.GetComponent<AudioSource>().clip = freshAudio;
				this.targetChild.gameObject.GetComponent<AudioSource>().Play();
			}
			else {
				if(this.gameObject.GetComponent<AudioSource>() == null) {
					this.gameObject.AddComponent<AudioSource>();
				}
				this.GetComponent<AudioSource>().clip = freshAudio;
				this.GetComponent<AudioSource>().Play();
			}
		}

		public void PlayLipSync(string animState, string audioLocation, string idleState) {
			GameObject targetObject;
			if(this.targetChild != null) {
				targetObject = this.targetChild.gameObject;
			}
			else {
				targetObject = this.gameObject;
			}

			if(targetObject.GetComponent<AudioSource>() == null) {
				targetObject.AddComponent<AudioSource>();
			}
			targetObject.GetComponent<AudioSource>().clip = Resources.Load(audioLocation) as AudioClip;
			targetObject.GetComponent<AudioSource>().Play();

			//if(targetObject.GetComponent<Animator>().Play(""
			targetObject.GetComponent<Animator>().Play(idleState);

			#if UNITY_ANDROID
			StartCoroutine(AnimDelay(animState, targetObject));
			#endif

			#if UNITY_STANDALONE
			targetObject.GetComponent<Animator>().Play(animState);
			Debug.Log("Hello World");
			#endif
		}

		public IEnumerator AnimDelay(string animStateName, GameObject targetObject) {
			yield return new WaitForSeconds(0.25f);
			targetObject.GetComponent<Animator>().Play(animStateName);
		}

		public void TrialPlaySync(string animState, string audioPath) {
			GameObject targetObject;
			if(this.targetChild != null) {
				targetObject = this.targetChild.gameObject;
			}
			else {
				targetObject = this.gameObject;
			}
			if(targetObject.GetComponent<AudioSource>() == null) {
				targetObject.AddComponent<AudioSource>();
			}
			targetObject.GetComponent<AudioSource>().clip = Resources.Load(audioPath) as AudioClip;
			targetObject.GetComponent<AudioSource>().Play();
			targetObject.GetComponent<Animator>().Play(animState);
			//Debug.Log(targetObject.GetComponent<Animator>().GetCurrentAnimationClipState(-1).Length);
			Debug.Log(targetObject.GetComponent<AudioSource>().time);
		}
	}

}
