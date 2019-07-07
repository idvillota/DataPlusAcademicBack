using DataPlus.Contracts.Logger;
using DataPlus.Contracts.Repositories;
using DataPlus.Contracts.Services;
using DataPlus.Entities;
using DataPlus.Entities.Models;
using DataPlus.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace DataPlus.Test.Signature
{
    public class SignatureServiceTest
    {
        private IList<DataPlus.Entities.Models.Signature> _signatureList;

        private ILoggerManager _logger;

        public SignatureServiceTest()
        {
            _signatureList = GetSignatureList();
            _logger = new Mock<ILoggerManager>().Object;
        }

        private IList<Entities.Models.Signature> GetSignatureList()
        {
            return new List<DataPlus.Entities.Models.Signature>
            {
                new DataPlus.Entities.Models.Signature
            {
                Id = Guid.NewGuid(),
                Name = "Signature 1",
                Description = "Signature 1 Description"
            },
                   new DataPlus.Entities.Models.Signature
            {
                Id = Guid.NewGuid(),
                Name = "Signature 3",
                Description = "Signature 3 Description"
            },
                      new DataPlus.Entities.Models.Signature
            {
                Id = Guid.NewGuid(),
                Name = "Signature 4",
                Description = "Signature 4 Description"
                      },
                   new DataPlus.Entities.Models.Signature
            {
                Id = Guid.NewGuid(),
                Name = "Signature 2",
                Description = "Signature 2 Description"
            }
            };

        }

        [Fact]
        public void GetAll_RequestAListOfSignatures_ListOfSignatures()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Signature.GetAll()).Returns(_signatureList.AsQueryable);

            var signatureService = new SignatureService(repositoryWrapper.Object, _logger);
            var signatures = signatureService.GetAll();

            Assert.True(signatures != null);
            Assert.True(signatures.Count == 4);
        }

        [Fact]
        public void GetAll_RequestAListOfSignaturesWithNullRepositoryWrapper_Exception()
        {
            var signatureService = new SignatureService(null, _logger);
            Assert.Throws<NullReferenceException>(() => signatureService.GetAll());
        }

        [Fact]
        public void GetById_DummySignatureId_EmptySignature()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var signatureId = new Guid("8dedfd1f-45b8-4a8f-a25f-730e352ca629");
            repositoryWrapper.Setup(x => x.Signature.GetById(signatureId)).Returns(new Entities.Models.Signature());

            var signatureService = new SignatureService(repositoryWrapper.Object, _logger);
            var signature = signatureService.GetById(signatureId);

            Assert.True(signature.Name == null);
            Assert.True(signature.Id == Guid.Empty);
        }

        [Fact]
        public void GetById_SendAndIdToGetAnSignature_SignatureFilteredById()
        {
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var signatureId = _signatureList.First().Id;
            repositoryWrapper.Setup(x => x.Signature.GetById(signatureId)).Returns(new DataPlus.Entities.Models.Signature
            {
                Id = signatureId,
                Name = "Signature 1",
                Description = "Signature 1 Description"
            });

            var signatureService = new SignatureService(repositoryWrapper.Object, _logger);
            var signature = signatureService.GetById(signatureId);

            Assert.True(signature != null);
            Assert.True(signature.Name == "Signature 1");
            Assert.True(signature.Description == "Signature 1 Description");
        }

        [Fact]
        public void Update_SignatureWithAllData_SignatureUpdated()
        {
            var signatureId = _signatureList.First().Id;
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Signature.GetAll()).Returns(_signatureList.AsQueryable);
            repositoryWrapper.Setup(x => x.Signature.GetById(signatureId)).Returns(_signatureList.First(s => s.Id == signatureId));

            SignatureService signatureService = new SignatureService(repositoryWrapper.Object, _logger);

            var signature = signatureService.GetById(signatureId);
            signature.Name = signature.Name + "_modified";

            signatureService.Update(signature);

            var updatedSignature = signatureService.GetById(signature.Id);
            Assert.Contains("_modified", updatedSignature.Name);
        }

        [Fact]
        public void Update_SignatureWithoutId_Exception()
        {
            var signature = new DataPlus.Entities.Models.Signature();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var signatureService = new SignatureService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => signatureService.Update(signature));
        }

        [Fact]
        public void Create_SignatureWithData_NewSignature()
        {
            var newSignature = new DataPlus.Entities.Models.Signature
            {
                Id = Guid.NewGuid(),
                Name = "Signature 6",
                Description = "Signature 6 Description"
            };

            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Signature.GetAll()).Returns(_signatureList.AsQueryable);

            var signatureService = new SignatureService(repositoryWrapper.Object, _logger);
            var numberOfSignatures = signatureService.GetAll().Count;
            signatureService.Create(newSignature);

        }

        [Fact]
        public void Create_EmptySignature_Exception()
        {
            var newSignature = new DataPlus.Entities.Models.Signature();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            var signatureService = new SignatureService(repositoryWrapper.Object, _logger);

            Assert.Throws<NullReferenceException>(() => signatureService.Create(newSignature));
        }

        [Fact]
        public void Delete_ExistingSignature_Successfully()
        {
            var signatureToDelete = new Entities.Models.Signature();
            var repositoryWrapper = new Mock<IWrapperRepository>();
            repositoryWrapper.Setup(x => x.Signature.Delete(signatureToDelete));

            var signatureService = new SignatureService(repositoryWrapper.Object, _logger);
            signatureService.Delete(signatureToDelete);

        }
    }
}
