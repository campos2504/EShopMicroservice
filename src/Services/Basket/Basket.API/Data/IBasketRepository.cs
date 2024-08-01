namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task DeleteBasket(string userName, CancellationToken cancellationToken);
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
    }
}
