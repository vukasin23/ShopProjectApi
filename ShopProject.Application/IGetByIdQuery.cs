namespace ShopProject.Application;

public interface IGetByIdQuery<TResponse> : IUseCase
{
    TResponse Execute(int id);
}
