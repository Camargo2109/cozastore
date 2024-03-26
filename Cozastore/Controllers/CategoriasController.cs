using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cozastore.Data;
using Cozastore.Models;
using System.Net;

namespace Cozastore.Controllers;

public class CategoriaController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _host;

    public CategoriaController(AppDbContext context, IWebHostEnvironment host)
    {
        _context = context;
        _host = host;
    }

    // GET: Categoria
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Categorias.Include(c => c.CategoriaMae);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Categoria/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .Include(c => c.CategoriaMae)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // GET: Categoria/Create
    public IActionResult Create()
    {
        ViewData["CategoriaMaeId"] = new SelectList(_context.Categorias, "Id", "Nome");
        return View();
    }

    // POST: Categoria/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Foto,Filtrar,Banner,CategoriaMaeId")] Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            _context.Add(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaMaeId"] = new SelectList(_context.Categorias, "Id", "Nome", categoria.CategoriaMaeId);
        return View(categoria);
    }

    // GET: Categoria/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        ViewData["CategoriaMaeId"] = new SelectList(_context.Categorias, "Id", "Nome", categoria.CategoriaMaeId);
        return View(categoria);
    }

    // POST: Categoria/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Foto,Filtrar,Banner,CategoriaMaeId")] Categoria categoria, IFormFile Foto)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
 
         _context.Update(categoria);
         await _context.SaveChangesAsync();
         if(Foto !=null)
         {
            string fileName = categoria.Id + Path.GetExtension(Foto.FileName);
            string upload = Path.Combine(_host.WebRootPath, "img\\categorias");
            string newFile = Path.Combine(upload, fileName);
            using (var stream = new FileStream(newFile, FileMode.Create))
            {
                Foto.CopyTo(stream);
            }
            categoria.Foto = "\\img\\categorias\\" + fileName;
            await _context.SaveChangesAsync();
         }
    
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaMaeId"] = new SelectList(_context.Categorias, "Id", "Nome", categoria.CategoriaMaeId);
        return View(categoria);
    }

    // GET: Categoria/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .Include(c => c.CategoriaMae)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // POST: Categoria/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoriaExists(int id)
    {
        return _context.Categorias.Any(e => e.Id == id);
    }
}

