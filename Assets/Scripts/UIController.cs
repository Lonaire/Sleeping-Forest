using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SleepingForest
{
	public class UIController : MonoBehaviour {

		[SerializeField] Button tree_button;
		[SerializeField] Button improve_fertilizer_button;
		[SerializeField] Button improve_gardener_button;
		[SerializeField] Button improve_source_button;
		[SerializeField] Button improve_mutagen_button;
		[SerializeField] Button improve_adk_button;
		[SerializeField] Text leafLabel;
		[SerializeField] Text ferDescription;
		[SerializeField] Text garDescription;
		[SerializeField] Text sourceDescription;
		[SerializeField] Text mutagenDescription;
		[SerializeField] Text adkDescription;
		[SerializeField] Text ferLvl;
		[SerializeField] Text garLvl;
		[SerializeField] Text sourceLvl;
		[SerializeField] Text mutagenLvl;
		[SerializeField] Text adkLvl;
		[SerializeField] Text ferPrice;
		[SerializeField] Text garPrice;
		[SerializeField] Text sourcePrice;
		[SerializeField] Text mutagenPrice;
		[SerializeField] Text adkPrice;
		[SerializeField] Text LeafsPerClick;


		void Start () {
			Game.self.Init();

			var button = tree_button.GetComponent<Button>();
			if (button != null)
				button.onClick.AddListener(OnClickTree);

			button = improve_fertilizer_button.GetComponent<Button>();
			if (button != null)
				button.onClick.AddListener(Game.self.improvements[(int)EnumImprovements.fertilizer].Improve);

			button = improve_gardener_button.GetComponent<Button>();
			if (button != null)
				button.onClick.AddListener(Game.self.improvements[(int)EnumImprovements.crazyGardener].Improve);

			button = improve_source_button.GetComponent<Button>();
			if (button != null)
				button.onClick.AddListener(Game.self.improvements[(int)EnumImprovements.undergroundSource].Improve);

			button = improve_mutagen_button.GetComponent<Button>();
			if (button != null)
				button.onClick.AddListener(Game.self.improvements[(int)EnumImprovements.growthMutagen].Improve);

			button = improve_adk_button.GetComponent<Button>();
			if (button != null)
				button.onClick.AddListener(Game.self.improvements[(int)EnumImprovements.adk].Improve);
		}
		
		void Update () {
			UpdateFields();
			UpdateButtons();
		}

		void UpdateFields() {
			var improvements = Game.self.improvements;

			leafLabel.text = Game.self.leafs.leafCounter.ToString();
			ferDescription.text = string.Format ("Кроны становятся гуще.\nКоличество листьев увеличено на {0} ед.", improvements[(int)EnumImprovements.fertilizer].Value.ToString());
			garDescription.text = string.Format ("Вы можете спать спокойно.\nКоличество листьев + {0} / сек.", improvements[(int)EnumImprovements.crazyGardener].Value.ToString());
			sourceDescription.text = string.Format ("Полезные свойства впитываются быстрее.\nПольза от удобрения увеличена на {0} ед.", improvements[(int)EnumImprovements.undergroundSource].Value.ToString());
			mutagenDescription.text = string.Format("Вызывает мутации.\nДеревья растут в {0:0.00} раза быстрее.", improvements[(int)EnumImprovements.growthMutagen].Value.ToString());
			adkDescription.text = string.Format("Аппарат для клонирования.\nМножитель деревьев + {0}.", improvements[(int)EnumImprovements.adk].Value.ToString());
			ferPrice.text = improvements[(int)EnumImprovements.fertilizer].Price.ToString();
			garPrice.text = improvements[(int)EnumImprovements.crazyGardener].Price.ToString();
			sourcePrice.text = improvements[(int)EnumImprovements.undergroundSource].Price.ToString();
			mutagenPrice.text = improvements[(int)EnumImprovements.growthMutagen].Price.ToString();
			adkPrice.text = improvements[(int)EnumImprovements.adk].Price.ToString();
			ferLvl.text = string.Format ("Ур. {0}", improvements[(int)EnumImprovements.fertilizer].Lvl.ToString());
			garLvl.text = string.Format ("Ур. {0}", improvements[(int)EnumImprovements.crazyGardener].Lvl.ToString());
			sourceLvl.text = string.Format("Ур. {0}", improvements[(int)EnumImprovements.undergroundSource].Lvl.ToString());
			mutagenLvl.text = string.Format("Ур. {0}", improvements[(int)EnumImprovements.growthMutagen].Lvl.ToString());
			adkLvl.text = string.Format("Ур. {0}", improvements[(int)EnumImprovements.adk].Lvl.ToString());
		}

		void UpdateButtons() {
			var improvements = Game.self.improvements;

			SetButtonEnabled(improve_fertilizer_button, Game.self.leafs.leafCounter >= improvements[(int)EnumImprovements.fertilizer].Price);
			SetButtonEnabled(improve_gardener_button, Game.self.leafs.leafCounter >= improvements[(int)EnumImprovements.crazyGardener].Price);
			SetButtonEnabled(improve_source_button, Game.self.leafs.leafCounter >= improvements[(int)EnumImprovements.undergroundSource].Price);
			SetButtonEnabled(improve_mutagen_button, Game.self.leafs.leafCounter >= improvements[(int)EnumImprovements.growthMutagen].Price);
			SetButtonEnabled(improve_adk_button, Game.self.leafs.leafCounter >= improvements[(int)EnumImprovements.adk].Price);
		}

		void SetButtonEnabled(Button button, bool is_enabled) {
			var img = button.gameObject.GetComponent<Image>();
			if (img != null) {
				var color = new Color(img.color.r, img.color.g, img.color.b, is_enabled ? 0.01f : 1.0f);
				img.color = color;
			}
			button.enabled = is_enabled;
		}

		public void OnClickTree () {
			Game.self.leafs.leafCounter += Game.self.leafs.leafsPerClick;
			LeafsPerClick.text = string.Format ("+{0}", Game.self.leafs.leafsPerClick.ToString());

		}

		public void OnClickImprovement (EnumImprovements impr) {
			if (Game.self.improvements.Count < (int)impr)
				return;

			var improvement = Game.self.improvements[(int)impr];
			if(improvement != null)
				improvement.Improve();
		}
	}
}
