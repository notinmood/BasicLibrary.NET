1、DirtyWord.txt 是系统缺省提供的脏词列表，项目使用时可以直接拷贝过去。
2、使用方法
		2.1、创建过滤器，并载入脏词字典
		HashFilter hash = new HashFilter();
	    using (StreamReader sw = new StreamReader(File.OpenRead(Server.MapPath("~/脏词过滤测试.txt"))))
		{
			string key = sw.ReadLine();
			while (key != null)
			{
				if (key != string.Empty)
				{
					hash.AddKey(key);
				}
				key = sw.ReadLine();
			}
		}

		2.2、调用过滤器内的方法对目标文本进行操作
		string targetString = string.Format(@"钓鱼岛是中国的，日本滚出去");
		string result = hash.Replace(targetString);
        