
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, decimal Price, string ImageFile)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Product Category is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product Description is required")
                .Length(2, 250).WithMessage("Description must be between 2 and 250 characters");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price must be greater than 0");
        }
    }
    internal class CreateProductCommandHandler
        (IDocumentSession session)
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
