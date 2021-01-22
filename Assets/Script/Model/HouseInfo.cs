namespace Assets.Script.Model
{
    /// <summary>
    /// 探索パートのハウス情報を格納するモデル
    /// </summary>
    public class HouseInfo
	{
		/// <summary>
		/// ID
		/// </summary>
		public string Id;

		/// <summary>
		/// 開始前のシナリオラベル
		/// </summary>
		public string BeforeScenarioLabel;

		/// <summary>
		/// 終了後のシナリオラベル
		/// </summary>
		public string EndScenarioLabel;

		/// <summary>
		/// クイズID
		/// </summary>
		public string QuizId;

		/// <summary>
		/// ハウスオブジェクトのファイルパス
		/// </summary>
		public string HouseObjectFilePath;

	}
}