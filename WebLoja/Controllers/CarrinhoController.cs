using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebLoja.Data;
using WebLoja.Models;

namespace WebLoja.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly WebLojaContext _context;

        public CarrinhoController(WebLojaContext context)
        {
            _context = context;
        }
        // GET: CarrinhoController
        public ActionResult<Carrinho> Index()
        {

            Carrinho carrinho;
            if (HttpContext.Session.GetString("carrinho") != null)
            {
                string jsonCarrinho = HttpContext.Session.GetString("carrinho");
                if (jsonCarrinho != null)
                {
                    var reader = new JsonTextReader(new StringReader(jsonCarrinho));
                    carrinho = JsonSerializer.CreateDefault().Deserialize<Carrinho>(reader);
                }
                else
                {
                    carrinho = new Carrinho();
                }
            }
            else
            {
                carrinho = new Carrinho();
            }

            return View(carrinho);
        }

        // GET: CarrinhoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarrinhoController/Create
        public ActionResult AddItem(int IdProduto, int Qt)
        {
            Produto produto = _context.Produto.Find(IdProduto);
            if (produto != null)
            {

                Carrinho carrinho;
                if (HttpContext.Session.GetString("carrinho") != null)
                {
                    string jsonCarrinho = HttpContext.Session.GetString("carrinho");
                    if (jsonCarrinho != null)
                    {
                        var reader = new JsonTextReader(new StringReader(jsonCarrinho));
                        carrinho = JsonSerializer.CreateDefault().Deserialize<Carrinho>(reader);
                    }
                    else
                    {
                        carrinho = new Carrinho();
                    }
                }
                else
                {
                    carrinho = new Carrinho();
                }

                if (carrinho.Itens == null)
                {
                    carrinho.Itens = new List<Item>();
                }
                Item item = new Item();
                item.Produto = produto;
                item.Quantidade = Qt;
                carrinho.AddItem(item);

                var jsCarrinho = JsonConvert.SerializeObject(carrinho);
                HttpContext.Session.SetString("carrinho", jsCarrinho);
                
                return RedirectToAction();
            }
            else
            { 
                return RedirectToAction("Index");
            }
        }


        // POST: CarrinhoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarrinhoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarrinhoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarrinhoController/Delete/5
        public ActionResult Delete(int id)
        {
            string jsonCarrinho = HttpContext.Session.GetString("carrinho");
            var reader = new JsonTextReader(new StringReader(jsonCarrinho));
            var carrinho = JsonSerializer.CreateDefault().Deserialize<Carrinho>(reader);

            carrinho.Itens.RemoveAt(id);

            var jsCarrinho = JsonConvert.SerializeObject(carrinho);
            HttpContext.Session.SetString("carrinho", jsCarrinho);

            return RedirectToAction("Index");
        }

    }
}
