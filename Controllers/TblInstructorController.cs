using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblInstructorController : ControllerBase
    {
        private readonly DBQuizContext _context;

        public TblInstructorController(DBQuizContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TblInstructorDto>>> GetTblInstructors()
        {
            return await _context.TblInstructors.Select(t => new TblInstructorDto
            {
                Id = t.Id,
                InstructorCode = t.InstructorCode,
                FullName = t.FullName
            }).ToListAsync();
        }
    }
       
}
