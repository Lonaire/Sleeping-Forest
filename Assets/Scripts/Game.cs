using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingForest
{
	public class Game : MonoBehaviour {
		public const int MAX_IMPROVEMENTS = 5;
		public float treePercent = 0.0f;
		public BigInt treeCount;
		public static Game self = null;
		public Leafs leafs;
		public List<Improvement> improvements;

		void Start () {
			//
		}

		public void Init () {
			leafs = new Leafs();

			improvements = new List<Improvement>();
			improvements.Add(new Fertilizer());
			improvements.Add(new CrazyGardener());
			improvements.Add(new UndergroundSource());
			improvements.Add(new GrowthMutagen());
			improvements.Add(new ADK());

			treeCount = new BigInt();
		}

		void Awake () {
			self = this;
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
