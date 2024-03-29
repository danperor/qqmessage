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
    ///  * 用来发送验证消息
    /// * 1. 头部
    /// * 2. 子命令，1字节
    /// * 3. 要添加的QQ号，4字节
    /// * 4. 是否允许对方加自己为好友，1字节
    /// * 5. 把好友加到第几组，我的好友组是0，然后以此类推，1字节
    /// * 6. 验证消息字节长度，1字节
    /// * 7. 验证消息
    /// * 8. 尾部
    /// 	<remark>abu 2008-02-28 </remark>
    /// </summary>
    public class AuthorizePacket : BasicOutPacket
    {
        public byte SubCommand { get; set; }
        public int To { get; set; }
        public RevenseAdd ReverseAdd { get; set; }
        public int DestGroup { get; set; }
        public string Message { get; set; }
        public AuthorizePacket(QQUser user)
            : base(QQCommand.Authorize, true, user)
        {
            SubCommand = 0x02;
            ReverseAdd = RevenseAdd.Allow;
            DestGroup = 0;
            Message = string.Empty;
        }
        public AuthorizePacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        protected override void PutBody(ByteBuffer buf)
        {
            buf.Put(SubCommand);
            buf.PutInt(To);
            buf.Put((byte)ReverseAdd);
            buf.Put((byte)DestGroup);
            byte[] b = Utils.Util.GetBytes(Message);
            buf.Put((byte)b.Length);
            buf.Put(b);
        }
    }
}
