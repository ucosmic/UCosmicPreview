using System;
using FluentValidation;
using System.Reflection;
namespace UCosmic
{
    public class UnityValidatorFactory : ValidatorFactoryBase
    {
        private readonly IInjectDependencies _dependencyInjector;

        public UnityValidatorFactory()
        {
            var unity = new UnityDependencyInjector();
            var validators = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetCallingAssembly());
            validators.ForEach(validator => unity.Container.RegisterType(validator.InterfaceType, validator.ValidatorType, null, null));
            _dependencyInjector = unity;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return _dependencyInjector.GetService(validatorType) as IValidator;
        }
    }
}
