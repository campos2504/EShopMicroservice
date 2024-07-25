﻿using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, decimal Price, string ImageFile)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product

            {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                Price = command.Price,
                ImageFile = command.ImageFile
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);

        }
    }
}
