﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Enum
{
    /// <summary>
    /// 普遍性使用頁數
    /// </summary>
    ///
    public enum BackPageListSize
    {
        /// <summary>
        /// The common size
        /// </summary>
        commonSize = 5
    }

    /// <summary>
    /// 對於資料使用的動作
    /// </summary>
    public enum Actions
    {
        /// <summary>
        /// 對於資料使用的動作_新增
        /// </summary>
        Create = 0,

        /// <summary>
        /// 對於資料使用的動作_修改
        /// </summary>
        Update = 1,

        /// <summary>
        /// 對於資料使用的動作_讀取
        /// </summary>
        Read = 2,

        /// <summary>
        /// 對於資料使用的動作_刪除
        /// </summary>
        Delete = 3
    }

    public enum LogLevel
    {
        /// <summary>
        /// 嚴重錯誤
        /// </summary>
        Error = 0
    }
}