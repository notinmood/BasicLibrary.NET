
一,在定义类型的时候,内部指定一个有动态代理生成的Empty静态属性
	public class Student
	{
		private int age = 0;
		public virtual int Age
		{
			get { return this.age; }
			set { this.age = value; }
		}

		public virtual int GetAge()
		{
			return this.age;
		}

		public virtual void HelloWorld()
		{
			Console.WriteLine("Hello World.");
		}

		private bool isEmpty = false;
		public bool IsEmpty
		{
			get { return this.isEmpty; }
		}

		public static Student Empty
		{
			get
			{
				ProxyGenerator proxy = new ProxyGenerator();
				Student empty = proxy.CreateClassProxy<Student>(new EmptyObjectInterceptor());
				empty.isEmpty = true;

				return empty;
			}
		}
	}

二.使用时
		Student studentEmpty = Student.Empty;
		   
		int age = studentEmpty.GetAge();
		Console.WriteLine(age);
		studentEmpty.HelloWorld();
		Console.ReadLine();

三.如何判断一个对象是空对象还是实际对象呢?

		我们的对象有一个IsEmpty属性可以使用