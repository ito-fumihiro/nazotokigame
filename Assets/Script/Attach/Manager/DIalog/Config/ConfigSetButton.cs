using Assets.Script.Controller.Manager.Dialog.Config;
using Assets.Script.Enum;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Manager.Dialog.Config
{
    /// <summary>
    /// コンフィグボタン
    /// </summary>
	public class ConfigSetButton : MonoBehaviour
	{
		/// <summary>
		/// 設定種別
		/// </summary>
		[SerializeField]
		private ConfigSettingType _configSettingType;

		/// <summary>
		/// 設定値
		/// </summary>
		[SerializeField]
		private float _settingValue;

		#region ボタンアクション

		/// <summary>
		/// クリック時
		/// </summary>
		public void Clicked()
		{
			var configController = GameObject.Find("Config").GetComponent<ConfigDialogController>();

			switch (_configSettingType)
			{
				case ConfigSettingType.Bgm:
					UtageUtil.GetNazotokiAdvEngineController().SetBgmVolume(_settingValue);
					break;
				case ConfigSettingType.Se:
					UtageUtil.GetNazotokiAdvEngineController().SetSeVolume(_settingValue);
					break;
				case ConfigSettingType.Voice:
					UtageUtil.GetNazotokiAdvEngineController().SetVoiceVolume(_settingValue);
					break;
				case ConfigSettingType.MessageSpeed:
					UtageUtil.GetNazotokiAdvEngineController().SetMessageSpeed(_settingValue);
					break;
				default:
					break;
			}
			configController.InitIsSetObjectsPosition();
		}

		#endregion
	}
}
