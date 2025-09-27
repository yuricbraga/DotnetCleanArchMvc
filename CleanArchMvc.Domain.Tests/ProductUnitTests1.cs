using CleanArchMvc.Domain.Entities;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTests1
{
  [Fact]
  public void CreateProduct_WithValidParameters_ResultObjectValidState()
  {
    Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "Product image");
    action.Should()
      .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
  }

  [Fact]
  public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
  {
    Action action = () => new Product(-1, "Product name", "Product description", 9.99m, 99, "Product image");
    action.Should()
      .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
      .WithMessage("Invalid Id value.");
  }

  [Fact]
  public void CreateProduct_ShortNameValue_DomainExceptionShortName()
  {
    Action action = () => new Product(1, "Pr", "Product description", 9.99m, 99, "Product image");
    action.Should()
      .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
      .WithMessage("Invalid name, too short, minimum 3 characters.");
  }

  [Fact]
  public void CreateProduct_LongImageName_DomainExceptionLongImageName()
  {
    string productImage = string.Create(251, 'a', (span, state) =>
    {
      span.Fill(state);
    });
    Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, productImage);
    action.Should()
      .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
      .WithMessage("Invalid image name, too long, maximum 250 characters.");
  }

  [Fact]
  public void CreateProduct_WithNullImageName_NoDomainException()
  {
    Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
    action.Should()
      .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
  }

  [Fact]
  public void CreateProduct_WithNullImageName_NoNullReferenceException()
  {
    Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
    action.Should()
      .NotThrow<NullReferenceException>();
  }

  [Fact]
  public void CreateProduct_WithEmptyImageName_NoDomainException()
  {
    Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "");
    action.Should()
      .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
  }

  [Fact]
  public void CreateProduct_InvalidPriceValue_DomainException()
  {
    Action action = () => new Product(1, "Product name", "Product description", -9.99m, 99, "Product image");
    action.Should()
      .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
      .WithMessage("Invalid price value.");
  }

  [Theory]
  [InlineData(-5)]
  public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
  {
    Action action = () => new Product(1, "Product name", "Product description", 9.99m, value, "Product image");
    action.Should()
      .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
      .WithMessage("Invalid stock value.");
  }
}
