# BVT.RepositoryWebApi
This is Dll project to use Web Api from MVC projects v1.0

Usage:

    public class ProductController : Controller
    {
        private RepositoryWebApi<Product> _repositoryProd;

        public ProductController()
        {
            _repositoryProd = new RepositoryWebApi<Product>("products");
        }
        
        public ActionResult Index()
        {
             return View(_repositoryProd.Get());
        }
        
        public ActionResult Details(int id)
        {
            var product = _repositoryProd.GetById(id);
            return View(product);
        }

        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductDTOAll model)
        {
            try
            {
                _repositoryProd.Create(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            return View(_repositoryProd.GetById(id));
        }
        
        [HttpPost]
        public ActionResult Edit(int id, ProductDTOAll model)
        {
            try
            {
                _repositoryProd.Update(id, model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            return View(_repositoryProd.GetById(id));
        }
 

        [HttpPost]
        public ActionResult Delete(int id, ProductDTOAll model)
        {
            try
            {
                _repositoryProd.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

Actions: 
      void Delete(T item);
      void Delete<TProperty>(TProperty id);
        
      List<T> Get();
      List<T> Get(string actionName);
      List<T> Get<TProperty>(string actionName, TProperty id);
      List<T> Get<TProperty>(string actionName, object args);
      T GetById<TProperty>(TProperty id);
      T GetById<TProperty>(string actionName, TProperty id);
      T GetByParams(string actionName, object args);
        
      void Update<TProperty>(TProperty id, T item);

      T Create(string actionName, T item);
        
      string SerializeEntity(T item);
        
  
        
