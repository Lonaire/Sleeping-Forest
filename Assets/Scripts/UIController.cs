using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SleepingForest
{
	public class UIController : MonoBehaviour {
		[SerializeField] Slider slider;
		[SerializeField] Button tree_button;
		[SerializeField] Button improve_fertilizer_button;
		[SerializeField] Button improve_gardener_button;
		[SerializeField] Button improve_source_button;
		[SerializeField] Button improve_mutagen_button;
		[SerializeField] Button improve_adk_button;
		[SerializeField] Button sound_toggle;
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
		[SerializeField] Text TreeCount;
		[SerializeField] Text EventTime;
		[SerializeField] Image EventIcon;
		[SerializeField] Sprite multiplier_icon;	
		[SerializeField] Sprite termites_icon;
		[SerializeField] Sprite fire_icon;
		[SerializeField] Sprite sound_on_icon;
		[SerializeField] Sprite sound_off_icon;

		private const int TREE_ADDITOR = 1000;

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

			if (sound_toggle != null)
				sound_toggle.onClick.AddListener(Game.self.ToggleSound);
		}
		
		void Update () {
			UpdateFields();
			UpdateButtons();
		}

		void UpdateFields() {
			var improvements = Game.self.improvements;

			leafLabel.text = Game.self.leafs.leafCounter.ToString();
			TreeCount.text = Game.self.treeCount.ToStringRounded();
			ferDescription.text = string.Format ("Кроны становятся гуще.\nКоличество листьев увеличено на {0} ед.", improvements[(int)EnumImprovements.fertilizer].Value.ToString());
			garDescription.text = string.Format ("Вы можете спать спокойно.\nКоличество листьев + {0} / сек.", improvements[(int)EnumImprovements.crazyGardener].Value.ToString());
			sourceDescription.text = string.Format ("Полезные свойства впитываются быстрее.\nПольза от удобрения увеличена на {0} ед.", improvements[(int)EnumImprovements.undergroundSource].Value.ToString());
			mutagenDescription.text = string.Format("Вызывает мутации.\nДеревья растут в {0:0.00} раза быстрее.", ((float)improvements[(int)EnumImprovements.growthMutagen].Value) * 100);
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

		public void SetSoundIcon(bool is_on) {
			var img = sound_toggle.GetComponent<Image>();
			if (img != null)
				img.sprite = is_on ? sound_on_icon : sound_off_icon;
		}

		public void OnClickTree () {
			Game.self.leafs.leafCounter += Game.self.leafs.leafsPerClick;
			LeafsPerClick.text = string.Format ("+{0}", Game.self.leafs.leafsPerClick.ToString());
			
			if (Game.self.ActiveEvent != EnumActiveEvent.Termites) {
				slider.value = Mathf.Clamp(slider.value + Game.self.treePercentPerClick, 0.0f, 1.0f);
				if (slider.value == 1.0f) {
					slider.value = 0;
					var adk = Game.self.improvements[(int)EnumImprovements.adk];
					BigInt tree_count = BigInt.Empty;
					if (adk.Lvl > 0)
						tree_count += adk.Value;
					tree_count += 1.0f;
					Game.self.leafs.leafCounter += (tree_count * TREE_ADDITOR);
					Game.self.treeCount += tree_count;
				}
			}
		}

		public void OnClickImprovement (EnumImprovements impr) {
			if (Game.self.improvements.Count < (int)impr)
				return;

			var improvement = Game.self.improvements[(int)impr];
			if(improvement != null)
				improvement.Improve();
		}

		public void OnEventChanged(EnumActiveEvent e) {
			switch (e) {
				case EnumActiveEvent.Multiplier:
					EventIcon.sprite = multiplier_icon;
					break;
				case EnumActiveEvent.Termites:
					EventIcon.sprite = termites_icon;
					break;
				case EnumActiveEvent.Fire:
					EventIcon.sprite = fire_icon;
					break;
			}
			EventIcon.gameObject.SetActive(e != EnumActiveEvent.None);
		}

		public void EventTick(int time) {
			EventTime.text = string.Format("{0} сек", time);
		}
	}
}
