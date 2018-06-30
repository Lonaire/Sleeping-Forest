using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingForest
{
	public class Game : MonoBehaviour {
		//public const int MAX_IMPROVEMENTS = 5;
		public float treePercentPerClick = 0.01f;
		public BigInt event_multiplier;
		public BigInt event_termites;
		public BigInt event_fires;
		public static Game self = null;
		public Leafs leafs;
		public UIController controller;
		public List<Improvement> improvements;

		void Start () {
			StartCoroutine(Tick());
		}

		public void Init () {
			leafs = new Leafs();

			improvements = new List<Improvement>();
			improvements.Add(new Fertilizer());
			improvements.Add(new CrazyGardener());
			improvements.Add(new UndergroundSource());
			improvements.Add(new GrowthMutagen());
			improvements.Add(new ADK());
		}

		void Awake () {
			self = this;
		}

		IEnumerator Tick() {
			while (true) {
				if (leafs != null)
					leafs.leafCounter += improvements[(int)EnumImprovements.crazyGardener].Value;
				GenerateEvent();
				yield return new WaitForSeconds(1.0f);
			}
		}

		void GenerateEvent(int chance = 5) {
			event_multiplier = new BigInt(0.1f);
			event_termites = new BigInt(0.05f);
			event_fires = new BigInt(0.1f);

			if (Random.Range(0, 100) >= chance)
				return;
			else if (Random.Range(0,100) >= 0 &&  Random.Range(0,100) <= 29) {
					//Множитель (не работате верно при 1)
					leafs.leafsPerClick += (leafs.leafsPerClick * event_multiplier);
					Debug.Log("Множитель активен");
				}
				else if (Random.Range(0,100) >= 30 &&  Random.Range(0,100) <= 55) {
					//Нашествие термитов
					//if (controller.treeCount > 0.0f)
						//controller.treeCount -= (controller.treeCount * event_termites);
					Debug.Log("Термиты активны");
				}
				else if (Random.Range(0,100) >= 56 &&  Random.Range(0,100) <= 100) {
					//Пожары
					leafs.leafCounter -= (leafs.leafCounter * event_fires);
					Debug.Log("Пожары активны");
				}
		}
	}

	public class Leafs {
		public BigInt leafCounter;
		public BigInt leafsPerClick;

		public Leafs() {
			leafCounter = new BigInt(0.0f);
			leafsPerClick = new BigInt(1.0f);
		}

	}
}
