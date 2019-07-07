using DataPlus.Academic.Controllers.Api;
using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Contracts.Services;
using DataPlus.Entities;
using DataPlus.Entities.Models;
using DataPlus.Repository;
using DataPlus.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataPlus.Test.Signature
{
    public class SignatureControllerTest : BaseControllerTest, IDisposable
    {   
        private ISignatureService _signatureService;
     
        public SignatureControllerTest()
        {
            _signatureService = new SignatureService(_repositoryWrapper, _logger);
        }

        [Fact]
        public void GetById_ExistingId_OkResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var signatureId = new Guid("EED7F976-58B7-4C05-E3E4-08D6F9F0FC2C");
            var data = controller.GetSignatureById(signatureId);
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetById_NoExistingId_NotFoundResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var signatureId = new Guid("A9253E48-A1AB-4F83-A1A7-0E14D0F12345");
            var data = controller.GetSignatureById(signatureId);
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void GetById_NullId_BadRequestResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            Guid signatureId = Guid.Empty;
            var data = controller.GetSignatureById(signatureId);
        }

        [Fact]
        public void GetAll_RequestSignatures_OkResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var data = controller.GetAllSignatures();
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetAll_RequestSignatures_MatchResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var data = controller.GetAllSignatures();
            Assert.IsType<OkObjectResult>(data);

            var signatures = data as OkObjectResult;
            var signatureList = signatures.Value as IList<Entities.Models.Signature>;

            Assert.Equal("eed7f976-58b7-4c05-e3e4-08d6f9f0fc2c", signatureList[0].Id.ToString());
            Assert.Equal("Signature 1", signatureList[0].Name);
        }

        [Fact]
        public void Create_SignatureWithValidData_OkResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var signature = new Entities.Models.Signature
            {
                Id = new Guid("e47c0cb5-05b6-437d-b40f-f2c5b5a08385"),
                Name = "Signature 6",
                Description = "Signature 6 description"
            };

            var data = controller.CreateSignature(signature);

            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);
        }

        [Fact]
        public void Create_SignatureWithInvalidData_BadRequest()
        {
            //var controller = new SignatureController(_logger, _signatureService);
            //var signature = new Entities.Models.Signature
            //{
            //    //Id = new Guid("e47c0cb5-05b6-437d-b40f-f2c5b5a08385"),
            //    FirstName = "HomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomeroHomero",
            //    LastName = "Simpson",
            //    DocumentType = Entities.Models.EDocumentType.CC,
            //    DocumentNumber = "1245687",
            //    Address = "Springfield 123",
            //    Email = "homero@hotmail.com",
            //    City = "Springfield",
            //    PhoneNumber = "7654321",
            //    Birth = new DateTime(1950, 11, 1)
            //};

            //var data = controller.CreateSignature(signature);
            //Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void Update_SignatureWithValidData_OkResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var signatureId = new Guid("EED7F976-58B7-4C05-E3E4-08D6F9F0FC2C");
            var existingSignature = controller.GetSignatureById(signatureId);

            Assert.IsType<OkObjectResult>(existingSignature);


            var signature = (existingSignature as ObjectResult).Value as Entities.Models.Signature;
            signature.Name = "Signature Modified";

            var updatedData = controller.UpdateSignature(signatureId, signature);

            Assert.IsType<NoContentResult>(updatedData);
        }

        [Fact]
        public void Delete_ExistingSignature_OkResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var signature = new Entities.Models.Signature
            {
                Id = new Guid("f7afefa9-2cc6-4ea9-901d-d99e227a12de"),
                Name = "Signature",
                Description = ""
            };

            var data = controller.CreateSignature(signature);
            Assert.IsType<CreatedAtRouteResult>(data as CreatedAtRouteResult);

            var deleteResult = controller.DeleteSignature(signature.Id);
            Assert.IsType<NoContentResult>(deleteResult);
        }

        [Fact]
        public void Delete_NonExistingSignature_NotFoundResult()
        {
            var controller = new SignatureController(_logger, _signatureService);
            var signatureId = new Guid("1c575a69-27f3-4379-b7be-5798a0001449");
            var data = controller.DeleteSignature(signatureId);
            Assert.IsType<NotFoundResult>(data);
        }

        public void Dispose()
        {
            //Delete created signature:
            var createdSignature = _signatureService.GetById(new Guid("e47c0cb5-05b6-437d-b40f-f2c5b5a08385"));
            if (createdSignature != null && createdSignature.Id != Guid.Empty)
                _signatureService.Delete(createdSignature);

            //Recovery modified signature:
            var modifiedSignature = _signatureService.GetById(new Guid("EED7F976-58B7-4C05-E3E4-08D6F9F0FC2C"));
            if (modifiedSignature != null && modifiedSignature.Id != Guid.Empty)
            {
                modifiedSignature.Name = "Signature 1";
                _signatureService.Update(modifiedSignature);
            }

        }
    }
}
