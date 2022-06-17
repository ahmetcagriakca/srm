using Fix.Environment.Dependency;
using FluentValidation;
using System;

namespace Fix.Validation
{
    public class FixValidatorFactory : IValidatorFactory, IDependency
    {
        private readonly IDependencySolver dependencySolver;

        public FixValidatorFactory(IDependencySolver dependencySolver)
        {
            this.dependencySolver = dependencySolver ?? throw new ArgumentNullException(nameof(dependencySolver));
        }
        public IValidator<T> GetValidator<T>()
        {

            return dependencySolver.Get<IValidator<T>>();
        }

        public IValidator GetValidator(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return dependencySolver.Get(typeof(IValidator<>).MakeGenericType(type)) as IValidator;
        }
    }
}
