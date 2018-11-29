using DevStore.Infra.DataContexts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using DevStore.Domain;
using System.Web.Http.Cors;

namespace DevStore.Api.Controllers
{   // origins(so vai receber requisição de uma determinada url)
    // headers( quais tipo de cabeçalho receber GET,POST,DELETE, ETC)
    // methods( quais tipo de metodos receber GET,POST,DELETE, ETC)
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1/public")] // localhost:61144/api/v1/public
    public class ProductController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();

        // Metodo GET
        [Route("products")] // /products
        public HttpResponseMessage GetProducts()
        {
            var result = db.Products.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories")] // /categories
        public HttpResponseMessage GetCategories()
        {
            var result = db.Categories.ToList(); // Pegando as Categorias
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories/{categoryId}/products")] // /categories
        public HttpResponseMessage GetProductsByCategories(int categoryId)
        {
            var result = db.Products.Include("Category").Where(x => x.CategoryId == categoryId).ToList(); // Pegando os produtos de uma categoria
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // Metodo POST
        [HttpPost]
        [Route("products")] // /products
        public HttpResponseMessage PostProduct(Product product) // Recebendo um produto da Requisição
        {
            if (product == null) // se o produto for nulo
                return Request.CreateResponse(HttpStatusCode.BadRequest); // retorna uma mensagem de erro BadRequest, se nao for continue

            try
            {
                db.Products.Add(product); // Adiciono o produto no banco de dados
                db.SaveChanges(); // Commit no banco de dados

                var result = product; // salva o valor da variavel products em uma variavel 
                return Request.CreateResponse(HttpStatusCode.OK, product); // Retorno um response na tela, com um 200OK passando o produto para ser renderizado
            }
            catch (System.Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir o produto.");
            }
        }

        // Método PATCH
        [HttpPatch] // Alterando um produto
        [Route("products")]
        public HttpResponseMessage PatchProduct(Product product) // Recebendo um produto da Requisição
        {
            if (product == null) // se o produto for nulo
                return Request.CreateResponse(HttpStatusCode.BadRequest); // retorna uma mensagem de erro BadRequest, se nao for continue

            try
            {
                db.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified; // Informa que o estado do produto foi modificado e salva a modificação
                db.SaveChanges(); // Commit no banco de dados

                var result = product; // salva o valor da variavel products em uma variavel 
                return Request.CreateResponse(HttpStatusCode.OK, product); // Retorno um response na tela, com um 200OK passando o produto para ser renderizado
            }
            catch (System.Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto.");
            }
        }

        // Método PUT
        [HttpPut] // Alterando um produto
        [Route("products")]
        public HttpResponseMessage PutProduct(Product product) // Recebendo um produto da Requisição
        {
            if (product == null) // se o produto for nulo
                return Request.CreateResponse(HttpStatusCode.BadRequest); // retorna uma mensagem de erro BadRequest, se nao for continue

            try
            {
                db.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified; // Informa que o estado do produto foi modificado e salva a modificação
                db.SaveChanges(); // Commit no banco de dados

                var result = product; // salva o valor da variavel products em uma variavel 
                return Request.CreateResponse(HttpStatusCode.OK, product); // Retorno um response na tela, com um 200OK passando o produto para ser renderizado
            }
            catch (System.Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto.");
            }
        }

        // Método DELETE
        /// <summary>
        ///  Chamar a url localhost/api/v1/public/products?productId=11 para deletar um produto passando o Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete] // Deletando um Produto
        [Route("products")]
        public HttpResponseMessage DeleteProduct(int productId) // Recebendo um produto pelo Id da Requisição
        {
            if (productId <= 0) // se o produto for menor ou igual a 0
                return Request.CreateResponse(HttpStatusCode.BadRequest); // retorna uma mensagem de erro BadRequest, se nao for continue

            try
            {
                db.Products.Remove(db.Products.Find(productId)); // Chama o metodo Remove procurando o produto no banco pelo Id, Remove o mesmo
                db.SaveChanges(); // Commit no banco de dados

                return Request.CreateResponse(HttpStatusCode.OK, "Produto Excluido"); // Retorno um response na tela, com um 200OK informando que o produto foi excluido
            }
            catch (System.Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao Deletar o produto.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}