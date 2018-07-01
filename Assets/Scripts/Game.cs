using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingForest
{
	public enum EnumActiveEvent {
		None,
		Multiplier,
		Termites,
		Fire
	}

	public class Game : MonoBehaviour {
		public UIController ui_controller;
		//public const int MAX_IMPROVEMENTS = 5;
		private float time = 20;
		public float treePercentPerClick = 0.01f;

		public BigInt event_multiplier;
		private BigInt event_termites;
		private BigInt event_fires;
		public static Game self = null;
		public Leafs leafs;
		public List<Improvement> improvements;
		public BigInt treeCount = BigInt.Empty;

		public bool hasActiveEvent = false;

		EnumActiveEvent activeEvent = EnumActiveEvent.None;
		public EnumActiveEvent ActiveEvent {
			get { return activeEvent; }
			set {
				activeEvent = value;
				if (ui_controller != null)
					ui_controller.OnEventChanged(activeEvent);
			}
		}

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
				var gardener = improvements[(int)EnumImprovements.crazyGardener];
				if (leafs != null && gardener != null && gardener.Lvl > 0)
					leafs.leafCounter += gardener.Value;
				if (ActiveEvent == EnumActiveEvent.None)
					GenerateEvent();
				yield return new WaitForSeconds(1.0f);
			}
		}

		void GenerateEvent(int chance = 10) {
			event_multiplier = new BigInt(0.1f);
			event_termites = new BigInt(0.05f);
			event_fires = new BigInt(0.05f);

			var num = Random.Range(0, 100);
			if (num >= chance)
				return;

			num = Random.Range(0, 100);
			if (num >= 0 && num <= 29) {
				//Множитель
				Debug.Log("Множитель активен");
				StartCoroutine(DoMultiplierEvent());
			}
			else if (num >= 30 && num <= 55) {
				//Нашествие термитов
				if (treeCount > BigInt.Empty) {
					Debug.Log("Термиты активны");
					StartCoroutine(DoTermitesEvent());
				}
			}
			else if (num >= 56 && num < 100) {
				//Пожары
				if (leafs.leafCounter > BigInt.Empty) {
					Debug.Log("Пожары активны");
					StartCoroutine(DoFireEvent());
				}
			}
		}

		public IEnumerator DoMultiplierEvent() {
			Debug.Log("Event is active");
			ActiveEvent = EnumActiveEvent.Multiplier;

			leafs.leafsPerClick += leafs.leafsPerClick * event_multiplier;

			int counter = 20;
			while (counter > 0) {
				if (ui_controller != null)
					ui_controller.EventTick(counter);
				counter--;
				yield return new WaitForSeconds(1.0f);
			}

			var fertilizer = improvements[(int)EnumImprovements.fertilizer];
			leafs.leafsPerClick = fertilizer.Value + new BigInt(1.0f);
			ActiveEvent = EnumActiveEvent.None;
			Debug.Log("Event is not active");
		}

		public IEnumerator DoTermitesEvent() {
			Debug.Log("Event is active");
			ActiveEvent = EnumActiveEvent.Termites;

			int counter = 20;
			while (counter > 0) {
				if (ui_controller != null)
					ui_controller.EventTick(counter);
				counter--;
				yield return new WaitForSeconds(1.0f);
			}

			ActiveEvent = EnumActiveEvent.None;
			Debug.Log("Event is not active");
		}

		public IEnumerator DoFireEvent() {
			Debug.Log("Event is active");
			ActiveEvent = EnumActiveEvent.Fire;

			int counter = 20;
			while (counter > 0) {
				leafs.leafCounter -= leafs.leafCounter * event_fires;
				if (ui_controller != null)
					ui_controller.EventTick(counter);
				counter--;
				yield return new WaitForSeconds(1.0f);
			}

			ActiveEvent = EnumActiveEvent.None;
			Debug.Log("Event is not active");
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
