using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DotFeather;

namespace TakeUpJewel
{
	public class JukeboxScene : Scene
	{
		public override void OnStart(Router router, GameBase game, Dictionary<string, object> args)
		{
			Root.Add(new Sprite(ResourceManager.LoadTexture("bgjukebox.png")));
			Core.I.BgmStop();
			menuItems = AudioList.Select((a, i) => new DEText("　" + a, Color.White)
			{
				Location = new Vector(32, 32 + i * 12),
			}).ToArray();

			Root.Add(new DEText("ジュークボックス", Color.White)
			{
				Location = new Vector(16, 16),
			});

			foreach (var item in menuItems)
				Root.Add(item);
		}

		public override void OnUpdate(Router router, GameBase game, DFEventArgs e)
		{
			foreach (var (item, i) in menuItems.Select((item, index) => (item, index)))
			{
				if (selectedIndex == i)
				{
					item.Color = Color.Yellow;
					item.Text = "♪" + item.Text.Substring(1);
				}
				else
				{
					item.Color = Color.White;
					item.Text = "　" + item.Text.Substring(1);
				}
			}

			if (DFKeyboard.Up.IsKeyDown)
				selectedIndex--;
			if (DFKeyboard.Down.IsKeyDown)
				selectedIndex++;
			if (selectedIndex < 0)
				selectedIndex = menuItems.Length - 1;
			if (selectedIndex > menuItems.Length - 1)
				selectedIndex = 0;
			if (DFKeyboard.Z.IsKeyDown)
			{
				if (selectedIndex == menuItems.Length - 1)
				{
					DESound.Play(Sounds.Back);
					Core.I.BgmStop();
					router.ChangeScene<TitleScene>();
				}
				else
				{
					Core.I.BgmPlay(AudioFileList[selectedIndex]);
				}
			}
			if (DFKeyboard.X.IsKeyDown)
			{
				Core.I.BgmStop(0);
			}
		}

		private int selectedIndex = 0;

		private DEText[] menuItems;

		private static readonly string[] AudioList =
		{
			"レジスタ街",
			"シーケンシャル平野",
			"エンカプセル洞窟",
			"リファクタ山",
			"ゴール!",
			"スパイウェだんの　アジト",
			"やられた!",
			"ゲームオーバー",
			"せんとう! クイーン",
			"せんとう! キング",
			"勝利!",
			"Vサイン",
			"エンディング",
			"(もどる)"
		};

		private static readonly string[] AudioFileList =
		{
			"registertown.mid",
			"hometownv2.mid",
			"流れる水の働き.mid",
			"c006.mid",
			"jingle_gameclear.mid",
			"team-spyway.mid",
			"zannnenn.mid",
			"IcyHeart.mid",
			"icypulse_rec.mid",
			"c014.mid",
			"fanfare.mid",
			"omedeto.mid",
			"草原をゆくv5.mid",
		};
	}
}