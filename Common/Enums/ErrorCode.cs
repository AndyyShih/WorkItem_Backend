using System.ComponentModel;

namespace Common.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// 請求 API 缺少或遺漏必要參數 ErrorCode:1
        /// </summary>
        [Description("請求 API 缺少或遺漏必要參數")]
        ERROR_REQUEST_MISSING_PARAMETERS = 1,
        /// <summary>
        /// 會員的使用者權杖不合法 ErrorCode:2
        /// </summary>
        [Description("會員的使用者權杖不合法")]
        ERROR_MEMBER_USERTOKEN_ILLEGAL = 2,
        /// <summary>
        /// 會員的使用者權杖時效已經過期 ErrorCode:3
        /// </summary>
        [Description("會員的使用者權杖時效已經過期")]
        ERROR_MEMBER_USERTOKEN_EXPIRY = 3,
        /// <summary>
        /// 系統發生例外錯誤 ErrorCode:4
        /// </summary>
        [Description("系統發生例外錯誤")]
        ERROR_SYSTEM_EXCEPTION = 4,
        /// <summary>
        /// 資料庫刪除資料失敗 ErrorCode:5
        /// </summary>
        [Description("資料庫刪除資料失敗")]
        ERROR_SQLSERVER_DELETE_FAILED = 5,
        /// <summary>
        /// 資料庫新增或更新資料失敗 ErrorCode:6
        /// </summary>
        [Description("資料庫新增或更新資料失敗")]
        ERROR_SQLSERVER_UPDATE_FAILED = 6,
        /// <summary>
        /// 登入認證發生錯誤 ErrorCode:7
        /// </summary>
        [Description("登入認證發生錯誤")]
        WARNING_LOGIN_VERIFICATION_FAILED = 7,
        /// <summary>
        /// 帳號重複 ErrorCode:8
        /// </summary>
        [Description("帳號重複")]
        WARNING_ACCOUNT_DUPLICATE = 8,
        /// <summary>
        /// 請求參數格式錯誤 ErrorCode:9
        /// </summary>
        [Description("請求參數格式錯誤")]
        ERROR_REQUEST_PARAMETERS_FORMAT = 9,
        /// <summary>
        /// 查無資料
        /// </summary>
        [Description("查無資料")]
        DATA_EMPTY = 0
    }
}
