using MauiBoilerplate.Core.Entities;
using System;

namespace MauiBoilerplate.EntityFrameworkCore.Seed
{
    internal class InitialDbBuilder
    {
        private MauiBoilerplateDbContext context;

        public InitialDbBuilder(MauiBoilerplateDbContext context)
        {
            this.context = context;
        }

        internal void Create()
        {
            if (!this.context.Song.Any(c => c.MusicTitle=="不顾一切的爱"))
            {
                this.context.Add(new Song()
                {
                    MusicTitle="不顾一切的爱",
                    Artist="李圣杰",
                    Album="手放开",
                    Duration=new TimeSpan(0, 4, 27),
                    ReleaseDate=new DateTime(2004, 1, 1)
                });

            }
            if (!this.context.Song.Any(c => c.MusicTitle=="阴天快乐"))
            {
                this.context.Add(new Song()
                {
                    MusicTitle="阴天快乐",
                    Artist="陈奕迅",
                    Album="米.闪",
                    Duration=new TimeSpan(0, 3, 25),
                    ReleaseDate=new DateTime(2014, 1, 1)
                });

            }

            if (!this.context.Song.Any(c => c.MusicTitle=="可惜不是你"))
            {
                this.context.Add(new Song()
                {
                    MusicTitle="可惜不是你",
                    Artist="梁静茹",
                    Album="通往爱的路途",
                    Duration=new TimeSpan(0, 4, 45),
                    ReleaseDate=new DateTime(2005, 1, 1)
                });

            }

            if (!this.context.Song.Any(c => c.MusicTitle=="喜欢你"))
            {
                this.context.Add(new Song()
                {
                    MusicTitle="喜欢你",
                    Artist="G.E.M.邓紫棋",
                    Album="喜欢你",
                    Duration=new TimeSpan(0, 3, 55),
                    ReleaseDate=new DateTime(2014, 1, 1)
                });

            }

        }
    }
}