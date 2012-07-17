using System.Reflection;
using FluentValidation;

namespace UCosmic.Impl
{
    public class UnityValidatorFactory : FluentValidatorFactory
    {
        public UnityValidatorFactory(UnityServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            var validators = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetCallingAssembly());
            validators.ForEach(validator => serviceProvider.Container.RegisterType(validator.InterfaceType, validator.ValidatorType, null, null));
        }
    }
}
