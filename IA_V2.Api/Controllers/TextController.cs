using AutoMapper;
using IA_V2.Api.Responses;
using IA_V2.Core.Entities;
using IA_V2.Core.Interfaces;
using IA_V2.Infrastructure.DTOs;
using IA_V2.Infrastructure.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IA_V2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextService _textService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public TextController(ITextService textService, IMapper mapper, IValidationService validationService)
        {
            _textService = textService;
            _mapper = mapper;
            _validationService = validationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var texts = await _textService.GetAllTextAsync();
                var result = _mapper.Map<IEnumerable<TextDTO>>(texts);
                return Ok(result); 
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var text = await _textService.GetTextByIdAsync(id);
                var textDto = _mapper.Map<TextDTO>(text);
                var response = new ApiResponse<TextDTO>(textDto);
                return Ok(response);
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(TextDTO dto)
        {
            try
            {
                var validation = await _validationService.ValidateAsync(dto);
                if (!validation.IsValid)
                    return BadRequest(new { errores = validation.Errors });
                var text = _mapper.Map<Text>(dto);
                await _textService.InsertTextAsync(text);

                var response = _mapper.Map<TextDTO>(text);
                return Ok(response);
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TextDTO dto)
        {
            try
            {
                dto.Id = id;
                var validation = await _validationService.ValidateAsync(dto);
                if (!validation.IsValid)
                    return BadRequest(new { errores = validation.Errors });

                var text = await _textService.GetTextByIdAsync(id);

                _mapper.Map(dto, text);

                await _textService.UpdateTextAsync(text);

                var textDto = _mapper.Map<TextDTO>(text);
                var response = new ApiResponse<TextDTO>(textDto);
                return Ok(response);
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _textService.DeleteTextAsync(id);
                return Ok(new { message = $"El texto con ID {id} fue eliminado correctamente." });
            }
            catch (Exception err)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, err.Message);
            }
        }
    }
}
