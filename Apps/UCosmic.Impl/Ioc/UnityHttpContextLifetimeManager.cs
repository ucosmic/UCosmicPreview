using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace UCosmic
{
    public class UnityHttpContextLifetimeManager : LifetimeManager
    {
        private const string KeyFormat = "SingletonPerCallContext_{0}";
        private readonly string _key;

        public UnityHttpContextLifetimeManager()
        {
            _key = string.Format(KeyFormat, Guid.NewGuid());
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[_key];
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_key] = newValue;
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(_key);
        }
    }
}