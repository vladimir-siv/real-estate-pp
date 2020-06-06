using System;
using System.Collections.Generic;

namespace repplib
{
	public static class AppContext
	{
		private static Dictionary<Type, object> TypedContext = new Dictionary<Type, object>();
		private static Dictionary<string, object> NamedContext = new Dictionary<string, object>();

		public static void Inject<T>(T obj) => TypedContext.Add(typeof(T), obj);
		public static T Resolve<T>() => (T)TypedContext[typeof(T)];

		public static void Inject(string name, object obj) => NamedContext.Add(name, obj);
		public static object Resolve(string name) => NamedContext[name];
		public static T Resolve<T>(string name) => (T)NamedContext[name];
	}
}
