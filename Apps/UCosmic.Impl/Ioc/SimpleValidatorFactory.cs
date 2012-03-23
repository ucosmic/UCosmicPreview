using System.Reflection;
using FluentValidation;
using SimpleInjector.Extensions;

namespace UCosmic
{
    public class SimpleValidatorFactory : FluentValidatorFactory
    {
        public SimpleValidatorFactory(SimpleDependencyInjector injector) : base(injector)
        {
            var validators = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetCallingAssembly());
            validators.ForEach(validator => injector.Container.Register(validator.InterfaceType, validator.ValidatorType));
            injector.Container.Verify();
        }
    }
}
