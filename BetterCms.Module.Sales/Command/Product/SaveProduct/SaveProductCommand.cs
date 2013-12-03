using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;
using BetterCms.Module.Root.Mvc;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.Product.SaveProduct
{
    public class SaveProductCommand : CommandBase, ICommand<ProductViewModel, ProductViewModel>
    {
        /// <summary>
        /// Executes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ProductViewModel Execute(ProductViewModel request)
        {
            var isNew = request.Id.HasDefaultValue();
            Models.Product product;

            if (isNew)
            {
                product = new Models.Product();
            }
            else
            {
                product = Repository.AsQueryable<Models.Product>(w => w.Id == request.Id).FirstOne();
            }

            product.Name = request.Name;
            product.Version = request.Version;
            product.Unit = Repository.AsProxy<Models.Unit>(request.Unit);
            product.Price = 0;

            Repository.Save(product);
            UnitOfWork.Commit();

            return new ProductViewModel
                {
                    Id = product.Id,
                    Version = product.Version,
                    Name = product.Name
                };
        }
    }
}