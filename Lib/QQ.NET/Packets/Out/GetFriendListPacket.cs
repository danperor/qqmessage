﻿#region 版权声明
/**
 * 版权声明：QQ.NET是基于LumaQQ分析的QQ协议，将其部分代码进行修改和翻译为.NET版本，并且继续使用LumaQQ的开源协议。
 * 本人没有对其核心协议进行改动， 也没有与腾讯公司的QQ软件有直接联系，请尊重LumaQQ作者Luma的著作权和版权声明。
 * 
 * 作者：阿不
 * 博客：http://hjf1223.cnblogs.com
 * Email：hjf1223@gmail.com
 * LumaQQ：http://lumaqq.linuxsir.org 
 * LumaQQ - Java QQ Client
 * 
 * Copyright (C) 2004 luma <stubma@163.com>
 * 
 * LumaQQ - For .NET QQClient
 * Copyright (C) 2008 阿不<hjf1223@gmail.com>
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace QQ.NET.Packets.Out
{
    /// <summary>
    ///  * 请求得到好友列表的包，其内容为
    /// * 1. 头部
    /// * 2. 两个字节的起始好友列表返回位置，注意这个起始位置是排序之后的
    /// *    加入你有9个好友，号码从10到100每隔10位一个，那么如果这两个数
    /// *    字的值指定为3，那10, 20, 30就不会返回了
    /// * 3. 返回的好友列表是否排序，1个字节，非0则排序
    /// * 4. 2个未知字节
    /// * 5. 尾部
    /// 	<remark>abu 2008-02-29 </remark>
    /// </summary>
    public class GetFriendListPacket : BasicOutPacket
    {
        /// <summary>好友列表开始位置，缺省是0
        /// 	<remark>abu 2008-02-29 </remark>
        /// </summary>
        /// <value></value>
        public ushort StartPosition { get; set; }
        public GetFriendListPacket(QQUser user) : base(QQCommand.Get_Friend_List, true, user) {
            StartPosition = QQGlobal.QQ_POSITION_FRIEND_LIST_START;
        }
        public GetFriendListPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Get Friend List Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            buf.PutUShort(StartPosition);
            buf.Put((byte)FriendListSort.Unsorted);
            buf.Put((byte)0);
            buf.Put((byte)1);
        }
    }
}
