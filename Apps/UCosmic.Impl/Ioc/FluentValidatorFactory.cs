using FluentValidation;
using System;

namespace UCosmic
{
    public abstract class FluentValidatorFactory : ValidatorFactoryBase
    {
        private IServiceProvider Injector { get; set; }

        protected FluentValidatorFactory(IServiceProvider injector)
        {
            Injector = injector;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return Injector.GetService(validatorType) as IValidator;
        }
    }
}
