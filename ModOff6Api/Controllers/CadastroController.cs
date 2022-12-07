using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModOff6Api.Context;
using ModOff6Api.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModOff6Api.Controllers
{
    [Route(template:"V1")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly Contexto _context;
        public CadastroController(Contexto context)
        {
            _context = context;
        }



        [HttpGet]
        [Route(template: "Cadastro/GetAsync")]
        public async Task<IActionResult> GetAsync()
        {
            var cad = await _context.Cadastros.ToListAsync();
            return Ok(cad);
        }

        [HttpGet]
        [Route(template:"Cadastro/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var cad = await _context.Cadastros.FirstOrDefaultAsync(x =>x.Id == id);
            return  cad == null ? NotFound() : Ok(cad);
        }

        [HttpPost(template: "Cadastro/PostAsync")]
        public async Task<IActionResult> PostAsync([FromBody] Cadastro cadastro)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                
                
                    //foreach (var item in cadastro)
                    //{
                        await _context.Cadastros.AddAsync(cadastro);
                        await _context.SaveChangesAsync();
                        return Created(uri: $"V1/Cadastro/{cadastro.Id}", cadastro);
                    // }
                

                //return Ok();

            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPut(template: "Cadastro/{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id,[FromBody] Cadastro cadastro)
        {
            if (!ModelState.IsValid) return BadRequest();
            var cad = await _context.Cadastros.FirstOrDefaultAsync(x => x.Id == id);
            if (cad == null) return NotFound(); 
            try
            {
                 cad = cadastro;
                 _context.Cadastros.Update(cad);
                await _context.SaveChangesAsync();
                return Ok(cad);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete(template:"Cadastro/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var cad = await _context.Cadastros.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                _context.Cadastros.Remove(cad);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest();
            }


        }
    }
}
