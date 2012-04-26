using FluentValidation;
using System;

namespace UCosmic.Impl
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        private IServiceProvider Injector { get; set; }

        public FluentValidatorFactory(IServiceProvider injector)
        {
            Injector = injector;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return Injector.GetService(validatorType) as IValidator;
        }
    }
}
