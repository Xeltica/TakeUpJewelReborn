﻿using System.Drawing;
using DotFeather;
using TakeUpJewel.AI;
using TakeUpJewel.Data;
using TakeUpJewel.Util;

namespace TakeUpJewel.Entities
{
    [EntityRegistry("Daemon", 3)]
    public class EntityDaemon : EntityLiving
    {
        public EntityDaemon(Vector pnt, Object[] obj, byte[,,] chips, EntityList par)
        {
            Location = pnt;
            Mpts = obj;
            Map = chips;
            Parent = par;
            Size = new Size(16, 16);
            MainAi = new AiWalk(this, 1, 0, 3, 7, 10);

            CollisionAIs.Add(new AiKillDefender(this));
        }

        public override Texture2D[] ImageHandle => ResourceUtility.Daemon;


        public override EntityGroup MyGroup => EntityGroup.Enemy;

        public override RectangleF Collision => new RectangleF(2, 2, 12, 14);

        public override void OnUpdate()
        {
            base.OnUpdate();
        }


        public override void SetKilledAnime()
        {
            SetAnime(5, 6, 15);
            LoopTimes = 0;
        }

        public override void SetCrushedAnime()
        {
            SetGraphic(4);
        }
    }
}