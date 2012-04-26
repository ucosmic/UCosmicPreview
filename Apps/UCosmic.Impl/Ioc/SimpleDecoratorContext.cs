using System;
using System.Linq.Expressions;

namespace UCosmic.Impl
{
    public class SimpleDecoratorContext
    {
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
        public Type[] AppliedDecorators { get; set; }
        public Expression Expression { get; set; }
    }
}