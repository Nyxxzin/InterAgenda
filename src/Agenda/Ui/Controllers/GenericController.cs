namespace Ui.Controllers
{
    public class GenericController<TEntity, Tkey>
        : Microsoft.AspNetCore.Mvc.Controller
        where TEntity : class
    {
        protected virtual Services.Generic.IGenericService<TEntity> Service { get; }

        public GenericController(Services.Generic.IGenericService<TEntity> service)
        {
            Service = service;
        }

        // GET: TEntity
        public virtual async
            System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult>
            Index() => View(await GetAll());

        // GET: TEntity/Details/5
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Details(Tkey id)
        {
            if (id == null)
                return NotFound();

            var resource = await GetById(id);

            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // GET: TEntity/Create
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Create()
        {
            await AddViewDataAsync();
            return View();
        }

        // POST: TEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Create(TEntity resource)
        {
            if (!ModelState.IsValid)
                return View(resource);

            await Service.AddAsync(resource);
            return RedirectToAction(nameof(Index));
        }

        // GET: TEntity/Edit/5
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Edit(Tkey id)
        {
            if (id == null)
                return NotFound();

            var resource = await GetById(id);

            if (resource == null)
                return NotFound();

            await AddViewDataAsync(resource);

            return View(resource);
        }

        // POST: TEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Edit(Tkey id, TEntity resource)
        {
            if (!ModelState.IsValid)
            {
                await AddViewDataAsync(resource);
                return View(resource);
            }

            try
            {
                await Service.UpdateAsync(resource, id);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!await Exists(id))
                    return NotFound();

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TEntity/Delete/5
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Delete(Tkey id)
        {
            if (id == null)
                return NotFound();

            var resource = await GetById(id);
            if (resource == null)
                return NotFound();

            return View(resource);
        }

        // POST: TEntity/Delete/5
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.ActionName("Delete")]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> DeleteConfirmed(Tkey id)
        {
            if (!await Exists(id))
                return NotFound();

            var resource = await Service.GetAsync(id);

            await Service.DeleteAsync(resource);

            return RedirectToAction(nameof(Index));
        }

        protected virtual async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TEntity>> GetAll() => await Service.GetAllAsync();

        protected virtual async System.Threading.Tasks.Task<bool> Exists(Tkey id) => await Service.GetAsync(id) != null;

        protected virtual async System.Threading.Tasks.Task<TEntity> GetById(Tkey id) => await Service.GetAsync(id);

        protected virtual async System.Threading.Tasks.Task<object> AddViewDataAsync(TEntity resource = null)
            => await System.Threading.Tasks.Task.FromResult(default(object));
    }
}
