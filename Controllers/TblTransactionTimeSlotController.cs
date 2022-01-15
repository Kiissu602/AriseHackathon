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
    public class TblTransactionTimeSlotController : ControllerBase
    {
        private readonly DBQuizContext _context;

        public TblTransactionTimeSlotController(DBQuizContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<TblTransactionTimeSlot>>> GetTblInstructor()
        {
            return await _context.TblTransactionTimeSlots.ToListAsync();
        }

        [HttpGet("Getfreetime")]
        public async Task<ActionResult<List<bookedTimeDto>>> FreeTimeOfInstructor([FromBody] GetFreetimeDto instructor)
        {
            return await _GennerateAllFreetime(instructor);
        }

        [HttpPost("Booking")]
        public async Task<ActionResult<TblTransactionTimeSlot>> BookingInstructor([FromBody] TblTransactionTimeSlotDto booking)
        {
            var id =  _context.TblTransactionTimeSlots.Count();
            var newBooking = new TblTransactionTimeSlot
            {
                InstrutorCode = booking.InstrutorCode,
                CreateDate = DateTime.Parse(booking.CreateDate),
                TimeStart = TimeSpan.Parse(booking.TimeStart),
                TimeEnd = TimeSpan.Parse(booking.TimeEnd)
            };
            _context.TblTransactionTimeSlots.Add(newBooking);

            await _context.SaveChangesAsync();

            return newBooking;
        }

        private async Task<List<bookedTimeDto>> _GennerateAllFreetime(GetFreetimeDto trainer)
        {
            var day = (await _context.TblInstructors
                .Where(t => trainer.InstrutorCode == t.InstructorCode)
                .Select(t => t.ActiveDay)
                .FirstAsync()).Split("|");

            string[] allDay = { "Monday", "TuesDay", "Wednesday", "Thurseday", "Friday", "Saturday", "Sunday" };

            var freeDay = new List<string>();

            for (int i = 0; i < 7; i++)
            {
                freeDay.Add(allDay[int.Parse(day[i])]);
            }
            bool free = false;
            foreach (string f in freeDay)
            {
                if (DateTime.Parse(trainer.Date).ToString().Contains(f))
                {
                    free = true;
                }
            }
            if (!free)
            {
                return new List<bookedTimeDto>();
            }
            var date = DateTime.Parse(trainer.Date).ToString();


            var time = await _context.TblInstructors
                .Where(t => trainer.InstrutorCode == t.InstructorCode)
                .Select(t => new AllFreeTimeDto
                {
                    Start = t.TimeStart.Value,
                    End = t.TimeEnd.Value,
                    Period = t.Period.Value
                }).FirstAsync();

            var start = int.Parse(time.Start.ToString().Substring(0, 2));
            var end = int.Parse(time.End.ToString().Substring(0, 2));
            var allPreriods = new List<bookedTimeDto>();

            for (int i = start; i < end; i += time.Period)
            {
                allPreriods.Add(new bookedTimeDto { Start = TimeSpan.FromHours(i), End = TimeSpan.FromHours(i + 1) });
            }

            var bookedtime = await _context.TblTransactionTimeSlots
                .Where(tbl => trainer.InstrutorCode == tbl.InstrutorCode && trainer.Date == tbl.CreateDate.ToString())
                .Select(tbl => new bookedTimeDto
                {
                    Start = tbl.TimeStart.Value,
                    End = tbl.TimeEnd.Value
                }).ToListAsync();

            foreach (bookedTimeDto t in bookedtime)
            {
                allPreriods = allPreriods.Where(a => t.Start != a.Start && t.End != a.End).ToList();
            }

            return allPreriods;
        }
    }
}
