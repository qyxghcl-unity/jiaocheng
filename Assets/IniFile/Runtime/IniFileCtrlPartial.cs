public partial class IniFileCtrl
{
#region KinectManager
    private string 最小检测距离;
    public string _最小检测距离
    {
        get
        {
            if (最小检测距离 == null)
            {
                最小检测距离 = GetString("KinectManager", "最小检测距离", "");
            }
            return 最小检测距离;
        }
        set { GetString("KinectManager", "最小检测距离", ""); }
    }
    private string 最大检测距离;
    public string _最大检测距离
    {
        get
        {
            if (最大检测距离 == null)
            {
                最大检测距离 = GetString("KinectManager", "最大检测距离", "");
            }
            return 最大检测距离;
        }
        set { GetString("KinectManager", "最大检测距离", ""); }
    }
#endregion
#region Video
    private string 音频;
    public string _音频
    {
        get
        {
            if (音频 == null)
            {
                音频 = GetString("Video", "音频", "");
            }
            return 音频;
        }
        set { GetString("Video", "音频", ""); }
    }
    private string 静音;
    public string _静音
    {
        get
        {
            if (静音 == null)
            {
                静音 = GetString("Video", "静音", "");
            }
            return 静音;
        }
        set { GetString("Video", "静音", ""); }
    }
#endregion
}
