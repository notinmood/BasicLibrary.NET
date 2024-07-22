首先说明如何使用此类。

为想要保存在Cookies中的类建立模型，并且继承自CookieInfo即可。比如下面建立了MyCookieInfo类，其中包含属性 pkid，TestValue和TestDateTime：

    /// <summary>
    /// 保存Cookies的数据对象
    /// </summary>
    [System.Serializable]
    public class MyCookieInfo : CookieInfo
    {    
        private int m_Pkid = 0; 
        public int Pkid
        {
            get
            {
                return m_Pkid ;
            }
            set
            {
                m_Pkid = value ;
            }
        }

        private string m_TestValue = "";
        public string TestValue
        {
            get
            {
                return m_TestValue;
            }
            set
            {
                m_TestValue = value;
            }
        }

        private DateTime m_TestDateTime = DateTime.Now;
        public DateTime TestDateTime
        {
            get
            {
                return m_TestDateTime;
            }
            set
            {
                m_TestDateTime = value;
            }
        }
    }

 

接下来就可以使用对象的Save和Load方法保存和读取Cookies：

    保存
    Save方法有两个重载，不带参数的Save方法表示Cookies的过期时间与浏览器相同，即浏览器关闭则Cookies消失。 否则需要传入Cookies过期时间。

MyCookieInfo testCookies = new MyCookieInfo();
    testCookies.Pkid = 1;
    testCookies.TestValue = "中文测试";
    testCookies.Save(); 

    读取

    MyCookieInfo testCookies = new MyCookieInfo();
    testCookies.Load();
    this.lblMsg.Text = "Pkid:" + testCookies.Pkid.ToString();
    this.lblMsg.Text += ",TestValue:" + testCookies.TestValue.ToString();
    this.lblMsg.Text += ",TestDateTime:" + 
    testCookies.TestDateTime.ToString("yyyy/MM/dd HH:mm:ss", 
    System.Globalization.DateTimeFormatInfo.InvariantInfo);

现在我们已经可以将一个强类型的对象读取和保存Cookies了。