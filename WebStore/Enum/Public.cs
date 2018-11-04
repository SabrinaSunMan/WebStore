using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public enum DataAction
    {
        /// <summary>
        /// 新增
        /// </summary>
        Create = 0,

        /// <summary>
        /// 建立成功
        /// </summary>
        [Description("建立成功")]
        CreateScuess = 1,

        /// <summary>
        /// 建立失敗
        /// </summary>
        [Description("建立失敗")]
        CreateFail = 2,

        /// <summary>
        /// 建立失敗
        /// </summary>
        [Description("已有重複資料，請重新輸入")]
        CreateFailReapet = 3,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 4,

        /// <summary>
        /// 更新成功
        /// </summary>
        [Description("更新成功")]
        UpdateScuess = 5,

        /// <summary>
        /// 更新失敗
        /// </summary>
        [Description("更新失敗")]
        UpdateFail = 6,

        Read = 7,

        /// <summary>
        /// 刪除
        /// </summary>
        Delete = 8,

        /// <summary>
        /// 刪除成功
        /// </summary>
        [Description("刪除成功")]
        DeleteScuess = 5,

        /// <summary>
        /// 刪除失敗
        /// </summary>
        [Description("刪除失敗")]
        DeleteFail = 6
    }

    public enum TableName
    {
        /// <summary>
        /// 產品
        /// </summary>
        [Description("產品")]
        Product = 0
    }
}