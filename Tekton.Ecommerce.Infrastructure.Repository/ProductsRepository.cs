using Dapper;
using System.Data;
using Tekton.Ecommerce.Domain.Entity;
using Tekton.Ecommerce.Infrastructure.Data;
using Tekton.Ecommerce.Infrastructure.Interface;

namespace Tekton.Ecommerce.Infrastructure.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DapperContext _context;

        public ProductsRepository(DapperContext context)
        {
            _context = context;
        }

        #region Métodos Síncronos

        public bool Insert(Products products)
        {
            using (var connection = _context.createConnection())
            {
                var query = "ProductInsert";
                var parameters = new DynamicParameters();
                parameters.Add("Name", products.Name);
                parameters.Add("Status", products.Status);
                parameters.Add("Stock", products.Stock);
                parameters.Add("Description", products.Description);
                parameters.Add("Price", products.Price);

                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool Update(Products products)
        {
            using (var connection = _context.createConnection())
            {
                var query = "ProductUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", products.ProductID);
                parameters.Add("Name", products.Name);
                parameters.Add("Status", products.Status);
                parameters.Add("Stock", products.Stock);
                parameters.Add("Description", products.Description);
                parameters.Add("Price", products.Price);

                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool Delete(string productId)
        {
            using (var connection = _context.createConnection())
            {
                var query = "ProductDelete";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", productId);

                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public Products? Get(string productId)
        {

            using (var connection = _context.createConnection())
            {
                var query = "ProductGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", productId);

                try
                {
                    var customer = connection.QuerySingle<Products>(query, param: parameters, commandType: CommandType.StoredProcedure);
                    return customer;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

        public IEnumerable<Products> GetAll()
        {
            using (IDbConnection connection = _context.createConnection())
            {
                var query = "ProductList";

                var products = connection.Query<Products>(query, commandType: CommandType.StoredProcedure);
                return products;
            }
        }
        #endregion


        #region Métodos Asíncronos

        public async Task<bool> InsertAsync(Products products)
        {
            using (var connection = _context.createConnection())
            {
                var query = "ProductInsert";
                var parameters = new DynamicParameters();
                parameters.Add("Name", products.Name);
                parameters.Add("Status", products.Status);
                parameters.Add("Stock", products.Stock);
                parameters.Add("Description", products.Description);
                parameters.Add("Price", products.Price);

                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> UpdateAsync(Products products)
        {
            using (var connection = _context.createConnection())
            {
                var query = "ProductUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", products.ProductID);
                parameters.Add("Name", products.Name);
                parameters.Add("Status", products.Status);
                parameters.Add("Stock", products.Stock);
                parameters.Add("Description", products.Description);
                parameters.Add("Price", products.Price);

                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(string productId)
        {
            using (var connection = _context.createConnection())
            {
                var query = "ProductDelete";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", productId);

                var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<Products> GetAsync(string productId)
        {
            using (var connection = _context.createConnection())
            {
                var query = "ProductGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("ProductID", productId);

                try
                {
                    var customer = await connection.QuerySingleAsync<Products>(query, param: parameters, commandType: CommandType.StoredProcedure);
                    return customer;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            using (IDbConnection connection = _context.createConnection())
            {
                var query = "ProductList";

                var products = await connection.QueryAsync<Products>(query, commandType: CommandType.StoredProcedure);
                return products;
            }
        }
        #endregion

    }
}