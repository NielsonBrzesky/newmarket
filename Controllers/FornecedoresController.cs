using Microsoft.AspNetCore.Mvc;
using newmarket.Data;
using newmarket.DTO;
using newmarket.Models;

namespace newmarket.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext dataBase;

        public FornecedoresController(ApplicationDbContext dataBase) {
            this.dataBase = dataBase;
        }

        [HttpPost]
        public IActionResult Salvar(FornecedorDTO fornecedorTemp) {
            if (ModelState.IsValid) {
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Nome = fornecedorTemp.Nome;
                fornecedor.Email = fornecedorTemp.Email;
                fornecedor.Telefone = fornecedorTemp.Telefone;
                fornecedor.Status = true;
                dataBase.Fornecedores.Add(fornecedor);
                dataBase.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            } else {
                return View("../Gestao/NovoFornecedor");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(FornecedorDTO fornecedorTemp) {
            if(ModelState.IsValid) {
                var fornecedor = dataBase.Fornecedores.First(fornecedor => fornecedor.Id.Equals(fornecedorTemp.Id));
                fornecedor.Nome = fornecedorTemp.Nome;
                fornecedor.Email = fornecedorTemp.Email;
                fornecedor.Telefone = fornecedorTemp.Telefone;
                dataBase.SaveChanges();
                return RedirectToAction("Fornecedores", "Gestao");
            } else {
                return View("../Gestao/EditarFornecedor");
            }
        }
    }
}