using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Services;
using DataPlus.Entities.Extensions;
using DataPlus.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DataPlus.Academic.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private ILoggerManager _logger;
        private ISignatureService _signatureService;

        public SignatureController(ILoggerManager logger, ISignatureService signatureService)
        {
            _logger = logger;
            _signatureService = signatureService;
        }
        
        // GET: api/Signature
        [HttpGet]
        [ProducesResponseType(typeof(IList<Signature>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetAllSignatures()
        {
            try
            {
                var signatures = _signatureService.GetAll();
                _logger.LogInfo($"Returned all signatures from database.");
                return Ok(signatures);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSignatures action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get signatures filter by Id
        /// </summary>
        /// <param name="id">Signature id</param>
        [HttpGet("{id}", Name = "SignatureById")]
        [ProducesResponseType(typeof(Signature), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetSignatureById(Guid id)
        {
            try
            {
                var signature = _signatureService.GetById(id);
                if (signature.IsEmptyObject())
                {
                    _logger.LogError($"Signature with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned signature with id: {id}");
                    return Ok(signature);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSignatureById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{id}/account")]
        //public IActionResult GetSignatureWithDetails(Guid id)
        //{
        //    try
        //    {
        //        var signature = _signatureService.GetSignatureWithDetails(id);

        //        if (signature.Id.Equals(Guid.Empty))
        //        {
        //            _logger.LogError($"Signature with id: {id}, hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInfo($"Returned signature with details for id: {id}");
        //            return Ok(signature);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside GetSignatureWithDetails action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        /// <summary>
        /// Allow create a signature
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Signature), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult CreateSignature([FromBody]Signature signature)
        {
            try
            {
                if (signature.IsObjectNull())
                {
                    _logger.LogError("Signature object sent from client is null.");
                    return BadRequest("Signature object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid signature object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _signatureService.Create(signature);
                return CreatedAtRoute("SignatureById", new { id = signature.Id }, signature);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateSignature action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow update a signature
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Signature), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSignature(Guid id, [FromBody]Signature signature)
        {
            try
            {
                if (signature.IsObjectNull())
                {
                    _logger.LogError("Signature object sent from client is null.");
                    return BadRequest("Signature object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid signature object sent from client.");
                    return BadRequest("Invalid model object");
                }
                
                _signatureService.Update(signature);

                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateSignature action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Allow delete a signature
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Signature), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult DeleteSignature(Guid SignatureId)
        {
            try
            {
                if (SignatureId == null || SignatureId == Guid.Empty )                
                {
                    _logger.LogError("Signature id null or empty");
                    return BadRequest("Signature id is null or empty");
                }

                var dbSignature = _signatureService.GetById(SignatureId);
                if (dbSignature.IsEmptyObject())
                {
                    _logger.LogError($"Signature with id: {SignatureId}, hasn't been found in db.");
                    return NotFound();
                }

                _signatureService.Delete(dbSignature);

                return NoContent();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteSignature action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
