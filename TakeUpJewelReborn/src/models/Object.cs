using DotFeather;

namespace TakeUpJewel
{
	/// <summary>
	/// mpt の最小単位を表します。
	/// </summary>
	public class Tile
	{
		public Tile(Texture2D handle, byte[,] mask)
		{
			ImageHandle = handle;
			HitMask = mask;
		}

		public Texture2D ImageHandle { get; set; }

		/// <summary>
		/// オブジェクトの当たり判定をビットマップで指定します。
		/// ～記法～
		/// 0...当たらない
		/// 1...当たる
		/// 2...当たるとダメージ
		/// 3...当たると即死
		/// 4...当たると水中
		/// </summary>
		public byte[,] HitMask { get; set; }

		public ColliderType CheckHit(int x, int y)
		{
			if (x < 0 || y < 0 || x >= HitMask.GetLength(0) || y >= HitMask.GetLength(1))
				return ColliderType.Air;
			return (ColliderType)HitMask[x, y];
		}
	}
}