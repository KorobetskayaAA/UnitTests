using System;
using Xunit;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.Data.Model;
using WebApp.Controllers;
using WebApp.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Moq;

namespace WebApp.Tests
{
    public class CitationsControllerTests
    {
        public CitationsControllerTests()
        {
            InitContext();
        }

        private AppDbContext _context;
        private IEnumerable<Citation> _citations;
        private readonly int _seed = 12345;

        private void InitContext()
        {
            var random = new Random(_seed);
            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestCitations");

            _context = new AppDbContext(builder.Options);
            _citations = Enumerable.Range(1, 10)
                .Select(i => new Citation 
                { 
                    Id = i, 
                    Quote = $"Sample quote {i}", 
                    Author = $"Author {random.Next(100)}",
                    Title = random.NextDouble() < 0.5 ? $"Title" : null,
                    Hero = random.NextDouble() < 0.5 ? $"Hero {random.Next(100)}" : null,
                })
                .ToList();
            if (_context.Citations.Count() != 10)
            {
                _context.Citations.AddRange(_citations);
                _context.SaveChanges();
            }
        }

        [Fact]
        public void Get_ById2_ReturnsCitation()
        {
            int id = 2;
            var expectedCitation = _citations.FirstOrDefault(c => c.Id == id);
            var repo = new CitationRepository(_context);
            var controller = new CitationsController(repo);

            var result = controller.Get(id);

            Assert.Null(result.Result);
            var citation = result.Value;
            Assert.Equal(expectedCitation.Quote, citation.Quote);
            Assert.Equal(expectedCitation.Author, citation.Author);
            Assert.Equal(expectedCitation.Hero, citation.Hero);
            Assert.Equal(expectedCitation.Title, citation.Title);
        }

        [Fact]
        public void Get_ById100_ReturnsNotFound()
        {
            int id = 100;
            var repo = new CitationRepository(_context);
            var controller = new CitationsController(repo);
            var result = controller.Get(id);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetAll_ReturnsCitations()
        {
            var repo = new CitationRepository(_context);
            var controller = new CitationsController(repo);
            var result = controller.GetAll(); 
            Assert.Equal(_citations.Count(), result.Count());
        }

        [Fact]
        public void Post_ReturnsCreated()
        {
            // Arrange
            int id = 999;
            string quote = "New quote";
            string author = "New Author";
            var c = new Citation()
            {
                Id = id,
                Quote = quote,
                Author = author,
            };
            var mockRepo = new Mock<IRepository<Citation, int>>();
            mockRepo.Setup(repo => repo.Create(It.IsAny<Citation>())).Returns(c);
            var controller = new CitationsController(mockRepo.Object);

            // Act
            var result = controller.Post(c);

            // Assert
            var citation = Assert.IsType<Citation>(result.Value);
            Assert.Equal(id, citation.Id);
            Assert.Equal(quote, citation.Quote);
            Assert.Equal(author, citation.Author);
            Assert.Null(citation.Hero);
            Assert.Null(citation.Title);
        }

        [Fact]
        public void Put_CorrectId_ReturnsOk()
        {
            // Arrange
            var c = new Citation()
            {
                Id = 999,
                Quote = "Updated quote",
                Author = "Updated Author",
            };
            var mockRepo = new Mock<IRepository<Citation, int>>();
            mockRepo.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Citation>())).Returns(true);
            var controller = new CitationsController(mockRepo.Object);

            // Act
            var result = controller.Put(c.Id, c);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Put_WrongId_ReturnsBadRequest()
        {
            // Arrange
            var c = new Citation()
            {
                Id = 999,
                Quote = "Updated quote",
                Author = "Updated Author",
            };
            var mockRepo = new Mock<IRepository<Citation, int>>();
            mockRepo.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Citation>())).Returns(true);
            var controller = new CitationsController(mockRepo.Object);

            // Act
            var result = controller.Put(1, c);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_WrongCitation_ReturnsBadRequest()
        {
            // Arrange
            var c = new Citation()
            {
                Id = 999,
                Quote = "Bad quote",
                Author = "Bad Author",
            };
            var mockRepo = new Mock<IRepository<Citation, int>>();
            mockRepo.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
            mockRepo.Setup(repo => repo.Update(It.IsAny<Citation>())).Returns(false);
            var controller = new CitationsController(mockRepo.Object);

            // Act
            var result = controller.Put(c.Id, c);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Delete_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var mockRepo = new Mock<IRepository<Citation, int>>();
            mockRepo.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
            mockRepo.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(true);
            var controller = new CitationsController(mockRepo.Object);

            // Act
            var result = controller.Delete(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNotFound()
        {
            // Arrange
            var id = 100;
            var mockRepo = new Mock<IRepository<Citation, int>>();
            mockRepo.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(false);
            var controller = new CitationsController(mockRepo.Object);

            // Act
            var result = controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
