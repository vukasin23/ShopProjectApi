namespace ShopProject.Application;

public interface IGetByOne<TData, TSearch> : IUseCase
{
    TData Execute(TSearch search);
}
