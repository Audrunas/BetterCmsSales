using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Mvc.Commands;

using BetterCms.Module.Root.Mvc;

using BetterCms.Module.Sales.Models;
using BetterCms.Module.Sales.ViewModels;

namespace BetterCms.Module.Sales.Command.SaveProduct
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
            Product product;

            if (isNew)
            {
                product = new Product();
            }
            else
            {
                product = Repository.AsQueryable<Product>(w => w.Id == request.Id).FirstOne();
            }

            product.Name = request.Name;
            product.Version = request.Version;

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