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

namespace QQ.NET.Entities
{
    /// <summary> 临时会话消息
    /// 	<remark>abu 2008-02-25 </remark>
    /// </summary>
    public class TempSessionIM
    {
        public int Sender { get; set; }
        public string Nick { get; set; }
        public string Site { get; set; }
        public string Message { get; set; }
        public long Time { get; set; }
        public FontStyle FontStyle { get; set; }

        /// <summary>
        /// 	<remark>abu 2008-02-25 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        public void Read(ByteBuffer buf)
        {
            // 发送者
            Sender = buf.GetInt();
            // 未知的4字节
            buf.GetInt();
            // 昵称
            int len = buf.Get() & 0xFF;
            Nick = Utils.Util.GetString(buf, len);
            // 群名称
            len = buf.Get() & 0xFF;
            Site = Utils.Util.GetString(buf, len);
            // 未知的1字节
            buf.Get();
            // 时间
            Time = (long)buf.GetInt() * 1000L;
            // 后面的内容长度
            len = buf.GetUShort();
            // 得到字体属性长度，然后得到消息内容
            int fontStyleLength = buf.Get(buf.Position + len - 1) & 0xFF;
            Message = Utils.Util.GetString(buf, len - fontStyleLength);
            // 字体属性
            FontStyle = new FontStyle();
            FontStyle.Read(buf);
        }
    }
}
