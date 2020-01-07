using System.Drawing;
using DotFeather;
using Newtonsoft.Json.Linq;
using TakeUpJewel.Data;
using TakeUpJewel.Util;
using static TakeUpJewel.Util.DevelopmentUtility;

namespace TakeUpJewel.Entities
{
    [EntityRegistry("Coin", 28)]
    public class EntityCoin : EntitySprite
    {
        private bool _animating;
        private WorkingType _worktype = WorkingType.Normal;

        public EntityCoin(Vector pnt, Object[] obj, byte[,,] chips, EntityList par)
        {
            Location = pnt;
            Mpts = obj;
            Map = chips;
            Parent = par;
            Size = new Size(16, 16);
            SetAnime(6, 9, 8);

            Velocity.Y = -12;
        }

        public override Texture2D[] ImageHandle => ResourceUtility.Item;


        public override EntityGroup MyGroup => EntityGroup.Stage;
        public static bool tutorialRead { get; set; }

        public override Entity SetEntityData(dynamic jsonobj)
        {
            if (jsonobj.IsDefined("WorkingType"))
                _worktype = (WorkingType)jsonobj.WorkingType;
            base.SetEntityData((object)jsonobj);
            if (_worktype == WorkingType.FromBlock)
                AnimeSpeed = 3;
            return this;
        }

        public override void OnUpdate()
        {
            if (!_animating)
                switch (_worktype)
                {
                    case WorkingType.Normal:
                        Velocity.Y = 0;
                        foreach (var entity in Parent.FindEntitiesByType<EntityPlayer>())
                        {
                            var ep = (EntityPlayer)entity;
                            if (!ep.IsDying && new RectangleF(ep.Location.ToPoint(), ep.Size).CheckCollision(new RectangleF(Location.ToPoint(), Size)))
                                _animating = true;
                        }
                        break;
                    case WorkingType.FromBlock:
                        _animating = true;
                        SoundUtility.PlaySound(Sounds.GetCoin);
                        Game.I.Coin++;
                        if (!tutorialRead)
                        {
                            EventRuntime.AddScript(GenerateItemDescription("トランジスタコイン\nトランジスタ王国の　通貨。",
                                "50枚　集めると　残機が　1だけ　増える。"));
                            tutorialRead = true;
                        }
                        break;
                }

            if (_animating)
                switch (_worktype)
                {
                    case WorkingType.Normal:
                        IsDead = true;
                        Parent.Add(Game.I.EntityRegister.CreateEntity("Coin", Location, Mpts, Map, Parent,
                            JObject.Parse(@"{""WorkingType"": 1}")));
                        break;
                    case WorkingType.FromBlock:
                        Velocity.Y *= 0.8f;
                        if (Velocity.Y > -0.1f)
                            Kill();
                        break;
                }

            base.OnUpdate();
        }
    }
}