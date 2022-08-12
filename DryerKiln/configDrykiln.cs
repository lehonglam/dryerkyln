using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdin20.tdinCode
{
    class configDrykiln : config
    {
        public void setUpDataBase()
        {

            string sql = @"CREATE TABLE [dbo].[CheDoSay] (
                                [Id]          INT           IDENTITY (1, 1) NOT NULL,
                                [Ten]         NVARCHAR (50) NULL,
                                [DoDayGoSay]  FLOAT (53)    NULL,
                                [DoAmDauVao]  FLOAT (53)    NULL,
                                [DoAmDauRa]   FLOAT (53)    NULL,
                                [CheDoSay]    INT           NULL,
                                [NhomGo]      INT           NULL,
                                [NgayKhoiTao] DATETIME      NULL,
                                [TacGia]      NVARCHAR (50) NULL,
                                [TgGd0]       FLOAT (53)    NULL,
                                [TgGd1]       FLOAT (53)    NULL,
                                [TgGd2]       FLOAT (53)    NULL,
                                [TgGd3]       FLOAT (53)    NULL,
                                [TgGd4]       FLOAT (53)    NULL,
                                [TgGd5]       FLOAT (53)    NULL,
                                [TgGd6]       FLOAT (53)    NULL,
                                [NhietDoGd0]  FLOAT (53)    NULL,
                                [NhietDoGd1]  FLOAT (53)    NULL,
                                [NhietDoGd2]  FLOAT (53)    NULL,
                                [NhietDoGd3]  FLOAT (53)    NULL,
                                [NhietDoGd4]  FLOAT (53)    NULL,
                                [NhietDoGd5]  FLOAT (53)    NULL,
                                [NhietDoGd6]  FLOAT (53)    NULL,
                                [DoMoVenGd0]  FLOAT (53)    NULL,
                                [DoMoVenGd1]  FLOAT (53)    NULL,
                                [DoMoVenGd2]  FLOAT (53)    NULL,
                                [DoMoVenGd3]  FLOAT (53)    NULL,
                                [DoMoVenGd4]  FLOAT (53)    NULL,
                                [DoMoVenGd5]  FLOAT (53)    NULL,
                                [DoMoVenGd6]  FLOAT (53)    NULL,
                                [DoAmGd0]     FLOAT (53)    NULL,
                                [DoAmGd1]     FLOAT (53)    NULL,
                                [DoAmGd2]     FLOAT (53)    NULL,
                                [DoAmGd3]     FLOAT (53)    NULL,
                                [DoAmGd4]     FLOAT (53)    NULL,
                                [DoAmGd5]     FLOAT (53)    NULL,
                                [DoAmGd6]     FLOAT (53)    NULL,
                                [ghichu]      NTEXT NULL,
                                PRIMARY KEY CLUSTERED ([Id] ASC)
                            );";
            AccessDatabase ac = new AccessDatabase();
            ac.ExeCuteNoQeury(sql);
        }
    }
}
