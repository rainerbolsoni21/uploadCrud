using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISession _session;

        public ProductController(ISession session)
        {
            _session = session;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _session.Query<Product>().ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                using(ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(product);
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Create));
                }
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int productId)
        {
            return View(await _session.GetAsync<Product>(productId));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int productId, Product product)
        {
            if (productId != product.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveOrUpdateAsync(product);
                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            var product = await _session.GetAsync<Product>(productId);

            using (ITransaction transaction = _session.BeginTransaction())
            {
                await _session.DeleteAsync(product);
                await transaction.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
