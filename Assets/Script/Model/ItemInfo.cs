using Assets.Script.Enum;
using UnityEngine;

namespace Assets.Script.Model
{
	/// <summary>
	/// アイテムの情報を格納するモデル
	/// </summary>
	public class ItemInfo
	{
		/// <summary>
		/// ID
		/// </summary>
		public string Id;

		/// <summary>
		/// アイテムタイプ
		/// </summary>
		public ItemType Type;

		/// <summary>
		/// アイテム名
		/// </summary>
		public string Name;

		/// <summary>
		/// 詳細
		/// </summary>
		public string Description;

		/// <summary>
		/// オリジナル画像パス
		/// </summary>
		public string OrgFilePath;

		/// <summary>
		/// サムネイル画像パス
		/// </summary>
		public string ThmFilePath;

		/// <summary>
		/// オリジナル画像のスプライト
		/// </summary>
		public Sprite SpOrg;

		/// <summary>
		/// サムネイル画像のスプライト
		/// </summary>
		public Sprite SpThm;
		
	}
}