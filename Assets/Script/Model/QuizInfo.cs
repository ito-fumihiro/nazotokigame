using System.Collections.Generic;

namespace Assets.Script.Model
{
	/// <summary>
	/// アイテムの情報を格納するモデル
	/// </summary>
	public class QuizInfo
	{
		/// <summary>
		/// ID
		/// </summary>
		public string Id;

		/// <summary>
		/// 質問オブジェクトのファイルパス
		/// </summary>
		public string QuestionObjectFilePath;

		/// <summary>
		/// 正解
		/// </summary>
		public string CorrectAnswer;

		/// <summary>
		/// 取得アイテムID
		/// </summary>
		public string GetItemId;

		/// <summary>
		/// ヒントのシナリオラベルリスト
		/// </summary>
		public List<string> HintScenarioLabelList;

		/// <summary>
		/// 開始前のシナリオラベル
		/// </summary>
		public string BeforeScenarioLabel;

		/// <summary>
		/// 終了後のシナリオラベル
		/// </summary>
		public string EndScenarioLabel;

	}
}