using System.Reflection;
using FluentValidation;

namespace UCosmic.Impl
{
    // ReSharper disable UnusedMember.Global
    public class UnityValidatorFactory : FluentValidatorFactory
    // ReSharper restore UnusedMember.Global
    {
        public UnityValidatorFactory(UnityDependencyInjector injector)
            : base(injector)
        {
            var validators = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetCallingAssembly());
            validators.ForEach(validator => injector.Container.RegisterType(validator.InterfaceType, validator.ValidatorType, null, null));
        }
    }
}
