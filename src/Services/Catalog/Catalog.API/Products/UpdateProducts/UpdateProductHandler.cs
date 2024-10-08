﻿using Catalog.API.Exception;
using FluentValidation;

namespace Catalog.API.Products.UpdateProducts
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Product Category is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description is required")
                .Length(2, 250).WithMessage("Description must be between 2 and 250 characters");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than 0");
        }
    }

    internal class UpdateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
