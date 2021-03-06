using Mendes.Trucks.Domain.Specifications;
using System;

/*
* https://www.codeproject.com/Tips/790758/Specification-and-Notification-Patterns
*/
namespace Mendes.Trucks.Domain.Extensions
{
	public static class ExtensionMethods
	{
		public static ISpecification<T> And<T>(this ISpecification<T> spec1, ISpecification<T> spec2)
		{
			return new AndSpecification<T>(spec1, spec2);
		}

		public static ISpecification<T> Or<T>(this ISpecification<T> spec1, ISpecification<T> spec2)
		{
			return new OrSpecification<T>(spec1, spec2);
		}

		public static ISpecification<T> Not<T>(this ISpecification<T> spec)
		{
			return new NotSpecification<T>(spec);
		}
	}

	public class AndSpecification<T> : ISpecification<T>
	{
		private readonly ISpecification<T> _spec1;
		private readonly ISpecification<T> _spec2;

		public AndSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
		{
			_spec1 = spec1 ?? throw new ArgumentNullException(nameof(spec1));
			_spec2 = spec2 ?? throw new ArgumentNullException(nameof(spec2));
		}

		public bool IsSatisfiedBy(T candidate) =>
			_spec1.IsSatisfiedBy(candidate) && _spec2.IsSatisfiedBy(candidate);
	}

	public class OrSpecification<T> : ISpecification<T>
	{
		private readonly ISpecification<T> _spec1;
		private readonly ISpecification<T> _spec2;

		public OrSpecification(ISpecification<T> spec1, ISpecification<T> spec2)
		{
			_spec1 = spec1 ?? throw new ArgumentNullException(nameof(spec1));
			_spec2 = spec2 ?? throw new ArgumentNullException(nameof(spec2));
		}

		public bool IsSatisfiedBy(T candidate) =>
			_spec1.IsSatisfiedBy(candidate) || _spec2.IsSatisfiedBy(candidate);
	}

	public class NotSpecification<T> : ISpecification<T>
	{
		private readonly ISpecification<T> _spec;

		public NotSpecification(ISpecification<T> spec)
		{
			_spec = spec ?? throw new ArgumentNullException(nameof(spec));
		}

		public bool IsSatisfiedBy(T candidate) =>
			!_spec.IsSatisfiedBy(candidate);
	}
}
