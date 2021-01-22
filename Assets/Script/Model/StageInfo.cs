namespace Assets.Script.Model
{
    /// <summary>
    /// ステージ情報のモデル
    /// </summary>
    public class StageInfo
	{
		/// <summary>
		/// ID
		/// </summary>
		public string Id;

		/// <summary>
		/// ステージオブジェクトのファイルパス
		/// </summary>
		public string StageObjectFilePath;

		/// <summary>
		/// プロローグのシナリオラベル
		/// </summary>
		public string PrologueScenarioLabel;

		/// <summary>
		/// エピローグのシナリオラベル
		/// </summary>
		public string EpilogueScenarioLabel;

	}
}