using System;
using System.Collections.Generic;
using IFramework;
using System.IO;
using IFramework.Modules.NodeAction;
using System.Drawing;
using System.Drawing.Imaging;
using IFramework.Modules.Message;
using FormSever.Net;

namespace FormSever.Game
{
    public class GameColum
    {
        public int posX;
        public int posY;
        public string color = "FFFFFFFF";
    }
    public class GameMap:IMessagePublisher
    {
        private const int mapSize = 100;
        private GameColum[,] map;
        private static string path = Data.Datas.root.CombinePath("Gamemap.png");

        private LockParam _lock=new LockParam();
        public void Load()
        {
            if (!File.Exists(path))
                Save();
            Bitmap bitmap = Bitmap.FromFile(path) as Bitmap;
            Bitmap nbt = new Bitmap(bitmap, new Size(mapSize, mapSize));
            map = new GameColum[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Color col = nbt.GetPixel( j, mapSize - 1 - i);
                    string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", col.R, col.G, col.B, col.A);
                    map[i, j] = new GameColum()
                    {
                        posX = i,
                        posY = j,
                        color = hex
                    };
                }
            }
            bitmap.Dispose();

            nbt.Dispose();
        }
        private Bitmap Get()
        {
            Bitmap bitmap = new Bitmap(mapSize, mapSize);
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    string hex = map[i, j].color;
                    if (hex.Length != 8) throw new System.Exception("Parse Err Color");
                    byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                    byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

                    bitmap.SetPixel(j, mapSize - 1 - i, Color.FromArgb(cc, br, bg, bb));
                }
            }
            return bitmap;
        }
        public void Save()
        {
            using (new LockWait(ref _lock))
            {
                if (map == null)
                {
                    map = new GameColum[mapSize, mapSize];
                    for (int i = 0; i < mapSize; i++)
                    {
                        for (int j = 0; j < mapSize; j++)
                        {
                            map[i, j] = new GameColum() { posX = i, posY = j };
                        }
                    }
                }
                Bitmap bitmap = Get();

                bitmap.Save(path, ImageFormat.Png);
                APP.message.Publish(this, 0, null, bitmap);

                bitmap.Dispose();
            }
        }
        public GameMap()
        {
            this.Sequence(APP.env)
                .Repeat((r) =>
                {
                    r.Sequence((s) =>
                    {
                        s.TimeSpan(TimeSpan.FromMinutes(5))
                        .Event(() =>
                        {
                            if (APP.form.Picture.Image != null)
                            {
                                APP.form.Picture.Image.Dispose();
                            }

                            APP.form.Picture.Image = Get();

                            BroadCastMap((resp) =>
                            {
                                APP.net.SendTcpMessageToAll(resp);

                            });

                        });
                    });
                }, -1).Run();
        }

        private List<Point2> GetColumns(Point2 po,int size)
        {
            List<Point2> cols = new List<Point2>();
            int distanse = size / 2;
            for (int i = -distanse; i < distanse; i++)
            {
                for (int j = -distanse; j < distanse; j++)
                {
                    Point2 pos = po + new Point2(i, j);
                    if (pos.x < 0 || pos.y < 0 || pos.x >= mapSize || pos.y >= mapSize)
                        continue;
                    if (Point2.Distance(po, pos) < distanse)
                    {
                        cols.Add(pos);
                    }
                }
            }
            return cols;
        }
        public bool WriteColumn(int posX, int posy, string color,int size)
        {
            using (new LockWait(ref _lock))
            {
                var pos = GetColumns(new Point2(posX, posy), size);
                for (int i = 0; i < pos.Count; i++)
                {
                    map[(int)pos[i].x, (int)pos[i].y].color = color;

                }
                return true;
            }

        }

        private GameColum[] GetColums()
        {
            List<GameColum> cs = new List<GameColum>();
            using (new LockWait(ref _lock))
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        cs.Add(map[i, j]);
                    }
                }
            }
            return cs.ToArray();

        }


        internal void BroadCastMap(Action<PanResponse> pan)
        {
            List<GameColum> colums = new List<GameColum>();
            var cols = APP.gameMap.GetColums();
            int offset = 0;
            while (cols.Length > offset)
            {
                var c = cols[offset];
                colums.Add(c);
                offset++;
                if (colums.Count >= 2000 || offset > cols.Length)
                {
                    pan?.Invoke(new PanResponse()
                    {
                        colums = colums
                    });
                    colums.Clear();
                }
            }
        }
    }
}
