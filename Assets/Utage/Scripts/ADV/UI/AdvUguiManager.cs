// UTAGE: Unity Text Adventure Game Engine (c) Ryohei Tokimura
using Assets.Script.Controller.Manager.Dialog.Config;
using Assets.Script.Controller.Manager.Dialog.Inventory;
using Assets.Script.Controller.Manager.Dialog.ItemDetail;
using Assets.Script.Controller.Manager.Dialog.Menu;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Utage
{
    /// <summary>
    /// UI全般の入力処理。
    /// 独自のキーボード入力などが必要な場合は
    /// これ（AdvUguiManager）かAdvUiManagerを継承して処理を書きかえること
    /// </summary>
    [AddComponentMenu("Utage/ADV/AdvUguiManager")]
	public class AdvUguiManager : AdvUiManager
	{
		// メッセージウィンドウ
		public AdvUguiMessageWindowManager MessageWindow{ get { return Engine.MessageWindowManager.UiMessageWindowManager as AdvUguiMessageWindowManager; }}

		[SerializeField]
		protected AdvUguiSelectionManager selection;

		[SerializeField]
		protected AdvUguiBacklogManager backLog;

		[SerializeField]
		protected ConfigDialogController config;

		[SerializeField]
		protected MenuDialogController menu;

		[SerializeField]
		protected InventoryDialogController inventory;

		[SerializeField]
		protected ItemDetailDialogController itemDetail;

		//マウスホイールによるバックログの有効・無効
		public bool DisableMouseWheelBackLog { get { return disableMouseWheelBackLog; } set { disableMouseWheelBackLog = value; } }
		[SerializeField]
		protected bool disableMouseWheelBackLog = false;

		public override void Open()
		{
			this.gameObject.SetActive(true);
			ChangeStatus(UiStatus.Default);
		}

		public override void Close()
		{
			this.gameObject.SetActive(false);
			MessageWindow.Close();
			if (selection != null) selection.Close();
			if (backLog != null) backLog.Close();
		}

		protected override void ChangeStatus(UiStatus newStatus)
		{
			switch (newStatus)
			{
				case UiStatus.Backlog:
					if (backLog == null) return;

					MessageWindow.Close();
					if (selection != null) selection.Close();
					Engine.Config.IsSkip = false;

					if (backLog != null) backLog.Open();
					if (config != null) config.Close();
					if (menu != null) menu.Close();
					if (config != null) inventory.Close();
					if (itemDetail != null) itemDetail.Close();
					break;

				case UiStatus.HideMessageWindow:
					MessageWindow.Close();
					if (selection != null) selection.Close();
					Engine.Config.IsSkip = false;

					if (backLog != null) backLog.Close();
					if (config != null) config.Close();
					if (menu != null) menu.Close();
					if (inventory != null) inventory.Close();
					if (itemDetail != null) itemDetail.Close();
					break;

				case UiStatus.Menu:
					MessageWindow.Close();
					if (selection != null) selection.Close();
					Engine.Config.IsSkip = false;

					if (menu != null) menu.Open();
					if (config != null) config.Close();
					if (backLog != null) backLog.Close();
					if (inventory != null) inventory.Close();
					if (itemDetail != null) itemDetail.Close();
					break;

				case UiStatus.Config:
					MessageWindow.Close();
					if (selection != null) selection.Close();
					Engine.Config.IsSkip = false;

					if (config != null) config.Open();
					if (menu != null) menu.Close();
					if (backLog != null) backLog.Close();
					if (inventory != null) inventory.Close();
					if (itemDetail != null) itemDetail.Close();
					break;

				case UiStatus.Inventory:
					MessageWindow.Close();
					if (selection != null) selection.Close();
					Engine.Config.IsSkip = false;

					if (inventory != null) inventory.Open();
					if (config != null) config.Close();
					if (menu != null) menu.Close();
					if (backLog != null) backLog.Close();
					if (itemDetail != null) itemDetail.Close();
					break;

				case UiStatus.ItemDetail:
					MessageWindow.Close();
					if (selection != null) selection.Close();
					Engine.Config.IsSkip = false;

					if (itemDetail != null) itemDetail.Open();
					if (inventory != null) inventory.Close();
					if (config != null) config.Close();
					if (menu != null) menu.Close();
					if (backLog != null) backLog.Close();
					break;

				case UiStatus.Default:
					MessageWindow.Open();
					if (selection != null) selection.Open();
					Engine.Config.IsSkip = false;

					if (backLog != null) backLog.Close();
					if (menu != null) menu.Close();
					if (config != null) config.Close();
					if (inventory != null) inventory.Close();
					if (itemDetail != null) itemDetail.Close();
					break;
			}
			this.status = newStatus;
		}

		//ウインドウ閉じるボタンが押された
		protected virtual void OnTapCloseWindow()
		{
			Status = UiStatus.HideMessageWindow;
		}

		protected virtual void Update()
		{
			//読み進みなどの入力
			bool IsInput = (Engine.Config.IsMouseWheelSendMessage && InputUtil.IsInputScrollWheelDown())
								|| InputUtil.IsInputKeyboadReturnDown();
			switch (Status)
			{
				case UiStatus.Backlog:
					break;
				case UiStatus.HideMessageWindow:	//メッセージウィンドウが非表示
					//右クリック
					if (InputUtil.IsMouseRightButtonDown())
					{	//通常画面に復帰
						Status = UiStatus.Default;
					}
					else if (!disableMouseWheelBackLog && InputUtil.IsInputScrollWheelUp())
					{
						//バックログ開く
						Status = UiStatus.Backlog;
					}
					break;
				case UiStatus.Default:
					if (IsShowingMessageWindow)
					{
						//テキストの更新
						Engine.Page.UpdateText();
					}
					if (IsShowingMessageWindow || Engine.SelectionManager.IsWaitInput)
					{	//入力待ち
						if (InputUtil.IsMouseRightButtonDown())
						{	//右クリックでウィンドウ閉じる
							Status = UiStatus.HideMessageWindow;
						}
						else if (!disableMouseWheelBackLog && InputUtil.IsInputScrollWheelUp())
						{	//バックログ開く
							Status = UiStatus.Backlog;
						}
						else
						{
							if (IsInput)
							{
								//メッセージ送り
								Engine.Page.InputSendMessage();
								base.IsInputTrig = true;
							}
						}
					}
					else
					{
						if (IsInput)
						{
							base.IsInputTrig = false;
						}
					}
					break;
			}
		}

		/// <summary>
		/// タッチされたとき
		/// </summary>
		public virtual void OnPointerDown(BaseEventData data)
		{
			if (data !=null && data is PointerEventData)
			{
				//左クリック入力のみ
				if((data as PointerEventData).button != PointerEventData.InputButton.Left) return;
			}

			OnInput(data);
		}

		/// <summary>
		/// クリックなどの入力があったとき（キーボード入力による文字送りなどを拡張するときに）
		/// </summary>
		public virtual void OnInput(BaseEventData data = null)
		{
			switch (Status)
			{
				case UiStatus.Backlog:
					break;
				case UiStatus.HideMessageWindow:    //メッセージウィンドウが非表示
					Status = UiStatus.Default;
					break;
				case UiStatus.Default:
					if (Engine.Config.IsSkip)
					{
						//スキップ中ならスキップ解除
						Engine.Config.ToggleSkip();
					}
					else
					{
						if (IsShowingMessageWindow)
						{
							if (!Engine.Config.IsSkip)
							{
								//文字送り
								Engine.Page.InputSendMessage();
							}
						}
						if (data != null && data is PointerEventData)
						{
							base.OnPointerDown(data as PointerEventData);
						}
					}
					break;
			}
		}
	}
}