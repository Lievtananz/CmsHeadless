using CmsHeadless.Models;
using CmsHeadless.ViewModels.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CmsHeadless.Pages.Attributes
{
    [Authorize]
    public class EditAttributesModel : PageModel
    {
        IQueryable<Models.Attributes> selectAttributesQuery;
        IQueryable<Models.Attributes> selectAttributesQueryOrder;
        public static int EditAttributesId = 0;
        public static int lastEdit = 0;
        public static int lastEditTypology = 0;
        public Models.Attributes attributes;
        public Models.Attributes EditAttributesNew { get; set; }
        [BindProperty]
        public EditAttributesViewModel _formEditAttributesModel { get; set; }
        private readonly Models.CmsHeadlessDbContext _context;
        public List<Models.Attributes> AttributesAvailable { get; set; }
        public List<Models.Typology> TypologyAvailable { get; set; }
        public List<int> TypologySelected { get; set; }
        public List<AttributesTypology> AttributesTypologySelected { get; set; }
        public List<AttributesTypology> AttributesTypology { get; set; }
        public EditAttributesModel(CmsHeadlessDbContext context)
        {
            _context = context;
            AttributesAvailable = new List<Models.Attributes>();

            IQueryable<Models.Typology> selectTypologyQuery = from Typology in _context.Typology select Typology;
            TypologyAvailable = selectTypologyQuery.ToList<Models.Typology>();

            TypologySelected = new List<int>();

            IQueryable<Models.AttributesTypology> selectAttributesTypologyQuery = from AttributesTypology in _context.AttributesTypology select AttributesTypology;
            AttributesTypologySelected=selectAttributesTypologyQuery.ToList<Models.AttributesTypology>();

            IQueryable<Models.AttributesTypology> AttributesTypologyQuery = from AttributesTypology in _context.AttributesTypology select AttributesTypology;
            AttributesTypology = AttributesTypologyQuery.ToList<Models.AttributesTypology>();
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            selectAttributesQueryOrder = from Attributes in _context.Attributes select Attributes;
            selectAttributesQuery = selectAttributesQueryOrder.OrderByDescending(c => c.AttributesId);
            AttributesAvailable = selectAttributesQuery.ToList<Models.Attributes>();
            if (id == null)
            {
                if (EditAttributesId != 0)
                {
                    id = EditAttributesId;
                }
                else
                {
                    return NotFound();
                }

            }
            EditAttributesId = (int)id;
            if (_context == null || _context.Attributes == null)
            {
                return NotFound();
            }
            attributes = await _context.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return NotFound();
            }

            AttributesTypologySelected = AttributesTypologySelected.Where(c => c.AttributesId == id).ToList();
            if (AttributesTypologySelected != null && AttributesTypologySelected.Count()>0)
            {
                foreach (var i in AttributesTypologySelected)
                {
                    TypologySelected.Add(i.TypologyId);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int attributesId)
        {
            lastEdit = 0;
            var attributesToUpdate = await _context.Attributes.FindAsync(attributesId);

            if (attributesToUpdate == null)
            {
                return NotFound();
            }

            if (attributesToUpdate.AttributeName != _formEditAttributesModel.AttributeName)
            {
                var attributesToSearch = from Attributes in _context.Attributes
                                  where Attributes.AttributeName == _formEditAttributesModel.AttributeName
                                  select Attributes;
                if (attributesToSearch.Count<Models.Attributes>() != 0)
                {
                    ModelState.AddModelError("Make", "Attributo gi� esistente. Inserirne un altro");
                    selectAttributesQueryOrder = from Attributes in _context.Attributes select Attributes;
                    selectAttributesQuery = selectAttributesQueryOrder.OrderByDescending(c => c.AttributesId);
                    AttributesAvailable = selectAttributesQuery.ToList<Models.Attributes>();
                    attributes = await _context.Attributes.FindAsync(attributesId);

                    return Page();
                }

            }

            attributesToUpdate.AttributeName = _formEditAttributesModel.AttributeName;
            attributesToUpdate.AttributeValue = _formEditAttributesModel.AttributeValue;

            /*AttributesTypology*/
            if (_formEditAttributesModel.Typology == null)
            {
                _formEditAttributesModel.Typology = new List<int>();
            }
            var tempAttributesTypology = new Models.AttributesTypology();
            IQueryable<int> selectTypologyIdQuery = from AttributesTypology in _context.AttributesTypology where AttributesTypology.AttributesId == attributesId select AttributesTypology.TypologyId;
            List<int> selectTypologyIdList = selectTypologyIdQuery.ToList<int>();
            foreach (int i in _formEditAttributesModel.Typology)
            {
                if (!selectTypologyIdList.Contains(i))
                {
                    var entryAttributesTypology = _context.Add(new Models.AttributesTypology());
                    tempAttributesTypology.AttributesId = attributesId;
                    tempAttributesTypology.TypologyId = i;
                    entryAttributesTypology.CurrentValues.SetValues(tempAttributesTypology);
                    lastEditTypology = await _context.SaveChangesAsync();
                    if (lastEditTypology <= 0)
                    {
                        ModelState.AddModelError("Make", "Errore nella modifica");
                        return Page();
                    }
                }
                else
                {
                    selectTypologyIdList.Remove(i);
                }

            }
            if (selectTypologyIdList.Count > 0)
            {

                foreach (int i in selectTypologyIdList)
                {
                    IQueryable<AttributesTypology> selectTypologyRemainIdQuery = from AttributesTypology in _context.AttributesTypology where (AttributesTypology.AttributesId == attributesId && AttributesTypology.TypologyId == i) select AttributesTypology;
                    AttributesTypology AttributesTypologyToDelete = selectTypologyRemainIdQuery.ToList<AttributesTypology>().First<AttributesTypology>();
                    _context.AttributesTypology.Remove(AttributesTypologyToDelete);
                }
            }


            lastEdit = await _context.SaveChangesAsync();

            selectAttributesQueryOrder = from Attributes in _context.Attributes select Attributes;
            selectAttributesQuery = selectAttributesQueryOrder.OrderByDescending(c => c.AttributesId);
            AttributesAvailable = selectAttributesQuery.ToList<Models.Attributes>();
            attributes = await _context.Attributes.FindAsync(attributesId);
            return RedirectToPage("./EditAttributes", new { id = attributesId });
        }
    }
}
