using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingForest
{
	public enum EnumImprovements {
		fertilizer,
		crazyGardener,
		undergroundSource,
		growthMutagen,
		adk
	}

	public abstract class Improvement {
		public BigInt Value;
		public int Lvl;
		public BigInt Price;

		public const float FERTILIZER_MULT = 1.1f;
		public const float SOURCE_MULT = 1.13f;
		public const float GARDENER_MULT = 1.17f;
		public const float PRICE_MULT = 1.3f;
		public const float ADK_PRICE_MULT = 1.5f;
		public const float MUTAGEN_ADDITOR = 0.05f;
		public const int ADK_ADDITOR = 1;

		public abstract void Improve();

	}

	//Удобрение
	public class Fertilizer : Improvement {
		public Fertilizer() {
			Value = new BigInt(10.0f);
			Price = new BigInt(20.0f);
		}

		public override void Improve() {
			var leaf_manager = Game.self.leafs;
			if (leaf_manager != null)
			{
				if (leaf_manager.leafCounter >= Price)
				{
					leaf_manager.leafCounter -= Price;
					Price *= PRICE_MULT;
					if (Lvl > 0)
						Value *= FERTILIZER_MULT;
					Lvl++;
					if (Game.self.improvements[(int)EnumImprovements.undergroundSource].Lvl > 0)
						Value += Game.self.improvements[(int)EnumImprovements.undergroundSource].Value;
					leaf_manager.leafsPerClick += Value;
				}
			}
		}
	}

	//Сумасшедший садовник
	public class CrazyGardener : Improvement {
		public CrazyGardener() {
			Value = new BigInt(100.0f);
			Price = new BigInt(170.0f);
		}

		public override void Improve(){

			var leaf_manager = Game.self.leafs;
			if (leaf_manager != null)
			{
				if (leaf_manager.leafCounter >= Price)
				{
					leaf_manager.leafCounter -= Price;
					leaf_manager.leafsPerClick += Value;
					Price *= PRICE_MULT;
					if (Lvl > 0)
						Value *= GARDENER_MULT;
					Lvl++;
				}
			}
		}
			
	}

	//Подземный источник
	public class UndergroundSource : Improvement {
		public UndergroundSource() {
			Value = new BigInt(25.0f);
			Price = new BigInt(350.0f);
		}

		public override void Improve(){
			var leaf_manager = Game.self.leafs;
			if (leaf_manager != null)
			{
				if (leaf_manager.leafCounter >= Price)
				{
					if (Game.self.improvements[(int)EnumImprovements.fertilizer].Lvl == 0)
						return;
					leaf_manager.leafCounter -= Price;
					leaf_manager.leafsPerClick += Value;
					Price *= PRICE_MULT;
					if (Lvl > 0)
						Value *= SOURCE_MULT;
					Lvl++;
				}
			}
		}
		
	}

	//Мутаген роста
	public class GrowthMutagen : Improvement {
		public GrowthMutagen() {
			Value = new BigInt(2.0f);
			Price = new BigInt(540.0f);
		}

		public override void Improve(){
			var leaf_manager = Game.self.leafs;
			if (leaf_manager != null)
			{
				if (leaf_manager.leafCounter >= Price)
				{
					leaf_manager.leafCounter -= Price;
					leaf_manager.leafsPerClick += Value;
					Price *= PRICE_MULT;
					if (Lvl > 0)
						Value += MUTAGEN_ADDITOR;
					Lvl++;
				}
			}
		}

	}

	//АДК-1000
	public class ADK : Improvement {
		public ADK() {
			Value = new BigInt(1.0f);
			Price = new BigInt(1500.0f);
		}

		public override void Improve(){
			var leaf_manager = Game.self.leafs;
			if (leaf_manager != null)
			{
				if (leaf_manager.leafCounter >= Price)
				{
					leaf_manager.leafCounter -= Price;
					leaf_manager.leafsPerClick += Value;
					Price *= ADK_PRICE_MULT;
					if (Lvl > 0)
						Value += ADK_ADDITOR;
					Lvl++;
				}
			}
		}
		
	}
}