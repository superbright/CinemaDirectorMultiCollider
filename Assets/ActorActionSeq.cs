using System.Collections.Generic;
using UnityEngine;

namespace CinemaDirector
{
	[System.Serializable]
	public class ColliderAction {

		public GameObject collider;
		public Cutscene cutscene;

	}
	/// <summary>
	/// Plays through a list of given cutscenes one by one.
	/// </summary>
	public class ActorActionSeq : MonoBehaviour
	{
		public List<ColliderAction> ColliderActions;
		private int index = 0;
		bool loop = false;

		/// <summary>
		/// Play the first cutscene and waits for it to finish
		/// </summary>
		void Start()
		{
			int i = 0;
			foreach (ColliderAction cutscene in ColliderActions) {
				CollisionHandler colhandlr = cutscene.collider.AddComponent<CollisionHandler> ();
				colhandlr.index = i;
				colhandlr.onCollisionTriggered += ObjectCollisionTriggered;
				i++;
			}
		}

		void Test() {

			if (ColliderActions.Count > 0) {
				ColliderActions [index].cutscene.CutsceneFinished += CutsceneQueue_CutsceneFinished;
				ColliderActions [index].cutscene.Play ();

			}
		}

		void ObjectCollisionTriggered(object sender, CollisionEventArgs e) {
			Debug.Log ("ok collider " + e.index);

			for (int k = 0; k < ColliderActions.Count; k++) {
				if (k == e.index) {
					ColliderActions [e.index].cutscene.CutsceneFinished += CutsceneQueue_CutsceneFinished;
					ColliderActions [e.index].cutscene.Play ();
				} else {
					ColliderActions [k].collider.SetActive (false);
				}
			}
		}

		/// <summary>
		/// On cutscene finish, play the next cutscene.
		/// </summary>
		void CutsceneQueue_CutsceneFinished(object sender, CutsceneEventArgs e)
		{
			Debug.Log (e.targetScene);
			e.targetScene.CutsceneFinished -= CutsceneQueue_CutsceneFinished;
			for (int k = 0; k < ColliderActions.Count; k++) {
				ColliderActions [k].collider.SetActive (true);
			}

			if (loop) {
				index++;
				if (index == ColliderActions.Count) {
					index = 0;
				} 

				ColliderActions [index].cutscene.CutsceneFinished += CutsceneQueue_CutsceneFinished;
				ColliderActions [index].cutscene.Play ();
			}
		}

		/// <summary>
		/// Disables all collider objects.
		/// </summary>
		void DisableColliderObjects() {

		}
	}
}