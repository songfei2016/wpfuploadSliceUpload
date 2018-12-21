namespace St.Common
{
    public static class Messages
    {
        public const string WarningUserCenterIpNotConfigured = "服务器地址未配置！";
        public const string WarningUserNameEmpty = "用户名不能为空！";
        public const string WarningPasswordEmpty = "密码不能为空！";
        public const string WarningAuthenticationFailure = "登录失败！";
        public const string WarningDeviceNotRegistered = "设备未注册！";
        public const string InfoLogging = "正在登录中，请稍后......";
        public const string ErrorLoginFailed = "登录失败！";
        public const string WarningLoginTimeout = "登录超时！";
        public const string WarningRefreshTokenFailed = "重新自动获取令牌失败！";
        public const string WarningDeviceExpires = "设备已过期！";
        public const string WarningEmptyDevice = "获取设备信息失败！";
        public const string WarningLockedDevice = "设备已经被锁定！";
        public const string WarningYouAreSignedOut = "请重新登录。可能的原因：\r\n1.您的账号已在别的设备上登录。\r\n2.登录已过期。";

        public const string WarningMeetingServerAlreadyStarted = "服务已经是启动状态！";
        public const string WarningMeetingServerNotStarted = "服务未启动！";

        //文件上传相关
        public const string WarningSelectFileFirst = "请选择文件！";
        public const string WarningUploadFail = "上传中断，请检查网络后继续上传！";
        public const string UploadFailThenTry3Time = "上传中断，将自动重试3次！";
        public const string ApplyUploadError = "申请上传失败，请重新登录，如果仍然失败，请联系管理员！";
    }
}
