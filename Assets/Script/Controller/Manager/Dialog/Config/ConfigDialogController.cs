using Assets.Script.Enum;
using Assets.Script.Utility;
using UnityEngine;

namespace Assets.Script.Controller.Manager.Dialog.Config
{
    /// <summary>
    /// コンフィグダイアログ
    /// </summary>
    [AddComponentMenu("Utage/ADV/ConfigDialogController")]
	public class ConfigDialogController : BaseUtageDialogController
	{
		/// <summary>
		/// Initialize
		/// </summary>
		protected override void Initialize()			
		{
			// 各セッティングボタンの位置を設定
			InitIsSetObjectsPosition();
		}

		/// <summary>
		/// 設定値のカーソル位置を初期化
		/// </summary>
		public void InitIsSetObjectsPosition()
        {
			var engine = UtageUtil.GetNazotokiAdvEngine();
			this.SetIsSetObjectPosition(ConfigSettingType.Bgm, engine.Config.BgmVolume);
			this.SetIsSetObjectPosition(ConfigSettingType.Se, engine.Config.SeVolume);
			this.SetIsSetObjectPosition(ConfigSettingType.Voice, engine.Config.VoiceVolume);
			this.SetIsSetObjectPosition(ConfigSettingType.MessageSpeed, engine.Config.MessageSpeed);
		}

		/// <summary>
		/// 現在の設定オブジェクトの位置を設定
		/// </summary>
		/// <param name="type">設定種別</param>
		/// <param name="setVal">設定値</param>
		private void SetIsSetObjectPosition(ConfigSettingType type, float setVal)
		{
			var objectName = "IsSet";
			switch (type)
			{
				case ConfigSettingType.Bgm:
					objectName += nameof(ConfigSettingType.Bgm);
					break;
				case ConfigSettingType.Se:
					objectName += nameof(ConfigSettingType.Se);
					break;
				case ConfigSettingType.Voice:
					objectName += nameof(ConfigSettingType.Voice);
					break;
				case ConfigSettingType.MessageSpeed:
					objectName += nameof(ConfigSettingType.MessageSpeed);
					break;
				default:
					return;
			}

			// 設定値の箇所にカーソルを持ってくる
			var cursorObject = GameObject.Find(objectName);
			var pos = cursorObject.transform.localPosition;
			float index = (setVal - 0.2f) / 0.2f;
			float offsetX = -38.0f;
			float dx = 51.0f * index;
			pos.x = offsetX + dx;
			cursorObject.transform.localPosition = pos;
		}
	}
}
